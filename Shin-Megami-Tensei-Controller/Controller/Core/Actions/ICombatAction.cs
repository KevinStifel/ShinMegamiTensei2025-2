namespace Shin_Megami_Tensei
{
    public interface ICombatAction
    {
        void  ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager);
    }
}