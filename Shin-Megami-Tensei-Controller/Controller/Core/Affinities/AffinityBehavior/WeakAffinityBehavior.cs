namespace Shin_Megami_Tensei;

public sealed class WeakAffinityBehavior : AffinityBehavior
{
    public override AffinityType Type => AffinityType.Weak;

    public override int ModifyDamage(int baseDamage) => (int)(baseDamage * 1.5);

    public override (int, int, int) CalculateTurnEffect() => (1, 0, 1); 
    // Consume un Full Turn, gana un Blinking Turn
}