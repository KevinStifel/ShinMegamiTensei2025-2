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
        foreach (var targetUnit in targets)
        {
            if (IsTargetAlive(targetUnit))
                continue;

            int healAmount = HealCalculator.CalculateHealAmount(targetUnit, skillExecutionContext.SkillData);

            targetUnit.Stats.Heal(healAmount);
            EffectView.ShowReviveEffect(casterUnit, targetUnit, healAmount);
        }

        ApplyTurnChange(skillExecutionContext.TurnManager);
    }

    private static bool IsTargetAlive(UnitBase targetUnit)
    {
        return targetUnit.Stats.HP > 0;
    }
}