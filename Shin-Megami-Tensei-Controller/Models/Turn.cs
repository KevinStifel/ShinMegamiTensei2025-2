namespace Shin_Megami_Tensei;

public class Turn
{
    public int FullTurns { get; set; }
    public int BlinkingTurns { get; set; }
    public List<UnitBase> AttackOrder { get; set; } = [];

    public Turn(int fullTurns, int blinkingTurns, List<UnitBase> attackOrder)
    {
        FullTurns = fullTurns;
        BlinkingTurns = blinkingTurns;
        AttackOrder = attackOrder;
    }
}