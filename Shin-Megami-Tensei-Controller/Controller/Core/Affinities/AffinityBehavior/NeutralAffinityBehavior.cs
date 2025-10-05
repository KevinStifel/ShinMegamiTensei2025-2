namespace Shin_Megami_Tensei;

public sealed class NeutralAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Neutral;

    public override int ModifyDamage(int baseDamage) => baseDamage;

    public override (int, int, int) CalculateTurnEffect() => (0, 1, 0);
    // Consume 1 Blinking Turn (o Full si no hay)
}