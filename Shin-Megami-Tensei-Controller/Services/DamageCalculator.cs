namespace Shin_Megami_Tensei;

public static class DamageCalculator
{
    public static int CalculatePhysicalDamage(UnitBase attacker)
    {
        return (int)(attacker.Stats.Str * 54 * 0.0114);
    }

    public static int CalculateGunDamage(UnitBase attacker)
    {
        return (int)(attacker.Stats.Skl * 80 * 0.0114);
    }

    public static int CalculateMagicDamage(UnitBase attacker, int skillPower)
    {
        return (int)Math.Sqrt(attacker.Stats.Mag * skillPower);
    }
}