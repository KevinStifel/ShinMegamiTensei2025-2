namespace Shin_Megami_Tensei;

public sealed class DrainAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Drain;

    public override double ModifyDamage(double baseDamage) => baseDamage;
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        int healAmount = Math.Abs(damage);
        target.Stats.Heal(healAmount);
    }
    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        return new TurnChange(fullTurns, blinkingTurns, 0);
    }
}