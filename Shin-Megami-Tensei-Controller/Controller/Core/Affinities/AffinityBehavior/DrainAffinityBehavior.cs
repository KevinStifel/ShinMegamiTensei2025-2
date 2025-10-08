namespace Shin_Megami_Tensei;

public sealed class DrainAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Drain;

    public override double ModifyDamage(double baseDamage) => baseDamage;
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        // Absorbe el daño → se cura
        int healAmount = Math.Abs(damage);
        target.Stats.Heal(healAmount);
    }

    // Igual que Repel → Consume todos los turnos disponibles.
    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        return new TurnManager.TurnDelta(fullTurns, blinkingTurns, 0);
    }
}