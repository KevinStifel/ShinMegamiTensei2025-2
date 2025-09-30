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
        
        protected static bool WasCanceledSelection(int selectedIndex) => selectedIndex < 0;
        
        protected int SelectEnemyTeamUnitIndex(UnitBase attackerOnTurn, List<UnitBase> enemyTeamAliveUnits)
        {
            var selectedIndex = _actionView.ReadEnemyTargetIndex(attackerOnTurn, enemyTeamAliveUnits);
            return selectedIndex;
        }

        protected static void ApplyDamage(UnitBase target, int damage)
        {
            target.Stats.TakeDamage(damage);
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