using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        var boardManager = skillExecutionContext.BoardManager;
        var turnManager = skillExecutionContext.TurnManager;
        int currentPlayerId = skillExecutionContext.CurrentPlayerId;

        var monsterToSummon = targets.First();
        var summonEffect = new SummonEffect(View);

        var (boardPosition, replacedUnit) = boardManager.GetPreparedSummonData(currentPlayerId);
        var placement = new SummonPlacement(boardPosition, replacedUnit);

        var formation = new PlayerBoardFormation(
            boardManager.SelectMutableBoard(currentPlayerId),
            boardManager.GetReserveUnitsForPlayer(currentPlayerId)
        );

        summonEffect.ApplySamuraiSummon(monsterToSummon, formation, placement);
        turnManager.UpdateOrderAfterSummon(casterUnit, monsterToSummon, replacedUnit);

        ApplyTurnChange(turnManager);
    }
}