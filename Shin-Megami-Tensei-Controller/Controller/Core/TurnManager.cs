namespace Shin_Megami_Tensei;

public class TurnManager
{
    private Turn _currentTurn = new(0, 0, new List<UnitBase>());

    public int FullTurns => _currentTurn.FullTurns;
    public int BlinkingTurns => _currentTurn.BlinkingTurns;
    public IReadOnlyList<UnitBase> AttackOrder => _currentTurn.AttackOrder;

    public void StartNewRound(List<UnitBase> activeUnits)
    {
        _currentTurn.FullTurns = CalculateInitialFullTurns(activeUnits);
        _currentTurn.BlinkingTurns = 0;
        _currentTurn.AttackOrder = GenerateAttackOrder(activeUnits);
    }

    private static int CalculateInitialFullTurns(List<UnitBase> activeUnits)
        => activeUnits.Count(unit => unit.Stats.HP > 0);

    private static List<UnitBase> GenerateAttackOrder(List<UnitBase> activeUnits)
        => activeUnits.OrderByDescending(u => u.Stats.Spd).ToList();

    public bool HasAvailableTurns()
        => _currentTurn.FullTurns > 0 || _currentTurn.BlinkingTurns > 0;

    public UnitBase GetAttackerOnTurn()
        => _currentTurn.AttackOrder[0];

    private void RotateAttackOrder()
    {
        if (_currentTurn.AttackOrder.Count == 0) return;
        var first = _currentTurn.AttackOrder[0];
        _currentTurn.AttackOrder.RemoveAt(0);
        _currentTurn.AttackOrder.Add(first);
    }

    public void ApplyTurnChange(TurnChange change)
    {
        _currentTurn.FullTurns = Math.Max(0, _currentTurn.FullTurns - change.ConsumedFull);
        _currentTurn.BlinkingTurns = Math.Max(0, _currentTurn.BlinkingTurns - change.ConsumedBlinking);
        _currentTurn.BlinkingTurns += change.GainedBlinking;
        RotateAttackOrder();
    }

    public void UpdateOrderAfterSummon(UnitBase summoner, UnitBase summoned, UnitBase? replaced)
    {
        var order = _currentTurn.AttackOrder;

        if (summoner is Samurai || replaced == null)
        {
            if (replaced == null)
            {
                order.Add(summoned);
            }
            else
            {
                int index = order.IndexOf(replaced);
                if (index >= 0) order[index] = summoned;
            }
        }
        else
        {
            int index = order.IndexOf(summoner);
            if (index >= 0) order[index] = summoned;
        }
    }

    public TurnChange ConsumePassTurn()
    {
        var change = _currentTurn.BlinkingTurns > 0
            ? new TurnChange(0, 1, 0)
            : new TurnChange(1, 0, 1);

        ApplyTurnChange(change);
        return change;
    }

    public TurnChange ConsumeSummonTurn()
    {
        var change = _currentTurn.BlinkingTurns > 0
            ? new TurnChange(0, 1, 0)
            : new TurnChange(1, 0, 1);

        ApplyTurnChange(change);
        return change;
    }

    public TurnChange ConsumeNeutralTurn()
    {
        var change = _currentTurn.BlinkingTurns > 0
            ? new TurnChange(0, 1, 0)
            : new TurnChange(1, 0, 0);

        ApplyTurnChange(change);
        return change;
    }

    public TurnChange ApplyAffinityTurnEffect(AffinityBehavior affinity)
    {
        var change = affinity.CalculateTurnEffect(
            _currentTurn.FullTurns,
            _currentTurn.BlinkingTurns
        );

        ApplyTurnChange(change);
        return change;
    }
}