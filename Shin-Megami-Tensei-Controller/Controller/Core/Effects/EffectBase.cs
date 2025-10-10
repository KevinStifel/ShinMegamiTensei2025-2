using System;
using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly EffectView EffectView;
    protected readonly View View;

    private int _remainingHits;

    protected EffectBase(View view)
    {
        View = view;
        EffectView = new EffectView(view);
    }

    public abstract void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        BattleFlowContext battleFlowContext);
}