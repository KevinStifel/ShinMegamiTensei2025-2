namespace Shin_Megami_Tensei;

public class Samurai : UnitBase
{
    public List<SkillData> Skills { get; }

    public Samurai(string name, Stats stats, Affinity affinity, List<SkillData> skills)
        : base(name, stats, affinity)
    {
        Skills = skills;
    }
}