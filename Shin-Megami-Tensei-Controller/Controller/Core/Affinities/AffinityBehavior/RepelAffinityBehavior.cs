namespace Shin_Megami_Tensei;

public sealed class RepelAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Repel;

    public override int ModifyDamage(int baseDamage) => baseDamage; 
    // Se devuelve al atacante en otro paso

    public override (int, int, int) CalculateTurnEffect() => (99, 99, 0);
    // Consume todos los turnos (Full Turn)
}
