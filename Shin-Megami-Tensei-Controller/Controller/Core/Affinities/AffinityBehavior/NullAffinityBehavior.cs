namespace Shin_Megami_Tensei;

public sealed class NullAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Null;

    public override double ModifyDamage(double baseDamage) => 0;

    public override void ApplyEffect(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
    }

    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        int totalTurnsToConsume = 2;

        int blinkingTurnsConsumed = Math.Min(blinkingTurns, totalTurnsToConsume);
        int turnsStillNeeded = totalTurnsToConsume - blinkingTurnsConsumed;
        int fullTurnsConsumed = Math.Min(fullTurns, turnsStillNeeded);

        return new TurnChange(fullTurnsConsumed, blinkingTurnsConsumed, 0);
    }
}