using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class UseSkillAction : CombatActionBase
{
    public UseSkillAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var casterUnit = turnManager.GetAttackerOnTurn();

        var selectedSkillData = PromptSkillSelection(casterUnit);
        if (selectedSkillData == null)
            throw new ActionCanceledException();

        ValidateManaAvailability(casterUnit, selectedSkillData);

        var skillInstance = CreateSkillInstance(selectedSkillData, boardManager);
        ApplySkill(skillInstance, casterUnit, currentPlayerId, boardManager, turnManager);

        ConsumeMana(casterUnit, selectedSkillData);
    }

    private SkillData? PromptSkillSelection(UnitBase casterUnit)
    {
        var availableSkills = GetUsableSkills(casterUnit);
        ActionView.ShowAvailableSkills(casterUnit, availableSkills);

        int selectedIndex = ActionView.ReadSkillIndexFromInput(availableSkills);
        return WasCanceledSelection(selectedIndex) ? null : availableSkills[selectedIndex];
    }

    private static IReadOnlyList<SkillData> GetUsableSkills(UnitBase casterUnit)
    {
        var allSkills = casterUnit is Samurai samurai
            ? samurai.Skills
            : ((Monster)casterUnit).Skills;

        return allSkills.Where(skill => skill.Cost <= casterUnit.Stats.MP).ToList();
    }

    private static void ValidateManaAvailability(UnitBase casterUnit, SkillData selectedSkillData)
    {
        if (casterUnit.Stats.MP < selectedSkillData.Cost)
            throw new ActionCanceledException();
    }

    private Skill CreateSkillInstance(SkillData selectedSkillData, BoardManager boardManager)
    {
        return SkillFactory.Create(selectedSkillData, boardManager, View);
    }

    private static void ApplySkill(Skill skillInstance, UnitBase casterUnit, int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        skillInstance.Apply(casterUnit, currentPlayerId, boardManager, turnManager);
    }

    // 🔹 Resta MP después de usar la habilidad
    private static void ConsumeMana(UnitBase casterUnit, SkillData selectedSkillData)
    {
        UnitStatsManager.ConsumeMP(casterUnit, selectedSkillData.Cost);
    }
}
