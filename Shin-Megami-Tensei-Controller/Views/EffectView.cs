using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class EffectView : AbstractView
{
    public EffectView(View view) : base(view) { }
    
    // Curación
    public void ShowHealEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} cura a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }

    public void ShowReviveEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} revive a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }


    // Invocación (Summon)
    public void ShowSummonResult(UnitBase target)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{target.Name} ha sido invocado");
    }
    
    public int ReadSummonPositionIndex(List<(string Position, UnitBase? Occupant)> summonOptions)
    {
        ShowSummonPositionMenu(summonOptions);
        string input = View.ReadLine();

        if (!int.TryParse(input, out int index))
            return -1;

        index -= 1;
        return index >= 0 && index < summonOptions.Count ? index : -1;
    }
    
    public void ShowSummonMenu(List<UnitBase> reserveUnits)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");

        var aliveUnits = reserveUnits.Where(u => u.Stats.HP > 0).ToList();

        for (int i = 0; i < aliveUnits.Count; i++)
        {
            var unit = aliveUnits[i];
            View.WriteLine($"{i + 1}-{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}");
        }

        View.WriteLine($"{aliveUnits.Count + 1}-Cancelar");
    }
    
    public int ReadSummonIndex(List<UnitBase> reserveUnits)
    {
        ShowSummonMenu(reserveUnits);
        string input = View.ReadLine();

        if (!int.TryParse(input, out int index))
            return -1;

        index -= 1;
        return index >= 0 && index < reserveUnits.Count ? index : -1;
    }
    
    public void ShowSummonPositionMenu(List<(string Position, UnitBase? Occupant)> summonOptions)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione una posición para invocar");

        for (int i = 0; i < summonOptions.Count; i++)
        {
            var (pos, occ) = summonOptions[i];
            int puesto = Array.IndexOf(GameConstants.BoardPositions, pos) + 1;

            string info = occ == null
                ? $"{i + 1}-Vacío (Puesto {puesto})"
                : $"{i + 1}-{occ.Name} HP:{occ.Stats.HP}/{occ.Stats.MaxHP} MP:{occ.Stats.MP}/{occ.Stats.MaxMP} (Puesto {puesto})";

            View.WriteLine(info);
        }

        View.WriteLine($"{summonOptions.Count + 1}-Cancelar");
    }
}