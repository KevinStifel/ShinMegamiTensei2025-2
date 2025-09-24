namespace Shin_Megami_Tensei
{
    public interface ICombatAction
    {
        ActionExecutionResult ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager);
    }
}