namespace Shin_Megami_Tensei;

public static class AffinityBehaviorFactory
{
    public static AffinityBehavior Create(string reaction)
    {
        string normalized = reaction.Trim().ToLower();

        return normalized switch
        {
            "wk" => new WeakAffinityBehavior(),
            "rs" => new ResistAffinityBehavior(),
            "nu" => new NullAffinityBehavior(),
            "rp" => new RepelAffinityBehavior(),
            "dr" => new DrainAffinityBehavior(),
            "-" => new NeutralAffinityBehavior(),
            _ => new NeutralAffinityBehavior()
        };
    }
}