namespace Shin_Megami_Tensei;

public static class HealCalculator
{
    public static int CalculateHealAmount(UnitBase target, SkillData skillData)
    {
        double healPercentage = skillData.Power / 100.0;
        int healAmount = (int)(target.Stats.MaxHP * healPercentage);
        return healAmount;
    }
}