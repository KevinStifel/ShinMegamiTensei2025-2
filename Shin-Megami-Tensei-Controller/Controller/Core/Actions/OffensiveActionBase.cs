using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class OffensiveActionBase : CombatActionBase
{
    protected abstract AffinityElement Element { get; }

    protected OffensiveActionBase(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var attacker = turnManager.GetAttackerOnTurn();
        var enemyPlayerId = BattleHelper.GetEnemyPlayerId(currentPlayerId);

        var target = SelectTarget(attacker, boardManager.GetAliveUnits(enemyPlayerId));
        var affinityBehavior = CreateAffinityBehavior(target);
        var damage = CalculateDamage(attacker, affinityBehavior);

        affinityBehavior.ApplyEffect(attacker, target, damage);
        ShowAffinityOutcome(attacker, target, damage);        
        ApplyTurnEffect(turnManager, affinityBehavior);  
        HandleTargetDeath(boardManager, enemyPlayerId, target);
        HandleAttackerDeath(boardManager, currentPlayerId, attacker);
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
    
    private void ShowAffinityOutcome(UnitBase attacker, UnitBase target, int inflictedDamage)
    {
        var affinityView = CreateViewForAffinity(target);
        ActionView.ShowSeparator();
        affinityView.ShowAffinityReaction(attacker, target, inflictedDamage);
        affinityView.ShowHp(attacker, target);
    }
    private AffinityViewBase CreateViewForAffinity(UnitBase target)
    {
        var reaction = target.Affinity.GetAffinityReaction(Element);
        var behaviorType = AffinityBehaviorFactory.Create(reaction).Type;
        return AffinityViewFactory.Create(behaviorType, View, Element);
    }

    private void ApplyTurnEffect(TurnManager turns, AffinityBehavior behavior)
    {
        var turnChange = turns.ApplyAffinityTurnEffect(behavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private static void HandleTargetDeath(BoardManager board, int enemyPlayerId, UnitBase target)
    {
        if (target.Stats.HP <= 0)
            board.HandleUnitDeath(enemyPlayerId, target);
    }

    private static void HandleAttackerDeath(BoardManager board, int currentPlayerId, UnitBase attacker)
    {
        if (attacker.Stats.HP <= 0)
            board.HandleUnitDeath(currentPlayerId, attacker);
    }
}
