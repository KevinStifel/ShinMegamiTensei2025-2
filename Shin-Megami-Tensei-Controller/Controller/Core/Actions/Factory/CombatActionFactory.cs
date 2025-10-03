using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public class CombatActionFactory
    {
        private readonly View _view;

        public CombatActionFactory(View view)
        {
            _view = view;
        }

        public ICombatAction CreateAction(string choice)
        {
            return choice switch
            {
                "attack" => new AttackAction(_view),
                "shoot"  => new ShootAction(_view),
                "skill"  => new UseSkillAction(_view),
                "surrender" => new SurrenderAction(_view),
                "pass" => new PassTurnAction(_view),
                "summon" => new SummonAction(_view),
                _ => throw new ArgumentException("Opción de acción no válida")
            };
        }
    }
}