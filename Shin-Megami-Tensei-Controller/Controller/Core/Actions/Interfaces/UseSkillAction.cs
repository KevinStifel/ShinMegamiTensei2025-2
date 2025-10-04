using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public class UseSkillAction : CombatActionBase
    {
        public UseSkillAction(View view) : base(view) { }

        public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
        {
            var casterOnTurn = turnManager.GetAttackerOnTurn();

            IReadOnlyList<Skill> selectableSkills = GetSelectableSkills(casterOnTurn);
            int selectedSkillIndex = _actionView.ReadSkillIndex(casterOnTurn, selectableSkills);

            if (WasCanceledSelection(selectedSkillIndex))
                throw new ActionCanceledException();

            // E1: aún no ejecutamos habilidades → por ahora no consume turnos
            throw new ActionCanceledException(); // seguimos en el menú
        }

        private static IReadOnlyList<Skill> GetSelectableSkills(UnitBase caster)
        {
            var allSkills = caster is Samurai s ? s.Skills : ((Monster)caster).Skills;
            return allSkills.Where(sk => sk.Cost <= caster.Stats.MP).ToList();
        }
    }
}