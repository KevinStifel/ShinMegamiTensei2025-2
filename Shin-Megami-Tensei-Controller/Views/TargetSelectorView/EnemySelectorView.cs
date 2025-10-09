using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class EnemySelectorView : TargetSelectorViewBase
{
    public EnemySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase caster, List<UnitBase> enemies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {caster.Name}");
        for (int i = 0; i < enemies.Count; i++)
        {
            var target = enemies[i];
            View.WriteLine($"{i + 1}-{target.Name} HP:{target.Stats.HP}/{target.Stats.MaxHP} MP:{target.Stats.MP}/{target.Stats.MaxMP}");
        }
        View.WriteLine($"{enemies.Count + 1}-Cancelar");
    }
}