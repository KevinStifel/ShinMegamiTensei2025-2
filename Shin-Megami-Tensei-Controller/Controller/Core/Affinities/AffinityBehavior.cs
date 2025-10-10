namespace Shin_Megami_Tensei;

public abstract class AffinityBehavior
{
    public abstract AffinityType Type { get; }
    public abstract double ModifyDamage(double baseDamage);
    
    public abstract void ApplyEffect(UnitBase caster, UnitBase target, int damage);

    public abstract TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns);
}