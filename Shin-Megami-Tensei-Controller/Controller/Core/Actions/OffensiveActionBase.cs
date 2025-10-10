using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

/// <summary>
/// Clase base para acciones ofensivas (físicas, de arma o mágicas).
/// Cumple con Clean Code: funciones pequeñas, nombres claros y SRP.
/// </summary>
public abstract class OffensiveActionBase : CombatActionBase
{
    protected abstract AffinityElement Element { get; }

    protected OffensiveActionBase(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var attackingUnit = turnManager.GetAttackerOnTurn();
        var enemyPlayerId = GetEnemyPlayerId(currentPlayerId);
        var targetUnit = SelectTarget(attackingUnit, boardManager.GetAliveUnits(enemyPlayerId));

        var affinityBehavior = CreateAffinityBehavior(targetUnit);
        var inflictedDamage = CalculateDamage(attackingUnit, affinityBehavior);
        ApplyAndShowAffinityEffect(attackingUnit, targetUnit, affinityBehavior, inflictedDamage);
        ApplyAndShowTurnEffect(turnManager, affinityBehavior);
        HandleDeaths(boardManager, currentPlayerId, enemyPlayerId, attackingUnit, targetUnit);
    }

    private UnitBase SelectTarget(UnitBase attackingUnit, List<UnitBase> enemies)
    {
        int index = SelectEnemyTeamUnitIndex(attackingUnit, enemies);
        if (WasCanceledSelection(index))
            throw new ActionCanceledException();

        return enemies[index];
    }

    private AffinityBehavior CreateAffinityBehavior(UnitBase targetUnit)
    {
        var affinityReaction = targetUnit.Affinity.GetAffinityReaction(Element);
        return AffinityBehaviorFactory.Create(affinityReaction);
    }

    private int CalculateDamage(UnitBase attackingUnit, AffinityBehavior affinityBehavior)
    {
        return DamageCalculator.CalculateFinalDamage(attackingUnit, affinityBehavior, Element);
    }

    private void ApplyAndShowAffinityEffect(UnitBase attackingUnit, UnitBase targetUnit, AffinityBehavior affinityBehavior, int inflictedDamage)
    {
        string verb = GetElementalMessage(Element);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);
        affinityBehavior.ApplyEffect(attackingUnit, targetUnit, inflictedDamage);
        ShowAffinityResult(attackingUnit, targetUnit, affinityView, inflictedDamage, verb);
    }

    private void ShowAffinityResult(UnitBase attackingUnit, UnitBase targetUnit, AffinityViewBase affinityView, int inflictedDamage, string verb)
    {
        ActionView.ShowSeparator();
        affinityView.ShowAffinityReaction(attackingUnit, targetUnit, inflictedDamage, verb);
        affinityView.ShowHp(attackingUnit, targetUnit);
    }

    private void ApplyAndShowTurnEffect(TurnManager turnManager, AffinityBehavior affinity)
    {
        var turnChange = turnManager.ApplyAffinityTurnEffect(affinity);
        ActionView.ShowTurnConsumption(turnChange.ConsumedFull, turnChange.ConsumedBlinking, turnChange.GainedBlinking);
    }

    private static void HandleDeaths(BoardManager boardManager, int currentPlayerId, int enemyPlayerId, UnitBase attackingUnit, UnitBase targetUnit)
    {
        HandleDeathIfNeeded(boardManager, enemyPlayerId, targetUnit);
        if (attackingUnit.Stats.HP <= 0)
            boardManager.HandleUnitDeath(currentPlayerId, attackingUnit);
    }
}
