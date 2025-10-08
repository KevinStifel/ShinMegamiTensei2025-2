namespace Shin_Megami_Tensei;

public static class SkillCatalog
{
    public static readonly HashSet<string> DamageSkills =
    [
        // Physical
        "lunge", "oni-kagura", "mortal jihad", "gram slice", "fatal sword",
        "berserker god", "bouncing claw", "damascus claw", "nihil claw",
        "axel claw", "iron judgement", "stigma attack",
        // Gun
        "needle shot", "tathlum shot", "grand tack", "riot gun",
        // Fire
        "agi", "agilao", "agidyne", "trisagion",
        // Ice
        "bufu", "bufula", "bufudyne",
        // Elec
        "zio", "zionga", "ziodyne",
        // Force
        "zan", "zanma", "zandyne", "deadly wind"
    ];

    public static readonly HashSet<string> HealSkills = ["dia", "diarama", "diarahan"];

    public static readonly HashSet<string> ReviveSkills = ["recarm", "samarecarm"];
}