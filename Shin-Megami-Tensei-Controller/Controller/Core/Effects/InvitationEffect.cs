using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class InvitationEffect : EffectBase
{
    public InvitationEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        BattleFlowContext battleFlowContext)
    {
        var boardManager = battleFlowContext.BoardManager;
        var turnManager = battleFlowContext.TurnManager;
        int currentPlayerId = battleFlowContext.CurrentPlayerId;

        var monsterToSummon = targets[0];
        var (chosenPosition, occupant) = boardManager.GetPreparedSummonData(currentPlayerId);
        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetReserveUnitsForPlayer(currentPlayerId);

        playerBoard[chosenPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);
        if (occupant != null)
            reserveUnits.Insert(0, occupant);

        if (monsterToSummon.Stats.HP == 0)
        {
            int healAmount = monsterToSummon.Stats.MaxHP;
            monsterToSummon.Stats.Heal(healAmount);
            EffectView.ShowSummonAndReviveEffect(caster, monsterToSummon, healAmount);
        }
        else
        {
            EffectView.ShowSummonResult(monsterToSummon);
        }

        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, occupant);

        var turnChange = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(turnChange);
    }
}