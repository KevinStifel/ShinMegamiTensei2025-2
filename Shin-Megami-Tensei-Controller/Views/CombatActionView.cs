using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class CombatActionView : AbstractView
{
    public CombatActionView(View view) : base(view) { }

    public void ShowAvailableTargets(UnitBase attacker, List<UnitBase> enemies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {attacker.Name}");
        for (int i = 0; i < enemies.Count; i++)
        {
            var target = enemies[i];
            View.WriteLine($"{i + 1}-{target.Name} HP:{target.Stats.HP}/{target.Stats.MaxHP} MP:{target.Stats.MP}/{target.Stats.MaxMP}");
        }
        View.WriteLine($"{enemies.Count + 1}-Cancelar");
    }
    
    public void ShowAttackIntro(UnitBase attacker, UnitBase target, string actionVerb, string affinityReaction)
    {
        ShowSeparator();
        View.WriteLine($"{attacker.Name} {actionVerb} a {target.Name}");
    }
    public void ShowTurnConsumption(int consumedFull, int consumedBlinking, int gainedBlinking)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Se han consumido {consumedFull} Full Turn(s) y {consumedBlinking} Blinking Turn(s)");
        View.WriteLine($"Se han obtenido {gainedBlinking} Blinking Turn(s)");
    }
    
    // Skills:
    public void ShowAvailableSkills(UnitBase caster, IReadOnlyList<SkillData> skills)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione una habilidad para que {caster.Name} use");
        for (var i = 0; i < skills.Count; i++)
        {
            var s = skills[i];
            View.WriteLine($"{i + 1}-{s.Name} MP:{s.Cost}");
        }
        View.WriteLine($"{skills.Count + 1}-Cancelar");
    }

    public int ReadSkillIndexFromInput(IReadOnlyList<SkillData> skills)
    {
        var userInput = ReadUserSelection();
        var selectedOptionIndex = int.Parse(userInput) - 1;
        var totalSkillsCount = skills.Count;

        if (IsCancelOption(selectedOptionIndex, totalSkillsCount))
            return -1;

        return selectedOptionIndex;
    }
    
    // Surrender:
    public void ShowSurrender(UnitBase teamLeader, int playerId)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{teamLeader.Name} (J{playerId}) se rinde");
    }
    
    // Summon
    private void ShowSummonMenu(List<UnitBase> reserveUnits)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");

        List<UnitBase> aliveUnits = reserveUnits
            .Where(unit => unit.Stats.HP > 0)
            .ToList();

        for (int i = 0; i < aliveUnits.Count; i++)
        {
            var unit = aliveUnits[i];
            View.WriteLine(
                $"{i + 1}-{unit.Name} " +
                $"HP:{unit.Stats.HP}/{unit.Stats.MaxHP} " +
                $"MP:{unit.Stats.MP}/{unit.Stats.MaxMP}");
        }
        View.WriteLine($"{aliveUnits.Count + 1}-Cancelar");
    }

    private void ShowSummonPositionMenu(List<(string Position, UnitBase? UnitToReplace)> summonOptions)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione una posición para invocar");

        for (int optionIndex = 0; optionIndex < summonOptions.Count; optionIndex++)
        {
            var (boardPosition, unitToReplace) = summonOptions[optionIndex];
            int displayIndex = optionIndex + 1; // se imprime desde 1, no desde 0
            int humanSlot = optionIndex + 2;   // el "puesto" comienza desde 2 (porque A es el samurái fijo en 1)

            if (unitToReplace == null )
            {
                View.WriteLine($"{displayIndex}-Vacío (Puesto {humanSlot})");
            }
            else
            {
                View.WriteLine($"{displayIndex}-{unitToReplace.Name} HP:{unitToReplace.Stats.HP}/{unitToReplace.Stats.MaxHP} " +
                               $"MP:{unitToReplace.Stats.MP}/{unitToReplace.Stats.MaxMP} (Puesto {humanSlot})");
            }
        }

        // opción de cancelar
        View.WriteLine($"{summonOptions.Count + 1}-Cancelar");
    }

    public int ReadSummonIndex(List<UnitBase> reserveUnits)
    {
        ShowSummonMenu(reserveUnits);
        var selection = ReadUserSelection();
        var index = int.Parse(selection) - 1;
        return IsCancelOption(index, reserveUnits.Count) ? -1 : index;
    }



    public int ReadSummonPositionIndex(List<(string position, UnitBase? unit)> options)
    {
        ShowSummonPositionMenu(options);
        var selection = ReadUserSelection();
        var index = int.Parse(selection) - 1;
        return IsCancelOption(index, options.Count) ? -1 : index;
    }

    public void ShowSummonResult(UnitBase summonedUnit)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{summonedUnit.Name} ha sido invocado");
    }
}