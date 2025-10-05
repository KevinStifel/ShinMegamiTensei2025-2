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

            // 1️⃣ Seleccionar objetivo enemigo
            List<UnitBase> enemyTeamAliveUnits = board.GetAliveUnits(enemyPlayerId);
            int selectedTargetIndex = SelectEnemyTeamUnitIndex(attacker, enemyTeamAliveUnits);

            if (WasCanceledSelection(selectedTargetIndex))
                throw new ActionCanceledException();

            var targetTeamUnit = enemyTeamAliveUnits[selectedTargetIndex];

            // 2️⃣ Obtener afinidad del objetivo respecto al ataque tipo "Gun"
            var affinityReaction = targetTeamUnit.Affinity.GetReaction(AffinityElement.Gun);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);

            // 3️⃣ Calcular daño final (daño base físico + efecto de afinidad)
            int finalDamage = DamageCalculator.CalculateFinalDamage(attacker, affinityBehavior, AffinityElement.Gun);

            // 4️⃣ Aplicar daño (o curación si el daño es negativo)
            if (finalDamage < 0)
                targetTeamUnit.Stats.Heal(-finalDamage); // daño negativo → cura
            else
                targetTeamUnit.Stats.TakeDamage(finalDamage);

            string actionVerb = GetElementalMessage(AffinityElement.Gun);
            
            // Mostrar encabezado del ataque y afinidad
            _actionView.ShowAttackIntro(attacker, targetTeamUnit, actionVerb, affinityReaction);

            // Mostrar resultado del ataque
            _actionView.ShowAttackOutcome(targetTeamUnit, finalDamage);



            // 6️⃣ Aplicar efecto en turnos según la afinidad
            var delta = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
            _actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);

            // 7️⃣ Verificar si el objetivo murió
            HandleDeathIfNeeded(board, enemyPlayerId, targetTeamUnit);
        }
    }
}
