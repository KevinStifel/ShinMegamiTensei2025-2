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

    public void ApplyTurnDelta(int consumeFull, int consumeBlinking, int gainBlinking)
    {
        if (consumeFull > 0)
        {
            _fullTurns = Math.Max(0, _fullTurns - consumeFull);
        }
        if (consumeBlinking > 0)
        {
            _blinkingTurns = Math.Max(0, _blinkingTurns - consumeBlinking);
        }
        if (gainBlinking > 0)
        {
            _blinkingTurns += gainBlinking;
        }
        RotateAttackOrder();
    }

    public void ConsumeBlinkingTurn()
    {
        if (_blinkingTurns > 0) _blinkingTurns--;
    }
    private void RotateAttackOrder()
    {
        if (_attackOrder.Count == 0) return;

        var firstUnit = _attackOrder[0];
        _attackOrder.RemoveAt(0);
        _attackOrder.Add(firstUnit);
    }
}