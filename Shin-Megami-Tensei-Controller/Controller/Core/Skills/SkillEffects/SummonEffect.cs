using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view)
        : base(view)
    {
    }

    public override void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillData skillData, TurnManager turnManager, int currentPlayerId, BoardManager board)
    {
        foreach (var target in targets)
        {
            EffectView.ShowSummon(target);
        }
    }
}