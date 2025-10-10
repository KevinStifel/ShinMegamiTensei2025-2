using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view) : base(view) { }

    public UnitBase? ApplySamuraiSummon(
        UnitBase monsterToSummon,
        string chosenPosition,
        UnitBase? occupantAtPosition,
        Dictionary<string, UnitBase?> playerBoard,
        List<UnitBase> reserveUnits)
    {
        playerBoard[chosenPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);

        if (occupantAtPosition != null)
            reserveUnits.Insert(0, occupantAtPosition);

        EffectView.ShowSummonResult(monsterToSummon);
        return occupantAtPosition;
    }

    public UnitBase ApplyMonsterSummon(
        UnitBase summoner,
        UnitBase monsterToSummon,
        Dictionary<string, UnitBase?> playerBoard,
        List<UnitBase> reserveUnits)
    {
        var summonerPosition = playerBoard.First(kvp => ReferenceEquals(kvp.Value, summoner)).Key;
        playerBoard[summonerPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);
        reserveUnits.Insert(0, summoner);

        EffectView.ShowSummonResult(monsterToSummon);
        return summoner;
    }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        BattleFlowContext battleFlowContext)
    {
        throw new NotImplementedException();
    }
}