using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class OffensiveActionBase : CombatActionBase
{
    protected abstract AffinityElement Element { get; }

    protected OffensiveActionBase(View view) : base(view) { }

    public override void ExecuteAction(BattleFlowContext battleFlowContext)
    {
        var attackerUnit = battleFlowContext.TurnManager.GetAttackerOnTurn();
        var enemyPlayerId = BattleHelper.GetEnemyPlayerId(battleFlowContext.CurrentPlayerId);

        var targetUnit = SelectTarget(attackerUnit, battleFlowContext.BoardManager.GetAliveUnits(enemyPlayerId));
        var affinityBehavior = CreateAffinityBehavior(targetUnit);
        var inflictedDamage = CalculateDamage(attackerUnit, affinityBehavior);

        ApplyAndShowAffinityEffect(attackerUnit, targetUnit, affinityBehavior, inflictedDamage, battleFlowContext);
        ApplyAndShowTurnEffect(battleFlowContext.TurnManager, affinityBehavior);
        HandleDeaths(battleFlowContext.BoardManager, battleFlowContext.CurrentPlayerId, enemyPlayerId, attackerUnit, targetUnit);
    }

    private UnitBase SelectTarget(UnitBase attackerUnit, List<UnitBase> enemyUnits)
    {
        int selectedIndex = SelectEnemyTeamUnitIndex(attackerUnit, enemyUnits);
        if (WasCanceledSelection(selectedIndex))
            throw new ActionCanceledException();

        return enemyUnits[selectedIndex];
    }

    private AffinityBehavior CreateAffinityBehavior(UnitBase targetUnit)
    {
        var reaction = targetUnit.Affinity.GetAffinityReaction(Element);
        return AffinityBehaviorFactory.Create(reaction);
    }

    private int CalculateDamage(UnitBase attackerUnit, AffinityBehavior affinityBehavior)
    {
        return DamageCalculator.CalculateFinalDamage(attackerUnit, affinityBehavior, Element);
    }

    private void ApplyAndShowAffinityEffect(
        UnitBase attackerUnit,
        UnitBase targetUnit,
        AffinityBehavior affinityBehavior,
        int inflictedDamage,
        BattleFlowContext battleFlowContext)
    {
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, battleFlowContext.View, Element);

        affinityBehavior.ApplyEffect(attackerUnit, targetUnit, inflictedDamage);
        ShowAffinityOutcome(attackerUnit, targetUnit, affinityView, inflictedDamage);
    }

    private void ShowAffinityOutcome(
        UnitBase attackerUnit,
        UnitBase targetUnit,
        AffinityViewBase affinityView,
        int inflictedDamage
        )
    {
        ActionView.ShowSeparator();
        affinityView.ShowAffinityReaction(attackerUnit, targetUnit, inflictedDamage);
        affinityView.ShowHp(attackerUnit, targetUnit);
    }

    private void ApplyAndShowTurnEffect(TurnManager turnManager, AffinityBehavior affinityBehavior)
    {
        var turnChange = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private static void HandleDeaths(
        BoardManager boardManager,
        int currentPlayerId,
        int enemyPlayerId,
        UnitBase attackerUnit,
        UnitBase targetUnit)
    {
        HandleDeathIfNeeded(boardManager, enemyPlayerId, targetUnit);
        if (attackerUnit.Stats.HP <= 0)
            boardManager.HandleUnitDeath(currentPlayerId, attackerUnit);
    }
}
