using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class ShootAction : CombatActionBase
    {
        public ShootAction(View view) : base(view) { }

        public override void ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var attackerOnTurn = turnManager.GetAttackerOnTurn();
            int enemyPlayerId = GetEnemyPlayerId(currentPlayerId);

            var selectedEnemyTeamUnit = SelectEnemyTeamUnit(attackerOnTurn, board, enemyPlayerId);

            int damage = DamageCalculator.CalculateGunDamage(attackerOnTurn);
            int gunDamageDealt = ApplyDamage(selectedEnemyTeamUnit, damage);

            HandleDeathIfNeeded(board, enemyPlayerId, selectedEnemyTeamUnit);

            _actionView.ShowShootResult(attackerOnTurn, selectedEnemyTeamUnit, gunDamageDealt);

            turnManager.ApplyTurnDelta(consumeFull: 1, consumeBlinking: 0, gainBlinking: 0);
            _actionView.ShowTurnConsumption(consumedFull: 1, consumedBlinking: 0, gainedBlinking: 0);
        }
    }
}
