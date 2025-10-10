using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class ReviveEffect : EffectBase
{
    public ReviveEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager boardManager)
    {
        foreach (var target in targets)
        {
            // Solo revive si está muerto
            if (target.Stats.HP > 0)
                continue;

            int healAmount = (int)(target.Stats.MaxHP * (skillData.Power / 100.0));
            target.Stats.Heal(healAmount);

            EffectView.ShowReviveEffect(caster, target, healAmount);
        }

        // Consumir un turno neutral
        var delta = turnManager.ConsumeNeutralTurn();

        var actionView = new CombatActionView(View);
        actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}