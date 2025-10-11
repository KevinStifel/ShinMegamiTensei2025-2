using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class CombatActionView : AbstractView
{
    public CombatActionView(View view) : base(view) { }

    public void ShowAvailableTargets(UnitBase attacker, List<UnitBase> enemies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {attacker.Name}");
        for (int index = 0; index < enemies.Count; index++)
        {
            var targetUnit = enemies[index];
            View.WriteLine($"{index + 1}-{targetUnit.Name} HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP} MP:{targetUnit.Stats.MP}/{targetUnit.Stats.MaxMP}");
        }
        View.WriteLine($"{enemies.Count + 1}-Cancelar");
    }
    public void ShowTurnConsumption(TurnChange turnChange)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Se han consumido {turnChange.ConsumedFull} Full Turn(s) y {turnChange.ConsumedBlinking} Blinking Turn(s)");
        View.WriteLine($"Se han obtenido {turnChange.GainedBlinking} Blinking Turn(s)");
    }
    public void ShowAvailableSkills(UnitBase casterUnit, IReadOnlyList<SkillData> skills)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione una habilidad para que {casterUnit.Name} use");
        for (var index = 0; index < skills.Count; index++)
        {
            var selectedSkill = skills[index];
            View.WriteLine($"{index + 1}-{selectedSkill.Name} MP:{selectedSkill.Cost}");
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
    public void ShowSurrender(UnitBase teamLeader, int playerId)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{teamLeader.Name} (J{playerId}) se rinde");
    }
    private void ShowSummonMenu(List<UnitBase> reserveUnits)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione un monstruo para invocar");

        List<UnitBase> aliveUnits = reserveUnits
            .Where(unit => unit.Stats.HP > 0)
            .ToList();

        for (int index = 0; index < aliveUnits.Count; index++)
        {
            var unit = aliveUnits[index];
            View.WriteLine(
                $"{index + 1}-{unit.Name} " +
                $"HP:{unit.Stats.HP}/{unit.Stats.MaxHP} " +
                $"MP:{unit.Stats.MP}/{unit.Stats.MaxMP}");
        }
        View.WriteLine($"{aliveUnits.Count + 1}-Cancelar");
    }

    private void ShowSummonPositionMenu(List<(string Position, UnitBase? UnitToReplace)> summonOptions)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine("Seleccione una posición para invocar");

        for (int index = 0; index < summonOptions.Count; index++)
        {
            var (boardPosition, unitToReplace) = summonOptions[index];
            int optionIndex = index + 1;
            int humanSlot = index + 2;

            if (unitToReplace == null )
            {
                View.WriteLine($"{optionIndex}-Vacío (Puesto {humanSlot})");
            }
            else
            {
                View.WriteLine($"{optionIndex}-{unitToReplace.Name} HP:{unitToReplace.Stats.HP}/{unitToReplace.Stats.MaxHP} " +
                               $"MP:{unitToReplace.Stats.MP}/{unitToReplace.Stats.MaxMP} (Puesto {humanSlot})");
            }
        }
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
}