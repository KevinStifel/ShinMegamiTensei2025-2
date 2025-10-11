namespace Shin_Megami_Tensei;

public class Affinity
{
    private readonly Dictionary<string, string> _affinities;

    public Affinity(Dictionary<string, string> affinities)
    {
        _affinities = new Dictionary<string, string>(affinities);
    }

    public string GetAffinityReaction(AffinityElement element)
        {
            string key = element switch
            {
                AffinityElement.Physical => "Phys",
                AffinityElement.Gun => "Gun",
                AffinityElement.Fire => "Fire",
                AffinityElement.Ice => "Ice",
                AffinityElement.Elec => "Elec",
                AffinityElement.Force => "Force",
                AffinityElement.Light => "Light",
                AffinityElement.Dark => "Dark",
                AffinityElement.Bind => "Bind",
                AffinityElement.Sleep => "Sleep",
                AffinityElement.Sick => "Sick",
                AffinityElement.Panic => "Panic",
                AffinityElement.Poison => "Poison",
                _ => "-"
            };

            return _affinities.GetValueOrDefault(key, "-");
        }

    public override string ToString()
    {
        return string.Join(", ", _affinities.Select(pair => $"{pair.Key}:{pair.Value}"));
    }
    
    public IReadOnlyDictionary<string, string> All => _affinities;
}