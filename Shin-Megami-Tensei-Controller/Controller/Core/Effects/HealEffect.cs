using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class HealEffect : EffectBase
{
    public HealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager board)
    {
        // 💚 Aplicar la curación
        foreach (var target in targets)
        {
            int healAmount = (int)(target.Stats.MaxHP * (skillData.Power / 100.0));
            target.Stats.Heal(healAmount);
            EffectView.ShowHealEffect(caster, target, healAmount);
        }

        // ⚙️ Consumir un turno neutral (sin afinidad)
        var delta = turnManager.ConsumeNeutralTurn();

        // 🧩 Mostrar el resultado
        CombatActionView actionView = new CombatActionView(View);
        actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}