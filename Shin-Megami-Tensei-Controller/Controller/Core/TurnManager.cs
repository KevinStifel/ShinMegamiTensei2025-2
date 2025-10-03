namespace Shin_Megami_Tensei;

public class TurnManager
{
    private int _fullTurns;
    private int _blinkingTurns;

    public int FullTurns => _fullTurns;
    public int BlinkingTurns => _blinkingTurns;
    private List<UnitBase> _attackOrder = [];
    
    public IReadOnlyList<UnitBase> AttackOrder => _attackOrder;
    
    public record TurnDelta(int ConsumedFull, int ConsumedBlinking, int GainedBlinking);

    public void StartNewRound(List<UnitBase> activeUnits)
    {
        _fullTurns = CalculateInitialFullTurns(activeUnits);
        _blinkingTurns = 0;
        _attackOrder = GenerateAttackOrder(activeUnits);
    }

    private static int CalculateInitialFullTurns(List<UnitBase> activeUnits)
    {
        return activeUnits.Count(unit => unit.Stats.HP > 0);
    }

    private static List<UnitBase> GenerateAttackOrder(List<UnitBase> aliveUnits)
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
        if (consumeBlinking > 0)
        {
            _blinkingTurns = Math.Max(0, _blinkingTurns - consumeBlinking);
        }
        if (consumeFull > 0)
        {
            _fullTurns = Math.Max(0, _fullTurns - consumeFull);
        }
        if (gainBlinking > 0)
        {
            _blinkingTurns += gainBlinking;
        }
        RotateAttackOrder();
    }
    
    public UnitBase GetAttackerOnTurn()
    {
        return _attackOrder[0];
    }
    
    private void RotateAttackOrder()
    {
        if (_attackOrder.Count == 0) return;

        var firstUnit = _attackOrder[0];
        _attackOrder.RemoveAt(0);
        _attackOrder.Add(firstUnit);
    }

    public TurnDelta ConsumeActionTurn()
    {
        if (BlinkingTurns > 0)
        {
            ApplyTurnDelta(0, 1, 0); // consume un blinking
            return new TurnDelta(0, 1, 0);
        }

        ApplyTurnDelta(1, 0, 0); // si no hay blinking → consume un full
        return new TurnDelta(1, 0, 0);
    }

    public void UpdateOrderAfterSummon(
        UnitBase summoner,
        UnitBase summoned,
        UnitBase? replacedUnit)
    {
        if (summoner is Samurai)
        {
            if (replacedUnit == null)
            {
                // Samurai invoca en espacio vacío → lo agrega al final
                _attackOrder.Add(summoned);
            }
            else
            {
                // Samurai invoca reemplazando a un monstruo
                int index = _attackOrder.IndexOf(replacedUnit);
                if (index >= 0)
                {
                    _attackOrder[index] = summoned;
                }
            }
        }
        else
        {
            // Monstruo invoca → reemplaza a sí mismo
            int index = _attackOrder.IndexOf(summoner);
            if (index >= 0)
            {
                _attackOrder[index] = summoned;
            }
        }
    }


    public TurnDelta ConsumePassTurn()
    {
        if (BlinkingTurns > 0)
        {
            ApplyTurnDelta(0, 1, 0); // consume blinking
            return new TurnDelta(0, 1, 0);
        }

        ApplyTurnDelta(1, 0, 1); // consume full y gana un blinking
        return new TurnDelta(1, 0, 1);
    }
    public TurnDelta ConsumeSummonTurn()
    {
        if (BlinkingTurns > 0)
        {
            ApplyTurnDelta(0, 1, 0); // consume blinking
            return new TurnDelta(0, 1, 0);
        }

        ApplyTurnDelta(1, 0, 1); // consume full (no gana blinking)
        return new TurnDelta(1, 0, 1);
    }


}