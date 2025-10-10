using System;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly EffectView EffectView;
    protected readonly View View;

    // 🔹 Contador interno para multi-hits
    private int _remainingHits;

    protected EffectBase(View view)
    {
        View = view;
        EffectView = new EffectView(view);
    }

    public abstract void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillData skillData, TurnManager turnManager, int currentPlayerId, BoardManager boardManager);
}