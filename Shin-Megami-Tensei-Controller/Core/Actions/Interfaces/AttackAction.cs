using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class AttackAction : CombatActionBase
    {
        public AttackAction(View view) : base(view) { }

        public override void ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var attackerOnTurn = turnManager.GetAttackerOnTurn();
            int enemyPlayerId = GetEnemyPlayerId(currentPlayerId);

            List<UnitBase> enemyTeamAliveUnits = board.GetAliveUnits(enemyPlayerId);
            int selectedEnemyIndex = SelectEnemyTeamUnitIndex(attackerOnTurn, enemyTeamAliveUnits);
            
            if (WasCanceledSelection(selectedEnemyIndex))
            {
                throw new ActionCanceledException();
            }

            var selectedEnemyTeamUnit = enemyTeamAliveUnits[selectedEnemyIndex];

            int damage = DamageCalculator.CalculatePhysicalDamage(attackerOnTurn);
            ApplyDamage(selectedEnemyTeamUnit, damage);
            HandleDeathIfNeeded(board, enemyPlayerId, selectedEnemyTeamUnit);

            _actionView.ShowAttackResult(attackerOnTurn, selectedEnemyTeamUnit, damage);

            turnManager.ApplyTurnDelta(consumeFull: 1, consumeBlinking: 0, gainBlinking: 0);
            _actionView.ShowTurnConsumption(consumedFull: 1, consumedBlinking: 0, gainedBlinking: 0);
        }
    }
}