namespace Shin_Megami_Tensei;

public static class BattleHelper
{
    public static int GetEnemyPlayerId(int currentPlayerId)
        => currentPlayerId == 1 ? 2 : 1;
}