using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class ReviveEffect : EffectBase
{
    public ReviveEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        foreach (var target in targets)
        {
            if (IsTargetAlive(target))
                continue;

            int healAmount = HealCalculator.CalculateHealAmount(target, skillExecutionContext.SkillData);

            target.Stats.Heal(healAmount);
            EffectView.ShowReviveEffect(casterUnit, target, healAmount);
        }

        ApplyTurnChange(skillExecutionContext.TurnManager);
    }

    private static bool IsTargetAlive(UnitBase target)
    {
        return target.Stats.HP > 0;
    }
}