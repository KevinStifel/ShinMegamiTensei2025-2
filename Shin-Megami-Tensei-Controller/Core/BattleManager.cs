using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class BattleManager
{
    private readonly List<UnitBase> _playerOneUnitsList;
    private readonly List<UnitBase> _playerTwoUnitsList;
    private readonly Board _board;
    private readonly View _view;

    private readonly RoundManager _roundManager;

    public BattleManager(Board board, List<UnitBase> playerOneUnitList, List<UnitBase> playerTwoUnitList, View view)
    {
        _board = board;
        _playerOneUnitsList = playerOneUnitList;
        _playerTwoUnitsList = playerTwoUnitList;
        _view = view;
        _roundManager = new RoundManager(view);
    }
    public void StartBattle()
    {
        int currentPlayerId = 1;
        while (true)
        {
            _roundManager.StartNewRound(currentPlayerId, _board);

            // lógica de cambio de jugador
            currentPlayerId = 2;

            // chequeo de fin de combate se agregará después
            break; // placeholder
        }
    }
}