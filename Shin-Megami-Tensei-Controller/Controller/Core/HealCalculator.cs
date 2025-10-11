namespace Shin_Megami_Tensei;

public static class HealCalculator
{

    public static int CalculateHealAmount(UnitBase targetUnit, SkillData skillData)
    {
        const double percentageBase = 100.0;
        double healPercentage = skillData.Power / percentageBase;
        double healValue = targetUnit.Stats.MaxHP * healPercentage;
        int healAmount = (int)healValue;

        return healAmount;
    }
}