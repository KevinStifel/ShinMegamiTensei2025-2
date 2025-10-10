using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public static class SkillFactory
{
    public static Skill Create(SkillData skillData, BattleFlowContext battleFlowContext)
    {
        string name = skillData.Name.ToLowerInvariant();
        BoardManager boardManager = battleFlowContext.BoardManager;
        View view = battleFlowContext.View;

        if (SkillCatalog.DamageSkills.Contains(name))
            return new Skill(
                skillData,
                new DamageEffect(view),
                new EnemySelector(view, boardManager)
            );

        if (SkillCatalog.HealSkills.Contains(name))
            return new Skill(
                skillData,
                new HealEffect(view),
                new AllySelector(view, boardManager)
            );

        if (SkillCatalog.ReviveSkills.Contains(name))
            return new Skill(
                skillData,
                new ReviveEffect(view),
                new DeadAllySelector(view, boardManager)
            );

        if (name == "invitation")
            return new Skill(
                skillData,
                new InvitationEffect(view),
                new ReserveSelectorAll(view, boardManager)
            );

        if (name == "sabbatma")
            return new Skill(
                skillData,
                new SpecialEffect(view),
                new SpecialSelector(view, boardManager)
            );

        throw new NotImplementedException($"Skill '{skillData.Name}' no está implementada en la SkillFactory.");
    }
}