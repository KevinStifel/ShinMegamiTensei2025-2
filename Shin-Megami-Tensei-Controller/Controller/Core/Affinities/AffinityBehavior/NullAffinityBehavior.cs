namespace Shin_Megami_Tensei;

public sealed class NullAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Null;

    public override int ModifyDamage(int baseDamage) => 0;

    public override (int, int, int) CalculateTurnEffect() => (0, 2, 0);
    // Consume 2 Blinking Turns (o Full si no hay suficientes)
}