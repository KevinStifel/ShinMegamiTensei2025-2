using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class DeadAllySelectorView : TargetSelectorViewBase
{
    public DeadAllySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase caster, List<UnitBase> deadAllies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {caster.Name}");

        for (int i = 0; i < deadAllies.Count; i++)
        {
            var ally = deadAllies[i];
            View.WriteLine($"{i + 1}-{ally.Name} HP:0/{ally.Stats.MaxHP} MP:{ally.Stats.MP}/{ally.Stats.MaxMP}");
        }

        View.WriteLine($"{deadAllies.Count + 1}-Cancelar");
    }
}