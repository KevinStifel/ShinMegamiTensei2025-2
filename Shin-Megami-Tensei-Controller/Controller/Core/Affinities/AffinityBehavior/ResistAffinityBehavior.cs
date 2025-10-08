namespace Shin_Megami_Tensei;

public sealed class ResistAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Resist;

    public override double ModifyDamage(double baseDamage) => (baseDamage * 0.5);
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        if (damage > 0)
            target.Stats.TakeDamage(damage);
    }

    // Consume 1 Blinking Turn, o 1 Full Turn si no hay Blinking.
    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        if (blinkingTurns > 0)
            return new TurnManager.TurnDelta(0, 1, 0);

        return new TurnManager.TurnDelta(1, 0, 0);
    }
}