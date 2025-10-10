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

    public void Apply(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var casterUnit = turnManager.GetAttackerOnTurn();
        var skillExecutionContext = new SkillExecutionContext(_skillData, boardManager, turnManager, currentPlayerId);
        List<UnitBase> targets = _targetSelector.SelectTargets(casterUnit, currentPlayerId, _skillData);

        if (targets.Count == 0)
            throw new ActionCanceledException();

        boardManager.RegisterPlayerSkillCounter(currentPlayerId);
        boardManager.IncrementSkillUseCount(currentPlayerId);

        _effect.ApplyEffect(casterUnit, targets, skillExecutionContext);
    }
}