namespace Shin_Megami_Tensei;

public sealed class DrainAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Drain;

    public override int ModifyDamage(int baseDamage) => -(int)(baseDamage * 0.5);
    // Daño negativo → cura

    public override (int, int, int) CalculateTurnEffect() => (99, 99, 0);
    // Consume todos los turnos
}