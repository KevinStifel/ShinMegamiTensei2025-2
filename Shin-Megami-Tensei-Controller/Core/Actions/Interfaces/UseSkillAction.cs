using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public class UseSkillAction : CombatActionBase
    {
        public UseSkillAction(View view) : base(view) { }

        public override ActionExecutionResult ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var casterOnTurn = turnManager.GetAttackerOnTurn();

            IReadOnlyList<Skill> selectableSkills = GetSelectableSkills(casterOnTurn);
            int selectedSkillIndex = _actionView.ReadSkillIndex(casterOnTurn, selectableSkills);

            if (WasCanceledSelection(selectedSkillIndex))
            {
                return ActionExecutionResult.StayInMenu();
            }

            // E1: aún no ejecutamos habilidades → no afecta turnos; nos quedamos en el menú.
            // (E2 hook: ejecutar chosenSkill y retornar AdvanceTurn o lo que corresponda)
            return ActionExecutionResult.NoEffect();
        }

        private static IReadOnlyList<Skill> GetSelectableSkills(UnitBase caster)
        {
            var allSkills = caster is Samurai s ? s.Skills : ((Monster)caster).Skills;
            return allSkills.Where(sk => sk.Cost <= caster.Stats.MP).ToList();
        }
    }
}