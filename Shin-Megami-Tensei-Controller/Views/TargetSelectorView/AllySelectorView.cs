using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class AllySelectorView : TargetSelectorViewBase
{
    public AllySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase attackerUnit, List<UnitBase> allies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {attackerUnit.Name}");

        for (int index = 0; index < allies.Count; index++)
        {
            var ally = allies[index];
            View.WriteLine($"{index + 1}-{ally.Name} HP:{ally.Stats.HP}/{ally.Stats.MaxHP} MP:{ally.Stats.MP}/{ally.Stats.MaxMP}");
        }

        View.WriteLine($"{allies.Count + 1}-Cancelar");
    }
}