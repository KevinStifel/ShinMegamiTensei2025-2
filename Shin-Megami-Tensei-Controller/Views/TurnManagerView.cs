using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei
{
    public class TurnManagerView
    {
        private readonly View _view;

        public TurnManagerView(View view)
        {
            _view = view;
        }

        public void ShowTurnStatus(TurnManager turnManager)
        {
            _view.WriteLine($"Full Turns: {turnManager.FullTurns}");
            _view.WriteLine($"Blinking Turns: {turnManager.BlinkingTurns}");
        }
    }
}