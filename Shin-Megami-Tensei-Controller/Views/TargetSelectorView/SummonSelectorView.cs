using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class SummonSelectorView : TargetSelectorViewBase
{
    public SummonSelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase caster, List<UnitBase> reserveMonsters)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");

        for (int i = 0; i < reserveMonsters.Count; i++)
        {
            var unit = reserveMonsters[i];
            View.WriteLine($"{i + 1}-{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}");
        }

        View.WriteLine($"{reserveMonsters.Count + 1}-Cancelar");
    }
}