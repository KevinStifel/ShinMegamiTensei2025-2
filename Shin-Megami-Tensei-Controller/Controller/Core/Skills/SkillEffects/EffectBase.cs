using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly AffinityBehavior Behavior;
    protected readonly EffectView EffectView;
    protected readonly View View;

    // 🔹 Nuevo: contador interno para multi-hits
    private int _remainingHits;

    protected EffectBase(AffinityBehavior behavior, View view)
    {
        Behavior = behavior;
        EffectView = new EffectView(view);
        View = view;
    }
    public void SetRemainingHits(int hits)
    {
        _remainingHits = hits;
    }
    protected bool IsLastHit() => _remainingHits == 1;
    protected void DecrementHit() => _remainingHits = Math.Max(0, _remainingHits - 1);
    public abstract void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData);
}