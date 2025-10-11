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
        foreach (var targetUnit in targets)
        {
            int healAmount = HealCalculator.CalculateHealAmount(targetUnit, skillExecutionContext.SkillData);
            targetUnit.Stats.Heal(healAmount);
            EffectView.ShowHealEffect(casterUnit, targetUnit, healAmount);
        }
        ApplyTurnChange(skillExecutionContext.TurnManager);
    }
}