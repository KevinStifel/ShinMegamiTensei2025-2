using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class CombatActionBase
{
    protected readonly CombatActionView ActionView;

    protected CombatActionBase(View view)
    {
        ActionView = new CombatActionView(view);
    }

    public abstract void ExecuteAction(BattleFlowContext battleFlowContext);

    protected static int GetEnemyPlayerId(int currentPlayerId) => currentPlayerId == 1 ? 2 : 1;
    protected static bool WasCanceledSelection(int selectedIndex) => selectedIndex < 0;

    protected int SelectEnemyTeamUnitIndex(UnitBase attacker, List<UnitBase> enemyUnits)
    {
        ActionView.ShowAvailableTargets(attacker, enemyUnits);

        var input = ActionView.ReadUserSelection();
        if (!int.TryParse(input, out int selectedIndex))
            return -1;

        selectedIndex -= 1;
        return selectedIndex >= 0 && selectedIndex < enemyUnits.Count ? selectedIndex : -1;
    }

    protected static void HandleDeathIfNeeded(BoardManager boardManager, int enemyPlayerId, UnitBase target)
    {
        if (target.Stats.HP == 0)
            boardManager.HandleUnitDeath(enemyPlayerId, target);
    }

    protected static string GetElementalMessage(AffinityElement element)
        => ElementMessageHelper.GetElementalMessage(element);
}