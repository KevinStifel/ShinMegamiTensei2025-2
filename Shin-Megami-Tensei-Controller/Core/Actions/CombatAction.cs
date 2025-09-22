namespace Shin_Megami_Tensei
{
    public interface ICombatAction
    {
        void Execute(int currentPlayerId, Board board, TurnManager turnManager);
    }
}