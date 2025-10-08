using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class AttackAction : CombatActionBase
    {
        public AttackAction(View view) : base(view) { }

        public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
        {
            var attacker = turnManager.GetAttackerOnTurn();
            int enemyPlayerId = GetEnemyPlayerId(currentPlayerId);

            List<UnitBase> enemyUnits = board.GetAliveUnits(enemyPlayerId);
            int selectedIndex = SelectEnemyTeamUnitIndex(attacker, enemyUnits);

            if (WasCanceledSelection(selectedIndex))
                throw new ActionCanceledException();

            var targetTeamUnit = enemyUnits[selectedIndex];

            // 1️⃣ Determinar afinidad
            var affinityReaction = targetTeamUnit.Affinity.GetAffinityReaction(AffinityElement.Physical);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            
            string verb = GetElementalMessage(AffinityElement.Physical);
            ActionView.ShowAttackIntro(attacker, targetTeamUnit, verb, affinityReaction);

            int finalDamage = DamageCalculator.CalculateFinalDamage(attacker, affinityBehavior, AffinityElement.Physical);

            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);

            
            affinityBehavior.ApplyEffect(attacker, targetTeamUnit, finalDamage);
            affinityView.ShowAffinityReaction(attacker, targetTeamUnit, finalDamage);
            
            var delta = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
            ActionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);

            // 7️⃣ Revisar muerte
            HandleDeathIfNeeded(board, enemyPlayerId, targetTeamUnit);
        }
    }
}