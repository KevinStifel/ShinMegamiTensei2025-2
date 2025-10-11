using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class HealEffect : EffectBase
{
    public HealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        foreach (var target in targets)
        {
            int healAmount = HealCalculator.CalculateHealAmount(target, skillExecutionContext.SkillData);
            target.Stats.Heal(healAmount);
            EffectView.ShowHealEffect(casterUnit, target, healAmount);
        }
        ApplyTurnChange(skillExecutionContext.TurnManager);
    }
}