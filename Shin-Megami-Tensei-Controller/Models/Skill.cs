namespace Shin_Megami_Tensei;

public class Skill
{
    public string Name { get; }
    public string Type { get; }
    public int Cost { get; }
    public int Power { get; }
    public string Target { get; }
    public string Hits { get; }
    public string Effect { get; }

    public Skill(string name, string type, int cost, int power, string target, string hits, string effect)
    {
        Name = name;
        Type = type;
        Cost = cost;
        Power = power;
        Target = target;
        Hits = hits;
        Effect = effect;
    }

    public void ApplyEffect()
    {
        
    }
}