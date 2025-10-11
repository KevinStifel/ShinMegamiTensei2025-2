namespace Shin_Megami_Tensei;

public static class DamageCalculator
{
    public static int CalculateFinalDamage(UnitBase attacker, AffinityBehavior affinityBehavior, AffinityElement element)
    {
        double baseDamage = CalculateBaseDamageForElement(attacker, element);
        int finalDamage = ApplyAffinityModifier(baseDamage, affinityBehavior);
        return finalDamage;
    }

    public static int CalculateFinalDamageForSkill(UnitBase attacker, SkillData skillData, AffinityBehavior affinityBehavior)
    {
        var elementType = AffinityMapper.Parse(skillData.Type);
        double baseDamage = CalculateBaseDamageForSkill(attacker, skillData, elementType);
        int finalDamage = ApplyAffinityModifier(baseDamage, affinityBehavior);
        return finalDamage;
    }

    private static double CalculateBaseDamageForElement(UnitBase attacker, AffinityElement affinityElement)
    {
        return affinityElement switch
        {
            AffinityElement.Physical => CalculatePhysicalBaseDamage(attacker),
            AffinityElement.Gun => CalculateGunBaseDamage(attacker),
            _ => CalculatePhysicalBaseDamage(attacker)
        };
    }

    private static double CalculateBaseDamageForSkill(UnitBase attacker, SkillData skillData, AffinityElement affinityElement)
    {
        return affinityElement switch
        {
            AffinityElement.Physical => CalculateSquareRootDamage(attacker.Stats.Str, skillData.Power),
            AffinityElement.Gun => CalculateSquareRootDamage(attacker.Stats.Skl, skillData.Power),
            AffinityElement.Fire or
            AffinityElement.Ice or
            AffinityElement.Elec or
            AffinityElement.Force => CalculateSquareRootDamage(attacker.Stats.Mag, skillData.Power),
            _ => CalculateSquareRootDamage(attacker.Stats.Str, skillData.Power)
        };
    }

    private static double CalculatePhysicalBaseDamage(UnitBase attacker)
    {
        return CalculateStatBasedDamage(attacker.Stats.Str, GameConstants.PhysicalDamageModifier);
    }

    private static double CalculateGunBaseDamage(UnitBase attacker)
    {
        return CalculateStatBasedDamage(attacker.Stats.Skl, GameConstants.GunDamageModifier);
    }

    private static double CalculateStatBasedDamage(int statValue, int damageModifier)
    {
        return statValue * damageModifier * GameConstants.BaseDamageModifier;
    }

    private static double CalculateSquareRootDamage(int statValue, int skillPower)
    {
        return Math.Sqrt(statValue * skillPower);
    }

    private static int ApplyAffinityModifier(double baseDamage, AffinityBehavior affinityBehavior)
    {
        double modifiedDamage = affinityBehavior.ModifyDamage(baseDamage);
        return (int)Math.Floor(modifiedDamage);
    }
}
