namespace Shin_Megami_Tensei;

public static class GameConstants
{
    // ===== Archivos JSON =====
    public const string SamuraiFilePath = "data/samurai.json";
    public const string MonsterFilePath = "data/monsters.json";
    public const string SkillsFilePath  = "data/skills.json";

    // ===== Mensajes de Error =====
    public const string InvalidTeamsMessage = "Archivo de equipos inválido";

    // ===== Límites de Reglas =====
    public const int MaxUnitsPerTeam = 8;
    public const int MaxSkillsPerSamurai = 8;

    // ===== Prefijos de TXT =====
    public const string SamuraiPrefix = "[Samurai]";

    // ===== Tablero =====
    public static readonly string[] BoardPositions = ["A", "B", "C", "D"];
    
    // Damages
    public const int PhysicalDamageModifier = 54;
    public const int GunDamageModifier = 80;
    public const double BaseDamageModifier = 0.0114;
    
}
