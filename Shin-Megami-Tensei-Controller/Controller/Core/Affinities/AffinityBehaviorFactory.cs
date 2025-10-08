namespace Shin_Megami_Tensei;

public static class AffinityBehaviorFactory
{
    public static AffinityBehavior Create(string reaction)
    {
        string normalized = reaction.Trim().ToLower();

        return normalized switch
        {
            // Weak
            "weak" or "wk" => new WeakAffinityBehavior(),

            // Resist
            "resist" or "rs" => new ResistAffinityBehavior(),

            // Null
            "null" or "nu" => new NullAffinityBehavior(),

            // Repel
            "repel" or "rp" => new RepelAffinityBehavior(),

            // Drain
            "drain" or "dr" => new DrainAffinityBehavior(),

            // Neutral or undefined
            "-" or "neutral" => new NeutralAffinityBehavior(),

            _ => new NeutralAffinityBehavior()
        };
    }
}