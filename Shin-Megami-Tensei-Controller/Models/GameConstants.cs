namespace Shin_Megami_Tensei;

public static class GameConstants
{
    public const string SamuraiFilePath = "data/samurai.json";
    public const string MonsterFilePath = "data/monsters.json";
    public const string SkillsFilePath  = "data/skills.json";
    
    public const int MaxUnitsPerTeam = 8;
    public const int MaxSkillsPerSamurai = 8;
    
    public static readonly string[] BoardPositions = ["A", "B", "C", "D"];
    
    public const int PhysicalDamageModifier = 54;
    public const int GunDamageModifier = 80;
    public const double BaseDamageModifier = 0.0114;
    
}
