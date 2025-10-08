namespace Shin_Megami_Tensei;

public static class UnitStatsManager
{
    public static bool HasEnoughMP(UnitBase unit, int cost)
        => unit.Stats.HasEnoughMP(cost);

    public static void ConsumeMP(UnitBase unit, int cost)
        => unit.Stats.UseMP(cost);

    public static void RestoreMP(UnitBase unit, int amount)
        => unit.Stats.RestoreMP(amount);

    public static void ApplyDamage(UnitBase unit, int damage)
        => unit.Stats.TakeDamage(damage);

    public static void Heal(UnitBase unit, int heal)
        => unit.Stats.Heal(heal);
}