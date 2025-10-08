namespace Shin_Megami_Tensei;

public static class AffinityMapper
{
    private static readonly Dictionary<string, AffinityElement> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Phys", AffinityElement.Physical },
        { "Gun", AffinityElement.Gun },
        { "Fire", AffinityElement.Fire },
        { "Ice", AffinityElement.Ice },
        { "Elec", AffinityElement.Elec },
        { "Force", AffinityElement.Force },
        { "Light", AffinityElement.Light },
        { "Dark", AffinityElement.Dark },
        { "Bind", AffinityElement.Bind },
        { "Sleep", AffinityElement.Sleep },
        { "Sick", AffinityElement.Sick },
        { "Panic", AffinityElement.Panic },
        { "Poison", AffinityElement.Poison }
    };

    public static AffinityElement Parse(string type)
    {
        return Map.TryGetValue(type, out var element)
            ? element
            : throw new ArgumentException($"Tipo de afinidad desconocido: {type}");
    }
}