namespace Shin_Megami_Tensei;

public static class DebugPrinter
{
    public static void PrintTeam(string title, List<UnitBase> team)
    {
        Console.WriteLine($"===== {title} =====");
        foreach (var unit in team)
        {
            PrintUnit(unit);
            Console.WriteLine();
        }
    }

    private static void PrintUnit(UnitBase unit)
    {
        Console.WriteLine($"Nombre: {unit.Name}");
        Console.WriteLine($"HP: {unit.Stats.HP}, MP: {unit.Stats.MP}");
        Console.WriteLine($"Str: {unit.Stats.Str}, Skl: {unit.Stats.Skl}, Mag: {unit.Stats.Mag}, Spd: {unit.Stats.Spd}, Lck: {unit.Stats.Lck}");

        // Afinidades
        Console.WriteLine("Afinidades:");
        foreach (var pair in unit.Affinity.All)
        {
            Console.WriteLine($"  - {pair.Key}: {pair.Value}");
        }

        // Skills
        if (unit is Samurai samurai)
        {
            Console.WriteLine("Tipo: Samurai");
            Console.WriteLine("Skills:");
            foreach (var skill in samurai.Skills)
                Console.WriteLine($"  - {skill.Name} (Cost {skill.Cost}, Power {skill.Power})");
        }
        else if (unit is Monster monster)
        {
            Console.WriteLine("Tipo: Monster");
            Console.WriteLine("Skills:");
            foreach (var skill in monster.Skills)
                Console.WriteLine($"  - {skill.Name} (Cost {skill.Cost}, Power {skill.Power})");
        }
    }
    
    public static void PrintBoard(string title, IReadOnlyDictionary<string, UnitBase?> board1, IReadOnlyDictionary<string, UnitBase?> board2)
    {
        Console.WriteLine($"===== {title} =====");
        Console.WriteLine("Player 1 Board:");
        foreach (var slot in board1)
            Console.WriteLine($"{slot.Key}: {(slot.Value?.Name ?? "-")}");

        Console.WriteLine("Player 2 Board:");
        foreach (var slot in board2)
            Console.WriteLine($"{slot.Key}: {(slot.Value?.Name ?? "-")}");
    }

}