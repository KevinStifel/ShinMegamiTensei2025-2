// Actions/SurrenderAction.cs
using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class SurrenderAction : CombatActionBase
    {
        public SurrenderAction(View view) : base(view) { }

        public override ActionExecutionResult ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
        {
            var teamLeader = board.GetTeamLeaderUnit(currentPlayerId);

            _actionView.ShowSurrender(teamLeader, currentPlayerId);

            IReadOnlyDictionary<string, UnitBase?> playerBoard = board.GetBoardForPlayer(currentPlayerId);
            foreach (var unit in playerBoard.Values)
            {
                if (unit is null) continue;
                if (unit.Stats.HP > 0)
                {
                    unit.Stats.TakeDamage(unit.Stats.HP);
                }
            }

            // Consumir turnos restantes de la ronda
            if (turnManager.FullTurns > 0 || turnManager.BlinkingTurns > 0)
            {
                turnManager.ApplyTurnDelta(
                    consumeFull:     turnManager.FullTurns,
                    consumeBlinking: turnManager.BlinkingTurns,
                    gainBlinking:    0
                );
            }

            return ActionExecutionResult.AdvanceTurn();
        }
    }
}