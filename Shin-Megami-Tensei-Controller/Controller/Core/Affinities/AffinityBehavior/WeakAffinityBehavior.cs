namespace Shin_Megami_Tensei;

public sealed class WeakAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Weak;

    public override double ModifyDamage(double baseDamage) => (baseDamage * 1.5);
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        if (damage > 0)
            target.Stats.TakeDamage(damage);
    }

    // Consume 1 Full Turn y gana 1 Blinking Turn. Si no hay Fulls, consume 1 Blinking Turn.
    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        if (fullTurns > 0)
            return new TurnManager.TurnDelta(1, 0, 1);

        return new TurnManager.TurnDelta(0, 1, 0);
    }
}