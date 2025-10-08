namespace Shin_Megami_Tensei;

public class Monster : UnitBase
{
    public List<SkillData> Skills { get; }

    public Monster(string name, Stats stats, Affinity affinity, List<SkillData> skills)
        : base(name, stats, affinity)
    {
        Skills = skills;
    }
}
