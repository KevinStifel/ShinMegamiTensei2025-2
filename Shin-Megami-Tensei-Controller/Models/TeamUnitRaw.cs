namespace Shin_Megami_Tensei;

public class TeamUnitRaw
{
    public bool IsSamurai { get; }
    public string Name { get; }
    public List<string> SkillNames { get; }

    public TeamUnitRaw(bool isSamurai, string name, List<string> skillNames)
    {
        IsSamurai = isSamurai;
        Name = name;
        SkillNames = skillNames;
    }
}