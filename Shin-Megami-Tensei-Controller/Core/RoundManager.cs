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
            var activeUnits = board.GetAliveUnits(currentPlayerId);
            _turnManager.StartNewRound(activeUnits);

            var leader = board.GetTeamLeaderUnit(currentPlayerId);
            _roundView.ShowRoundHeader(currentPlayerId, leader);

            while (_turnManager.HasAvailableTurns())
            {
                ShowRoundResume(board);
                ProcessPlayerAttackTurn(currentPlayerId, board);

                if (board.HasWinner())
                {
                    _roundView.ShowWinner(board.GetWinner(), board);
                    EndBattle();
                }
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
                    return;
                }
                catch (ActionCanceledException) { }
            }
        }

        private void EndBattle()
        {
            throw new BattleEndedException();
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
