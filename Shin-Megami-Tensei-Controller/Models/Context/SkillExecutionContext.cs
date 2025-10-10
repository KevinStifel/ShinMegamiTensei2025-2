namespace Shin_Megami_Tensei;

public sealed class SkillExecutionContext
{
    public SkillData SkillData { get; }
    public BoardManager BoardManager { get; }
    public TurnManager TurnManager { get; }
    public int CurrentPlayerId { get; }

    public SkillExecutionContext(
        SkillData skillData,
        BoardManager boardManager,
        TurnManager turnManager,
        int currentPlayerId)
    {
        SkillData = skillData;
        BoardManager = boardManager;
        TurnManager = turnManager;
        CurrentPlayerId = currentPlayerId;
    }
}