namespace Shin_Megami_Tensei;

public class Affinity
{
    private readonly Dictionary<string, string> _affinities;

    public Affinity(Dictionary<string, string> affinities)
    {
        _affinities = new Dictionary<string, string>(affinities);
    }

    public string GetReaction(string attackType)
    {
        return _affinities.ContainsKey(attackType)
            ? _affinities[attackType]
            : "-"; // Neutral si no está definido
    }

    public override string ToString()
    {
        return string.Join(", ", _affinities.Select(pair => $"{pair.Key}:{pair.Value}"));
    }
    
    // Utilizado solo para el DebugPrinter
    public IReadOnlyDictionary<string, string> All => _affinities;
}