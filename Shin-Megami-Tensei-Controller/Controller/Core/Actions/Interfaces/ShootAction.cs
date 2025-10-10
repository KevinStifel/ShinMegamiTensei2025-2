using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class ShootAction : CombatActionBase
    {
        public ShootAction(View view) : base(view) { }

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
            var affinityReaction = targetTeamUnit.Affinity.GetAffinityReaction(AffinityElement.Gun);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            
            string verb = GetElementalMessage(AffinityElement.Gun);

            // 2️⃣ Calcular daño
            int finalDamage = DamageCalculator.CalculateFinalDamage(attacker, affinityBehavior, AffinityElement.Gun);
            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);
            
            affinityBehavior.ApplyEffect(attacker, targetTeamUnit, finalDamage);
            ActionView.ShowSeparator();
            affinityView.ShowAffinityReaction(attacker, targetTeamUnit, finalDamage, verb);
            affinityView.ShowHp(attacker, targetTeamUnit);
            
            // 6️⃣ Aplicar turnos según afinidad
            var delta = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
            ActionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);

            // 7️⃣ Revisar muerte
            HandleDeathIfNeeded(board, enemyPlayerId, targetTeamUnit);
            if (attacker.Stats.HP <= 0)
            {
                board.HandleUnitDeath(currentPlayerId, attacker);
            }
        }
    }
}
