using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class OffensiveActionBase : CombatActionBase
{
    protected abstract AffinityElement Element { get; }

    protected OffensiveActionBase(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var attackerUnit = turnManager.GetAttackerOnTurn();
        var enemyPlayerId = BattleHelper.GetEnemyPlayerId(currentPlayerId);

        var targetEnemyUnit = SelectTarget(attackerUnit, boardManager.GetAliveUnits(enemyPlayerId));
        var affinityBehavior = CreateAffinityBehavior(targetEnemyUnit);
        var damage = CalculateDamage(attackerUnit, affinityBehavior);

        affinityBehavior.ApplyEffect(attackerUnit, targetEnemyUnit, damage);
        ShowAffinityOutcome(attackerUnit, targetEnemyUnit, damage);        
        ApplyTurnEffect(turnManager, affinityBehavior);  
        HandleUnitDeath(boardManager, enemyPlayerId, targetEnemyUnit);
        HandleUnitDeath(boardManager, currentPlayerId, attackerUnit);
    }

    private UnitBase SelectTarget(UnitBase attackerUnit, List<UnitBase> enemyUnits)
    {
        int selectedIndex = SelectEnemyTeamUnitIndex(attackerUnit, enemyUnits);
        if (IsSelectionCanceled(selectedIndex))
            throw new ActionCanceledException();

        return enemyUnits[selectedIndex];
    }

    private AffinityBehavior CreateAffinityBehavior(UnitBase targetEnemyUnit)
    {
        var reaction = targetEnemyUnit.Affinity.GetAffinityReaction(Element);
        return AffinityBehaviorFactory.Create(reaction);
    }

    private int CalculateDamage(UnitBase attackerUnit, AffinityBehavior affinityBehavior)
    {
        return DamageCalculator.CalculateFinalDamage(attackerUnit, affinityBehavior, Element);
    }
    
    private void ShowAffinityOutcome(UnitBase attackerUnit, UnitBase targetEnemyUnit, int inflictedDamage)
    {
        var affinityView = CreateAffinityView(targetEnemyUnit);
        ActionView.ShowSeparator();
        affinityView.ShowAffinityReaction(attackerUnit, targetEnemyUnit, inflictedDamage);
        affinityView.ShowHp(attackerUnit, targetEnemyUnit);
    }
    private AffinityViewBase CreateAffinityView(UnitBase targetEnemyUnit)
    {
        var affinityReaction = targetEnemyUnit.Affinity.GetAffinityReaction(Element);
        var affinityBehaviorType = AffinityBehaviorFactory.Create(affinityReaction).Type;
        return AffinityViewFactory.Create(affinityBehaviorType, View, Element);
    }

    private void ApplyTurnEffect(TurnManager turns, AffinityBehavior affinityBehavior)
    {
        var turnChange = turns.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }
    
    private static void HandleUnitDeath(BoardManager board, int playerId, UnitBase unit)
    {
        if (IsUnitDead(unit))
            board.HandleUnitDeath(playerId, unit);
    }
    private static bool IsUnitDead(UnitBase unit)
    {
        return unit.Stats.HP <= 0;
    }
}
