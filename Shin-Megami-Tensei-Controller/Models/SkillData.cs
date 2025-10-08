namespace Shin_Megami_Tensei;

public class SkillData
{
    public string Name { get; }
    public string Type { get; }
    public int Cost { get; }
    public int Power { get; }
    public string Target { get; }
    public string Hits { get; }
    public string Effect { get; }

    public SkillData(string name, string type, int cost, int power, string target, string hits, string effect)
    {
        Name = name;
        Type = type;
        Cost = cost;
        Power = power;
        Target = target;
        Hits = hits;
        Effect = effect;
    }
    public override string ToString()
    {
        return
            $"[SkillData]\n" +
            $"  Name   : {Name}\n" +
            $"  Type   : {Type}\n" +
            $"  Cost   : {Cost}\n" +
            $"  Power  : {Power}\n" +
            $"  Target : {Target}\n" +
            $"  Hits   : {Hits}\n" +
            $"  Effect : {Effect}\n";
    }
}