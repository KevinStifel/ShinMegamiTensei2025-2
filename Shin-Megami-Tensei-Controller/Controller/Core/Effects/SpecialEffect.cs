using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        BattleFlowContext battleFlowContext)
    {
        var boardManager = battleFlowContext.BoardManager;
        var turnManager = battleFlowContext.TurnManager;
        int currentPlayerId = battleFlowContext.CurrentPlayerId;

        var summonEffect = new SummonEffect(View);
        var (chosenPosition, occupant) = boardManager.GetPreparedSummonData(currentPlayerId);
        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetReserveUnitsForPlayer(currentPlayerId);
        var monsterToSummon = targets[0];

        summonEffect.ApplySamuraiSummon(monsterToSummon, chosenPosition, occupant, playerBoard, reserveUnits);

        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, occupant);

        var turnChange = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(turnChange);
    }
}