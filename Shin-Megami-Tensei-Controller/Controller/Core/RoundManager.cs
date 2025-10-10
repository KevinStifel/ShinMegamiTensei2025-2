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

        public void StartNewRound(int currentPlayerId, BoardManager boardManager)
        {
            var activeUnits = boardManager.GetAliveUnits(currentPlayerId);
            _turnManager.StartNewRound(activeUnits);

            var leader = boardManager.GetTeamLeaderUnit(currentPlayerId);
            _roundView.ShowRoundHeader(currentPlayerId, leader);

            while (_turnManager.HasAvailableTurns())
            {
                ShowRoundResume(boardManager);
                ProcessPlayerAttackTurn(currentPlayerId, boardManager);

                if (boardManager.HasWinner())
                {
                    _roundView.ShowWinner(boardManager.GetWinner(), boardManager);
                    EndBattle();
                }
            }
        }

        private void ShowRoundResume(BoardManager boardManager)
        {
            _roundView.ShowBothTeams(boardManager);
            _roundView.ShowTurnStatus(_turnManager.FullTurns, _turnManager.BlinkingTurns);
            _roundView.ShowAttackOrder(_turnManager.AttackOrder);
        }

        private void ProcessPlayerAttackTurn(int currentPlayerId, BoardManager boardManager)
        {
            var turnActor = _turnManager.AttackOrder.First();
            
            while (true)
            {
                ShowActionsMenu(turnActor);
                var selectedActionKey = ReadActionKeyFromMenu(turnActor);
                var selectedAction = _actionFactory.CreateAction(selectedActionKey);

                try
                {
                    selectedAction.ExecuteAction(currentPlayerId, boardManager, _turnManager);
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
