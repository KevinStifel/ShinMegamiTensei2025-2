using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class SpecialSelectorView : TargetSelectorViewBase
{
    public SpecialSelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase caster, List<UnitBase> candidates)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");
        for (int i = 0; i < candidates.Count; i++)
        {
            var m = candidates[i];
            View.WriteLine($"{i + 1}-{m.Name} HP:{m.Stats.HP}/{m.Stats.MaxHP} MP:{m.Stats.MP}/{m.Stats.MaxMP}");
        }
        View.WriteLine($"{candidates.Count + 1}-Cancelar");
    }

    public void ShowSummonPositions(List<(string Position, UnitBase? Occupant)> summonOptions)
    {
        View.WriteLine("Seleccione una posición para invocar");
        for (int i = 0; i < summonOptions.Count; i++)
        {
            var (pos, occ) = summonOptions[i];

            // ✅ Buscar el número de puesto (A → 1, B → 2, etc.)
            int puesto = Array.IndexOf(GameConstants.BoardPositions, pos) + 1;

            string info = occ == null
                ? $"{i + 1}-Vacío (Puesto {puesto})"
                : $"{i + 1}-{occ.Name} HP:{occ.Stats.HP}/{occ.Stats.MaxHP} MP:{occ.Stats.MP}/{occ.Stats.MaxMP} (Puesto {puesto})";

            View.WriteLine(info);
        }
        View.WriteLine($"{summonOptions.Count + 1}-Cancelar");
    }



    public int ReadPositionIndex(int total)
    {
        string input = View.ReadLine();
        if (!int.TryParse(input, out int idx)) return -1;
        idx -= 1;
        return idx >= 0 && idx < total ? idx : -1;
    }
}