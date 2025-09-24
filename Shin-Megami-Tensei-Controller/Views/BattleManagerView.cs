using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public sealed class BattleManagerView : AbstractView
    {
        public BattleManagerView(View view) : base(view) { }

        public void ShowWinner(int winnerId, Board board)
        {
            View.WriteLine("----------------------------------------");

            if (winnerId == 0)
            {
                View.WriteLine("Empate: ambos equipos fueron derrotados.");
                return;
            }

            var leader = board.GetTeamLeaderUnit(winnerId);
            View.WriteLine($"Ganador: {leader.Name} (J{winnerId})");
        }
    }
}