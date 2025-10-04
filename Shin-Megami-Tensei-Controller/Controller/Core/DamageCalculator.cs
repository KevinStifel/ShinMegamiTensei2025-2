namespace Shin_Megami_Tensei;

public static class DamageCalculator
{
    public static int CalculatePhysicalDamage(UnitBase attacker)
    {
        return (int)(attacker.Stats.Str * GameConstants.PhysicalDamageModifier * GameConstants.BaseDamageModifier);
    }

    public static int CalculateGunDamage(UnitBase attacker)
    {
        return (int)(attacker.Stats.Skl * GameConstants.GunDamageModifier * GameConstants.BaseDamageModifier);
    }

    public static int CalculateMagicDamage(UnitBase attacker, int skillPower)
    {
        return (int)Math.Sqrt(attacker.Stats.Mag * skillPower);
    }
}