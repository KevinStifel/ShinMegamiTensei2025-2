using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class EnemySelectorView : TargetSelectorViewBase
{
    public EnemySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase attackerUnit, List<UnitBase> enemies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {attackerUnit.Name}");
        for (int index = 0; index < enemies.Count; index++)
        {
            var targetUnit = enemies[index];
            View.WriteLine($"{index + 1}-{targetUnit.Name} HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP} MP:{targetUnit.Stats.MP}/{targetUnit.Stats.MaxMP}");
        }
        View.WriteLine($"{enemies.Count + 1}-Cancelar");
    }
}