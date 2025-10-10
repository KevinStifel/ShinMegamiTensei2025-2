using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public static class AffinityViewFactory
{
    public static AffinityViewBase Create(AffinityType type, View view, AffinityElement element) => type switch
    {
        AffinityType.Weak    => new WeakAffinityView(view, element),
        AffinityType.Resist  => new ResistAffinityView(view, element),
        AffinityType.Null    => new NullAffinityView(view, element),
        AffinityType.Repel   => new RepelAffinityView(view, element),
        AffinityType.Drain   => new DrainAffinityView(view, element),
        AffinityType.Neutral => new NeutralAffinityView(view, element),
        _ => new NeutralAffinityView(view, element)
    };
}