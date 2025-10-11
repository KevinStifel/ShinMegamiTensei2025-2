using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class CombatActionBase
{
    protected readonly CombatActionView ActionView;
    protected readonly View View;

    protected CombatActionBase(View view)
    {
        ActionView = new CombatActionView(view);
        View = view;
    }

    public abstract void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager);

    protected static bool IsSelectionCanceled(int selectedIndex) => selectedIndex < 0;

    protected int SelectEnemyTeamUnitIndex(UnitBase attacker, List<UnitBase> enemyUnits)
    {
        ActionView.ShowAvailableTargets(attacker, enemyUnits);

        var input = ActionView.ReadUserSelection();
        if (!int.TryParse(input, out int selectedIndex))
            return -1;

        selectedIndex -= 1;
        return selectedIndex >= 0 && selectedIndex < enemyUnits.Count ? selectedIndex : -1;
    }
}