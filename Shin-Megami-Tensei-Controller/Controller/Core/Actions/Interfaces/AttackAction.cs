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
            var affinityReaction = targetTeamUnit.Affinity.GetReaction(AffinityElement.Physical);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);

            // 2️⃣ Calcular daño
            int finalDamage = DamageCalculator.CalculateFinalDamage(attacker, affinityBehavior, AffinityElement.Physical);

            // 3️⃣ Aplicar daño o curación
            if (finalDamage < 0)
                targetTeamUnit.Stats.Heal(-finalDamage);
            else
                targetTeamUnit.Stats.TakeDamage(finalDamage);
            
            // 4️⃣ Mostrar resultado visual
            string actionVerb = GetElementalMessage(AffinityElement.Physical);
            // Mostrar encabezado del ataque y afinidad
            _actionView.ShowAttackIntro(attacker, targetTeamUnit, actionVerb, affinityReaction);

            // Mostrar resultado del ataque
            _actionView.ShowAttackOutcome(targetTeamUnit, finalDamage);



            // 6️⃣ Aplicar turnos según afinidad
            var delta = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
            _actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);

            // 7️⃣ Revisar muerte
            HandleDeathIfNeeded(board, enemyPlayerId, targetTeamUnit);
        }
        
    }
}