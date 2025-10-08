using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public abstract class CombatActionBase : ICombatAction
    {
        protected readonly CombatActionView ActionView;
        protected readonly View View;

        protected CombatActionBase(View view)
        {
            ActionView = new CombatActionView(view);
            View = view;
        }

        public abstract void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager);
        
        protected static int GetEnemyPlayerId(int currentPlayerId) => currentPlayerId == 1 ? 2 : 1;
        
        protected static bool WasCanceledSelection(int selectedIndex) => selectedIndex < 0;
        
        protected static void ApplyDamage(UnitBase target, int damage)
        {
            target.Stats.TakeDamage(damage);
        }

        protected static void Heal(UnitBase target, int heal)
        {
            target.Stats.Heal(heal);
        }
        
        protected int SelectEnemyTeamUnitIndex(UnitBase attacker, List<UnitBase> enemyUnits)
        {
            ActionView.ShowAvailableTargets(attacker, enemyUnits);

            var input = ActionView.ReadUserSelection();
            if (!int.TryParse(input, out int index))
                return -1;

            index -= 1;
            return index >= 0 && index < enemyUnits.Count ? index : -1;
        }


        protected static void HandleDeathIfNeeded(BoardManager board, int enemyPlayerId, UnitBase target)
        {
            if (target.Stats.HP == 0)
            {
                board.HandleUnitDeath(enemyPlayerId, target);
            }
        }

        protected string GetElementalMessage(AffinityElement element)
        {
            return element switch
            {
                AffinityElement.Physical => "ataca",
                AffinityElement.Gun => "dispara",
                AffinityElement.Fire => "lanza fuego",
                AffinityElement.Ice => "lanza hielo",
                AffinityElement.Elec => "lanza electricidad",
                AffinityElement.Force => "lanza viento",
                AffinityElement.Light => "ataca con luz",
                AffinityElement.Dark => "ataca con oscuridad",
                _ => "usa una habilidad"
            };
        }
    }
}