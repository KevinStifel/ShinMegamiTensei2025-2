namespace Shin_Megami_Tensei;

public class Samurai : UnitBase
{
    public List<Skill> Skills { get; }

    public Samurai(string name, Stats stats, Affinity affinity, List<Skill> skills)
        : base(name, stats, affinity)
    {
        Skills = skills;
    }
}