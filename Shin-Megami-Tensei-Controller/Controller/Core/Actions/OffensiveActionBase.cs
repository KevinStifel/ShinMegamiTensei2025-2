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
        HandleUnitDeath(boardManager, enemyPlayerId, target);
        HandleUnitDeath(boardManager, currentPlayerId, attacker);
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
        var affinityView = CreateAffinityView(target);
        ActionView.ShowSeparator();
        affinityView.ShowAffinityReaction(attacker, target, inflictedDamage);
        affinityView.ShowHp(attacker, target);
    }
    private AffinityViewBase CreateAffinityView(UnitBase target)
    {
        var affinityReaction = target.Affinity.GetAffinityReaction(Element);
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
