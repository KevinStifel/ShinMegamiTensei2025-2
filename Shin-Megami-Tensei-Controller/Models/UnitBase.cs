namespace Shin_Megami_Tensei;

public abstract class UnitBase
{
    public string Name { get; }
    public Stats Stats { get; set; }
    public Affinity Affinity { get; }

    protected UnitBase(string name, Stats stats, Affinity affinity)
    {
        Name = name;
        Stats = stats;
        Affinity = affinity;
    }
}