using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public static class SkillFactory
{
    public static Skill Create(SkillData skillData, AffinityBehavior behavior, View view)
    {
        string name = skillData.Name.ToLowerInvariant();

        if (SkillCatalog.DamageSkills.Contains(name))
            return new Skill(skillData, new DamageEffect(behavior, view));

        if (SkillCatalog.HealSkills.Contains(name))
            return new Skill(skillData, new HealEffect(behavior, view));

        if (SkillCatalog.ReviveSkills.Contains(name))
            return new Skill(skillData, new ReviveEffect(behavior, view));

        if (name == "invitation")
            return new Skill(skillData, new SummonEffect(behavior, view));

        if (name == "sabbatma")
            return new Skill(skillData, new SpecialEffect(behavior, view));

        throw new NotImplementedException($"Skill '{skillData.Name}' no está implementada en la SkillFactory.");
    }
}