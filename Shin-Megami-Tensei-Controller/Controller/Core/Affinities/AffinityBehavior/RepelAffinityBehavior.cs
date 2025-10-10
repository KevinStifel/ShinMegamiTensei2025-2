namespace Shin_Megami_Tensei;

public sealed class RepelAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Repel;

    public override double ModifyDamage(double baseDamage) => baseDamage;
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        caster.Stats.TakeDamage(damage);
    }

    public override TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        return new TurnChange(fullTurns, blinkingTurns, 0);
    }
}