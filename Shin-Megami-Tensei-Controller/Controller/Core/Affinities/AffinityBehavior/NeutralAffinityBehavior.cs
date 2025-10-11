namespace Shin_Megami_Tensei;

public sealed class NeutralAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Neutral;

    public override double ModifyDamage(double baseDamage) => baseDamage;

    public override void ApplyEffect(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        targetUnit.Stats.TakeDamage(damage);
    }

    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
        => blinkingTurns > 0
            ? new TurnChange(0, 1, 0)
            : new TurnChange(1, 0, 0);
}