using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var summonerUnit = turnManager.GetAttackerOnTurn();
        var summonEffect = new SummonEffect(View);

        var monsterToSummon = GetSelectedMonsterToSummon(boardManager, currentPlayerId);
        var boardFormation = CreatePlayerBoardFormation(boardManager, currentPlayerId);
        var summonData = new SummonData(summonerUnit, monsterToSummon);

        var displacedUnit = GetDisplacedUnit(summonData, summonEffect, boardFormation);
        UpdateTurnAndOrder(turnManager, summonData, displacedUnit);
    }

    private UnitBase GetSelectedMonsterToSummon(BoardManager boardManager, int currentPlayerId)
    {
        var availableReserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        int selectedIndex = ActionView.ReadSummonIndex(availableReserveUnits);

        if (IsSelectionCanceled(selectedIndex))
            throw new ActionCanceledException();

        return availableReserveUnits[selectedIndex];
    }

    private static PlayerBoardFormation CreatePlayerBoardFormation(BoardManager boardManager, int currentPlayerId)
    {
        var activeBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        return new PlayerBoardFormation(activeBoard, reserveUnits);
    }

    private UnitBase? GetDisplacedUnit(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        return summonData.Summoner is Samurai
            ? SummonWithSamurai(summonData, summonEffect, boardFormation)
            : SummonWithMonster(summonData, summonEffect, boardFormation);
    }

    private UnitBase? SummonWithSamurai(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        var summonOptions = GetSummonPositions(boardFormation.ActiveBoard);
        int selectedIndex = ActionView.ReadSummonPositionIndex(summonOptions);
        if (IsSelectionCanceled(selectedIndex))
            throw new ActionCanceledException();

        var (boardPosition, displacedUnit) = summonOptions[selectedIndex];
        var placement = new SummonPlacement(boardPosition, displacedUnit);

        return summonEffect.ApplySamuraiSummon(summonData.MonsterToSummon, boardFormation, placement);
    }

    private UnitBase? SummonWithMonster(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        return summonEffect.ApplyMonsterSummon(summonData, boardFormation);
    }

    private static List<(string BoardPosition, UnitBase? DisplacedUnit)> GetSummonPositions(Dictionary<string, UnitBase?> playerBoard)
    {
        return GameConstants.BoardPositions
            .Skip(1)
            .Select(position => (BoardPosition: position, DisplacedUnit: playerBoard[position]))
            .ToList();
    }

    private void UpdateTurnAndOrder(TurnManager turnManager, SummonData summonData, UnitBase? displacedUnit)
    {
        turnManager.UpdateOrderAfterSummon(summonData.Summoner, summonData.MonsterToSummon, displacedUnit);
        var turnChange = turnManager.ConsumeSummonTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
}
