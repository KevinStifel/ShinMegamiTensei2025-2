using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Skill
{
    private readonly SkillData _skillData;
    private readonly EffectBase _effect;
    private readonly TargetSelectorBase _targetSelector;

    public Skill(SkillData skillData, EffectBase effect, TargetSelectorBase targetSelector)
    {
        _skillData = skillData;
        _effect = effect;
        _targetSelector = targetSelector;
    }

    public void Apply(UnitBase caster, BattleFlowContext battleFlowContext)
    {
        List<UnitBase> targets = _targetSelector.SelectTargets(
            caster,
            battleFlowContext.CurrentPlayerId,
            _skillData);

        if (targets == null || targets.Count == 0)
            throw new ActionCanceledException();
        
        battleFlowContext.BoardManager.RegisterPlayerSkillCounter(battleFlowContext.CurrentPlayerId);
        battleFlowContext.BoardManager.IncrementSkillUseCount(battleFlowContext.CurrentPlayerId);

        _effect.ApplyEffect(caster, targets, _skillData, battleFlowContext);
    }
}