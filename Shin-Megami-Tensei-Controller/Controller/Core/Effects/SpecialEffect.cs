using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillExecutionContext skillContext)
    {
        var boardManager = skillContext.BoardManager;
        var turnManager = skillContext.TurnManager;
        int currentPlayerId = skillContext.CurrentPlayerId;

        var monsterToSummon = targets.First();
        var summonEffect = new SummonEffect(View);

        var (boardPosition, replacedUnit) = boardManager.GetPreparedSummonData(currentPlayerId);
        var placement = new SummonPlacement(boardPosition, replacedUnit);

        var formation = new PlayerBoardFormation(
            boardManager.SelectMutableBoard(currentPlayerId),
            boardManager.GetReserveUnitsForPlayer(currentPlayerId)
        );

        summonEffect.ApplySamuraiSummon(monsterToSummon, formation, placement);
        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, replacedUnit);

        var turnChange = turnManager.ConsumeNeutralTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
}