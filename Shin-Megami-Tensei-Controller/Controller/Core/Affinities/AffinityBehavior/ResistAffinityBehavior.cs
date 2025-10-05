namespace Shin_Megami_Tensei;

public sealed class ResistAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Resist;

    public override int ModifyDamage(int baseDamage) => (int)(baseDamage * 0.5);

    public override (int, int, int) CalculateTurnEffect() => (0, 1, 0);
    // Consume 1 Blinking Turn (o 1 Full si no hay)
}