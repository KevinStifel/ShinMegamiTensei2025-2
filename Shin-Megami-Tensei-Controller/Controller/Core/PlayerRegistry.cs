namespace Shin_Megami_Tensei;

public static class PlayerRegistry
{
    private static readonly Dictionary<int, int> _skillCounters = new();
    
    public static void RegisterPlayer(int playerId)
    {
        _skillCounters.TryAdd(playerId, 0);
    }

    public static int GetSkillCount(int playerId)
    {
        _skillCounters.TryAdd(playerId, 0);
        return _skillCounters[playerId];
    }

    public static void IncrementSkillCount(int playerId)
    {
        _skillCounters.TryAdd(playerId, 0);
        _skillCounters[playerId]++;
    }

    public static void ResetAll() => _skillCounters.Clear();
}