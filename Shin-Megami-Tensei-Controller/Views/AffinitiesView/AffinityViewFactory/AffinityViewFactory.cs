using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public static class AffinityViewFactory
{
    public static AffinityViewBase Create(AffinityType type, View view) => type switch
    {
        AffinityType.Weak    => new WeakAffinityView(view),
        AffinityType.Resist  => new ResistAffinityView(view),
        AffinityType.Null    => new NullAffinityView(view),
        AffinityType.Repel   => new RepelAffinityView(view),
        AffinityType.Drain   => new DrainAffinityView(view),
        AffinityType.Neutral => new NeutralAffinityView(view),
        _ => new NeutralAffinityView(view)
    };
}