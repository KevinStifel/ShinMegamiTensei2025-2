namespace Shin_Megami_Tensei;

public class TurnManager
{
    private int _fullTurns;
    private int _blinkingTurns;

    public int FullTurns => _fullTurns;
    public int BlinkingTurns => _blinkingTurns;
    private List<UnitBase> _attackOrder = [];
    
    public IReadOnlyList<UnitBase> AttackOrder => _attackOrder;
    

    public void StartNewRound(List<UnitBase> activeUnits)
    {
        _fullTurns = CalculateInitialFullTurns(activeUnits);
        _blinkingTurns = 0;
        _attackOrder = GenerateAttackOrder(activeUnits);
    }

    private int CalculateInitialFullTurns(List<UnitBase> activeUnits)
    {
        return activeUnits.Count(unit => unit.Stats.HP > 0);
    }

    private List<UnitBase> GenerateAttackOrder(List<UnitBase> aliveUnits)
    {
        return aliveUnits
            .OrderByDescending(unit => unit.Stats.Spd)
            .ToList();
    }
    public bool HasAvailableTurns()
    {
        return _fullTurns > 0 || _blinkingTurns > 0;
    }

    public void ConsumeFullTurn()
    {
        if (_fullTurns > 0) _fullTurns--;
    }

    public void ConsumeBlinkingTurn()
    {
        if (_blinkingTurns > 0) _blinkingTurns--;
    }
}