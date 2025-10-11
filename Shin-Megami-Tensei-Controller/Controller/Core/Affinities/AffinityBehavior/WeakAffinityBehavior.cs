namespace Shin_Megami_Tensei;

public sealed class WeakAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Weak;

    public override double ModifyDamage(double baseDamage) => baseDamage * 1.5;

    public override void ApplyEffect(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        targetUnit.Stats.TakeDamage(damage);
    }

    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        bool hasFullTurnsAvailable = fullTurns > 0;

        return hasFullTurnsAvailable 
            ? new TurnChange(1, 0, 1) 
            : new TurnChange(0, 1, 0);
    }
}