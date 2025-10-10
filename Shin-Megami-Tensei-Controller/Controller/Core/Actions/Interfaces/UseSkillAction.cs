using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class UseSkillAction : CombatActionBase
{
    public UseSkillAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
    {
        var caster = turnManager.GetAttackerOnTurn();
        var selectedSkill = PromptSkillSelection(caster);
        Console.WriteLine(selectedSkill);

        if (selectedSkill == null)
            throw new ActionCanceledException();

        if (caster.Stats.MP < selectedSkill.Cost)
            throw new ActionCanceledException();
        
        // Crear la skill
        var skillInstance = SkillFactory.Create(selectedSkill, board, View);
        
        skillInstance.Apply(caster, currentPlayerId, board, turnManager);
        UnitStatsManager.ConsumeMP(caster, selectedSkill.Cost);

        
        
    }
    private SkillData? PromptSkillSelection(UnitBase caster)
    {
        var available = GetUsableSkills(caster);
        ActionView.ShowAvailableSkills(caster, available);

        int index = ActionView.ReadSkillIndexFromInput(available);
        return WasCanceledSelection(index) ? null : available[index];
    }

    private static IReadOnlyList<SkillData> GetUsableSkills(UnitBase caster)
    {
        var allSkills = caster is Samurai s ? s.Skills : ((Monster)caster).Skills;
        return allSkills.Where(skill => skill.Cost <= caster.Stats.MP).ToList();
    } 
}
