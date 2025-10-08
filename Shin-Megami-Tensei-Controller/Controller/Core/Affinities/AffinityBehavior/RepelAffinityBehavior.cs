namespace Shin_Megami_Tensei;

public sealed class RepelAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Repel;

    public override double ModifyDamage(double baseDamage) => baseDamage;
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        // Refleja el daño al atacante
        caster.Stats.TakeDamage(damage);
    }

    // Consume todos los turnos disponibles (Full + Blinking)
    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        return new TurnManager.TurnDelta(fullTurns, blinkingTurns, 0);
    }
}