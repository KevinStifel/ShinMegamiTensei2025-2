namespace Shin_Megami_Tensei;

public sealed class NeutralAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Neutral;

    public override double ModifyDamage(double baseDamage) => baseDamage;

    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        if (damage > 0)
            target.Stats.TakeDamage(damage);
    }

    // Igual que Resist → Consume 1 Blinking Turn, o 1 Full Turn si no hay Blinking.
    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
        => blinkingTurns > 0
            ? new(0, 1, 0)
            : new(1, 0, 0);
}