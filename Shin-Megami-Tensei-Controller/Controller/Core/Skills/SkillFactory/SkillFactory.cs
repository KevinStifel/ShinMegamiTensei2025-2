using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public static class SkillFactory
{
    public static Skill Create(SkillData skillData, BoardManager board, View view)
    {
        string name = skillData.Name.ToLowerInvariant();

        // ⚔️ Daño → usa EnemySelector (enemigos vivos)
        if (SkillCatalog.DamageSkills.Contains(name))
            return new Skill(
                skillData,
                new DamageEffect(view),
                new EnemySelector(view, board)
            );

        // 💚 Curación → usa AllySelector (aliados vivos)
        if (SkillCatalog.HealSkills.Contains(name))
            return new Skill(
                skillData,
                new HealEffect(view),
                new AllySelector(view, board)
            );

        // 💀 Revivir → usa DeadAllySelector (aliados muertos, incluye Samurai)
        if (SkillCatalog.ReviveSkills.Contains(name))
            return new Skill(
                skillData,
                new ReviveEffect(view),
                new DeadAllySelector(view, board)
            );

        // 🌀 Invitation → revive o invoca monstruos (vivos o muertos de la reserva)
        if (name == "invitation")
            return new Skill(
                skillData,
                new SummonEffect(view),
                new DeadAllySelector(view, board)
            );

        // ✨ Sabbatma → invoca monstruos vivos de la reserva
        if (name == "sabbatma")
            return new Skill(
                skillData,
                new SpecialEffect(view),
                new SpecialSelector(view, board)
            );

        throw new NotImplementedException($"Skill '{skillData.Name}' no está implementada en la SkillFactory.");
    }
}