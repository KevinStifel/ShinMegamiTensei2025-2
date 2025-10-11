using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class SummonSelectorView : TargetSelectorViewBase
{
    public SummonSelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase summonerUnit, List<UnitBase> reserveMonsters)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");

        for (int index = 0; index < reserveMonsters.Count; index++)
        {
            var monster = reserveMonsters[index];
            View.WriteLine($"{index + 1}-{monster.Name} HP:{monster.Stats.HP}/{monster.Stats.MaxHP} MP:{monster.Stats.MP}/{monster.Stats.MaxMP}");
        }

        View.WriteLine($"{reserveMonsters.Count + 1}-Cancelar");
    }
}