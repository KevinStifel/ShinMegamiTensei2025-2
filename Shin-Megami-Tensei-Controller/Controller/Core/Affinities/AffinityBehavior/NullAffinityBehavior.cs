namespace Shin_Megami_Tensei;

public sealed class NullAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Null;

    public override double ModifyDamage(double baseDamage) => 0;
    
    public override void ApplyEffect(UnitBase caster, UnitBase target, int damage)
    {
        // No aplica daño. Solo bloquea el ataque.
    }

    public override TurnManager.TurnDelta CalculateTurnEffect(int fullTurns, int blinkingTurns)
    {
        int need = 2;
        int consumeBlink = Math.Min(blinkingTurns, need);
        int remaining = need - consumeBlink;
        int consumeFull = Math.Min(fullTurns, remaining);

        return new TurnManager.TurnDelta(consumeFull, consumeBlink, 0);
    }
}