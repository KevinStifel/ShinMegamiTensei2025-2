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

    // 📊 Manejo de golpes múltiples
    public void SetRemainingHits(int hits)
    {
        _remainingHits = hits;
    }

    protected bool IsLastHit() => _remainingHits == 1;

    protected void DecrementHit()
    {
        _remainingHits = Math.Max(0, _remainingHits - 1);
    }

    public abstract void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillData skillData, TurnManager turnManager, int currentPlayerId, BoardManager board);
}