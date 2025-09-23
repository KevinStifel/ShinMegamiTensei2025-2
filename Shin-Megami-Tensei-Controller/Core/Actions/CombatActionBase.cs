using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public abstract class CombatActionBase : ICombatAction
    {
        protected readonly CombatActionView _actionView;

        protected CombatActionBase(View view)
        {
            _actionView = new CombatActionView(view);
        }

        public abstract void ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager);
        
        protected static int GetEnemyPlayerId(int currentPlayerId) => currentPlayerId == 1 ? 2 : 1;

        protected UnitBase SelectEnemyTeamUnit(UnitBase attackerOnTurn, Board board, int enemyPlayerId)
        {
            List<UnitBase> enemyTeamAliveUnits = board.GetAliveUnits(enemyPlayerId);
            int selectedIndex = _actionView.ReadEnemyTargetIndex(attackerOnTurn, enemyTeamAliveUnits);
            return enemyTeamAliveUnits[selectedIndex];
        }

        protected static int ApplyDamage(UnitBase target, int damage)
        {
            target.Stats.TakeDamage(damage);
            return damage;
        }

        protected static void HandleDeathIfNeeded(Board board, int enemyPlayerId, UnitBase target)
        {
            if (target.Stats.HP == 0)
            {
                board.HandleUnitDeath(enemyPlayerId, target);
            }
        }
    }
}