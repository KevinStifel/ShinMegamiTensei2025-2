namespace Shin_Megami_Tensei;

public sealed class ResistAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Resist;

    public override double ModifyDamage(double baseDamage) => baseDamage * 0.5;

    public override void ApplyEffect(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        targetUnit.Stats.TakeDamage(damage);
    }

    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        bool hasBlinkingTurnsAvailable = blinkingTurns > 0;

        return hasBlinkingTurnsAvailable 
            ? new TurnChange(0, 1, 0) 
            : new TurnChange(1, 0, 0);
    }
}