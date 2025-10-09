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

        int enemyPlayerId = GetEnemyPlayerId(currentPlayerId);
        var target = SelectTarget(caster, board, enemyPlayerId);
        if (target == null)
            throw new ActionCanceledException();

        UnitStatsManager.ConsumeMP(caster, selectedSkill.Cost);

        var element = AffinityMapper.Parse(selectedSkill.Type);
        var reaction = target.Affinity.GetAffinityReaction(element);
        var behavior = AffinityBehaviorFactory.Create(reaction);
        
        // Llamar a una factory para devolver vista asociada a la affinity
        var skillInstance = SkillFactory.Create(selectedSkill, behavior, View);
        
        ActionView.ShowSeparator();
        skillInstance.Apply(caster, target, currentPlayerId);
        
        var delta = turnManager.ApplyAffinityTurnEffect(behavior);
        ActionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);

        HandleDeathIfNeeded(board, enemyPlayerId, target);
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

    private UnitBase? SelectTarget(UnitBase caster, BoardManager board, int enemyPlayerId)
    {
        var enemies = board.GetAliveUnits(enemyPlayerId);
        int index = SelectEnemyTeamUnitIndex(caster, enemies);
        return WasCanceledSelection(index) ? null : enemies[index];
    }
}
