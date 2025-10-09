using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class NullAffinityView : AffinityViewBase
{
    public NullAffinityView(View view) : base(view) { }

    public override void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage, string verb)
    {
        View.WriteLine($"{caster.Name} {verb} a {target.Name}");
        View.WriteLine($"{target.Name} bloquea el ataque de {caster.Name}");
    }
}