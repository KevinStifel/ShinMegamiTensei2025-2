using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class AttackAction : ICombatAction
    {
        private readonly CombatActionView _actionView;
        private readonly View _view;

        public AttackAction(View view)
        {
            _actionView = new CombatActionView(view);
            _view = view;
        }

        public void Execute(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var unitTakingTurn = turnManager.AttackOrder.First();

            int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
            var enemyUnitsOnBoard = board.GetAliveUnits(enemyPlayerId);

            int selectedEnemyIndex = _actionView.ReadEnemyTargetIndex(unitTakingTurn, enemyUnitsOnBoard);
            var targetUnit = enemyUnitsOnBoard[selectedEnemyIndex];

            int damageDealt = DamageCalculator.CalculatePhysicalDamage(unitTakingTurn);
            targetUnit.Stats.TakeDamage(damageDealt);

            _actionView.ShowAttackResult(unitTakingTurn, targetUnit, damageDealt);

            turnManager.ApplyTurnDelta(1, 0, 0);
            _actionView.ShowTurnConsumption(1, 0, 0);
        }

    }
}