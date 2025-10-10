using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class UseSkillAction : CombatActionBase
{
    public UseSkillAction(View view) : base(view) { }

    public override void ExecuteAction(BattleFlowContext battleFlowContext)
    {
        var casterUnit = battleFlowContext.TurnManager.GetAttackerOnTurn();
        var selectedSkill = PromptSkillSelection(casterUnit);

        if (selectedSkill == null)
            throw new ActionCanceledException();

        ValidateManaAvailability(casterUnit, selectedSkill);

        var skillInstance = CreateSkillInstance(selectedSkill, battleFlowContext);
        ApplySkill(skillInstance, casterUnit, battleFlowContext);

        ConsumeMana(casterUnit, selectedSkill);
    }

    // 🔹 1. Mostrar y seleccionar habilidad
    private SkillData? PromptSkillSelection(UnitBase casterUnit)
    {
        var availableSkills = GetUsableSkills(casterUnit);
        ActionView.ShowAvailableSkills(casterUnit, availableSkills);

        int selectedIndex = ActionView.ReadSkillIndexFromInput(availableSkills);
        return WasCanceledSelection(selectedIndex) ? null : availableSkills[selectedIndex];
    }

    // 🔹 2. Obtener habilidades utilizables según MP
    private static IReadOnlyList<SkillData> GetUsableSkills(UnitBase casterUnit)
    {
        var allSkills = casterUnit is Samurai samurai
            ? samurai.Skills
            : ((Monster)casterUnit).Skills;

        return allSkills.Where(skill => skill.Cost <= casterUnit.Stats.MP).ToList();
    }

    // 🔹 3. Validar MP antes de usar la habilidad
    private static void ValidateManaAvailability(UnitBase casterUnit, SkillData selectedSkill)
    {
        if (casterUnit.Stats.MP < selectedSkill.Cost)
            throw new ActionCanceledException();
    }

    // 🔹 4. Crear instancia de habilidad
    private static Skill CreateSkillInstance(SkillData selectedSkill, BattleFlowContext battleFlowContext)
    {
        return SkillFactory.Create(selectedSkill, battleFlowContext.BoardManager, battleFlowContext.View);
    }

    // 🔹 5. Aplicar la habilidad seleccionada
    private static void ApplySkill(Skill skillInstance, UnitBase casterUnit, BattleFlowContext battleFlowContext)
    {
        skillInstance.Apply(casterUnit, battleFlowContext);
    }

    // 🔹 6. Consumir MP tras usar la habilidad
    private static void ConsumeMana(UnitBase casterUnit, SkillData selectedSkill)
    {
        UnitStatsManager.ConsumeMP(casterUnit, selectedSkill.Cost);
    }
}
