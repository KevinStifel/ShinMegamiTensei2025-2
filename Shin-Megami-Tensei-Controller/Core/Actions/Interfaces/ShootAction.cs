using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class ShootAction : CombatActionBase
    {
        public ShootAction(View view) : base(view) { }

        public override ActionExecutionResult ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var attackerOnTurn = turnManager.GetAttackerOnTurn();
            int enemyPlayerId = GetEnemyPlayerId(currentPlayerId);

            List<UnitBase> enemyTeamAliveUnits = board.GetAliveUnits(enemyPlayerId);
            int selectedEnemyIndex = SelectEnemyTeamUnitIndex(attackerOnTurn, enemyTeamAliveUnits);
            if (WasCanceledSelection(selectedEnemyIndex))
            {
                return ActionExecutionResult.StayInMenu();
            }

            var selectedEnemyTeamUnit = enemyTeamAliveUnits[selectedEnemyIndex];

            int damage = DamageCalculator.CalculateGunDamage(attackerOnTurn);
            ApplyDamage(selectedEnemyTeamUnit, damage);
            HandleDeathIfNeeded(board, enemyPlayerId, selectedEnemyTeamUnit);

            _actionView.ShowShootResult(attackerOnTurn, selectedEnemyTeamUnit, damage);

            turnManager.ApplyTurnDelta(consumeFull: 1, consumeBlinking: 0, gainBlinking: 0);
            _actionView.ShowTurnConsumption(consumedFull: 1, consumedBlinking: 0, gainedBlinking: 0);

            return ActionExecutionResult.AdvanceTurn();
        }
    }
}