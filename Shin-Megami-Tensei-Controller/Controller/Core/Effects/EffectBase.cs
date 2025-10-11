using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly EffectView EffectView;
    protected readonly CombatActionView ActionView;
    protected readonly View View;

    protected EffectBase(View view)
    {
        View = view;
        EffectView = new EffectView(view);
        ActionView = new CombatActionView(view);
    }

    public abstract void ApplyEffect(UnitBase casterUnit, List<UnitBase> targets, SkillExecutionContext skillExecutionContext);
    protected void ApplyTurnChange(TurnManager turnManager)
    {
        var turnChange = turnManager.ConsumeNeutralTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
    
}