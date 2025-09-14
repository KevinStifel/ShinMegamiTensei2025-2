namespace Shin_Megami_Tensei;

public class TurnManager
{
    private int _fullTurns;
    private int _blinkingTurns;

    public int FullTurns => _fullTurns;
    public int BlinkingTurns => _blinkingTurns;

    public void StartNewRound()
    {
        _fullTurns = 1;
        _blinkingTurns = 0;
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