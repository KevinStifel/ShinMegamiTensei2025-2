namespace Shin_Megami_Tensei;

public static class DamageCalculator
{
    private static int CalculateBaseDamage(int statValue, int modifier)
    {
        return (int)(statValue * modifier * GameConstants.BaseDamageModifier);
    }

    private static int CalculatePhysicalDamage(UnitBase attacker)
        => CalculateBaseDamage(attacker.Stats.Str, GameConstants.PhysicalDamageModifier);

    private static int CalculateGunDamage(UnitBase attacker)
        => CalculateBaseDamage(attacker.Stats.Skl, GameConstants.GunDamageModifier);

    private static int CalculateMagicDamage(UnitBase attacker, int skillPower)
        => (int)Math.Sqrt(attacker.Stats.Mag * skillPower);

    private static int CalculateAffinityDamage(int baseDamage, AffinityBehavior affinityBehavior)
    {
        Console.WriteLine(baseDamage);
        Console.WriteLine(affinityBehavior);
        Console.WriteLine(affinityBehavior.ModifyDamage(baseDamage));
        return affinityBehavior.ModifyDamage(baseDamage);
    }

    public static int CalculateFinalDamage(UnitBase attacker, AffinityBehavior affinityBehavior, AffinityElement element)
    {
        int baseDamage = element switch
        {
            AffinityElement.Physical => CalculatePhysicalDamage(attacker),
            AffinityElement.Gun => CalculateGunDamage(attacker),
            _ => CalculatePhysicalDamage(attacker)
        };
        

        return CalculateAffinityDamage(baseDamage, affinityBehavior);
    }
}