namespace Shin_Megami_Tensei;

public class Monster : UnitBase
{
    public List<Skill> Skills { get; }

    public Monster(string name, Stats stats, Affinity affinity, List<Skill> skills)
        : base(name, stats, affinity)
    {
        Skills = skills;
    }
}
