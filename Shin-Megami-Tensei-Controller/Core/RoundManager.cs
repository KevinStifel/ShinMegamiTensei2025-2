using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class RoundManager
    {
        private readonly RoundManagerView _roundView;
        private readonly TurnManager _turnManager;
        private readonly CombatActionFactory _actionFactory;
        private readonly View _view;

        public RoundManager(View view)
        {
            _view = view;
            _roundView = new RoundManagerView(_view);
            _turnManager = new TurnManager();
            _actionFactory = new CombatActionFactory(_view);
        }

        public void StartNewRound(int currentPlayerId, Board board)
        {
            // 👇 Revisar si ya hay un ganador antes de empezar
            int? winnerId = CheckForWinner(board);
            if (winnerId.HasValue)
            {
                _roundView.ShowWinner(winnerId.Value, board);
                return;            }

            var activeUnits = board.GetAliveUnits(currentPlayerId);
            _turnManager.StartNewRound(activeUnits);

            var leader = board.GetTeamLeaderUnit(currentPlayerId);
            _roundView.ShowRoundHeader(currentPlayerId, leader);

            while (_turnManager.HasAvailableTurns())
            {
                ShowRoundResume(board);
                ProcessPlayerAttackTurn(currentPlayerId, board);

                // 👇 Revisar ganador después de cada acción
                winnerId = CheckForWinner(board);
                if (winnerId.HasValue)
                {
                    _roundView.ShowWinner(winnerId.Value, board);
                    return;                }
            }
        }

        private void ShowRoundResume(Board board)
        {
            _roundView.ShowBothTeams(board);
            _roundView.ShowTurnStatus(_turnManager.FullTurns, _turnManager.BlinkingTurns);
            _roundView.ShowAttackOrder(_turnManager.AttackOrder);
        }

        private void ProcessPlayerAttackTurn(int currentPlayerId, Board board)
        {
            var turnActor = _turnManager.AttackOrder.First();

            while (true)
            {
                ShowActionsMenu(turnActor);

                var selectedActionKey = ReadActionKeyFromMenu(turnActor);
                var selectedAction = _actionFactory.CreateAction(selectedActionKey);

                try
                {
                    selectedAction.ExecuteAction(currentPlayerId, board, _turnManager);

                    int? winnerId = CheckForWinner(board);
                    if (winnerId.HasValue)
                    {
                        return;
                    }
                    return;
                }
                catch (ActionCanceledException) { }
            }
        }

        
        private int? CheckForWinner(Board board)
        {
            bool p1Alive = board.GetAliveUnits(1).Any();
            bool p2Alive = board.GetAliveUnits(2).Any();

            if (!p1Alive && !p2Alive) return 0;
            if (!p1Alive) return 2;
            if (!p2Alive) return 1;

            return null; 
        }

        private string ReadActionKeyFromMenu(UnitBase unit)
        {
            var options = ActionOptionsProvider.CreateMenuOptions(unit);
            var menuSelection = _view.ReadLine();
            return options.GetSelectedOption(menuSelection);
        }

        private void ShowActionsMenu(UnitBase unit)
        {
            if (unit is Samurai)
                _roundView.ShowAvailableActionsForSamurai(unit);
            else
                _roundView.ShowAvailableActionsForMonster(unit);
        }
    }
}
