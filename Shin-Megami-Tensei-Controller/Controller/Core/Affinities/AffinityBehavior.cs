namespace Shin_Megami_Tensei;

public abstract class AffinityBehavior
{
    public abstract AffinityType Type { get; }

    // Nuevo método abstracto para modificar el daño
    public abstract int ModifyDamage(int baseDamage);

    // Ya existente: calcular efecto sobre los turnos
    public abstract (int ConsumedFull, int ConsumedBlinking, int GainedBlinking) CalculateTurnEffect();

    public virtual string GetAffinityReactionText() => Type.ToString().ToUpper();
}