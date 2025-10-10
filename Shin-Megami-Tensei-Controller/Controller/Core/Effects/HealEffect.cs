using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class HealEffect : EffectBase
{
    public HealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        foreach (var target in targets)
        {
            int healAmount = (int)(target.Stats.MaxHP * (skillExecutionContext.SkillData.Power / 100.0));
            target.Stats.Heal(healAmount);
            EffectView.ShowHealEffect(caster, target, healAmount);
        }
        
        var turnManager = skillExecutionContext.TurnManager;
        ApplyTurnChange(turnManager);

    }
}