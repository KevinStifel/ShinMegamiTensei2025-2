using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view) : base(view) { }

    public UnitBase? ApplySamuraiSummon(UnitBase monsterToSummon, PlayerBoardFormation boardFormation, SummonPlacement placement)
    {
        boardFormation.ActiveBoard[placement.BoardPosition] = monsterToSummon;
        boardFormation.ReserveUnits.Remove(monsterToSummon);

        if (placement.ReplacedUnit != null)
            boardFormation.ReserveUnits.Insert(0, placement.ReplacedUnit);

        EffectView.ShowSummonResult(monsterToSummon);
        return placement.ReplacedUnit;
    }

    public UnitBase ApplyMonsterSummon(SummonData summonData, PlayerBoardFormation boardFormation)
    {
        var summonerPosition = boardFormation.ActiveBoard.First(kvp => ReferenceEquals(kvp.Value, summonData.Summoner)).Key;
        boardFormation.ActiveBoard[summonerPosition] = summonData.MonsterToSummon;

        boardFormation.ReserveUnits.Remove(summonData.MonsterToSummon);
        boardFormation.ReserveUnits.Insert(0, summonData.Summoner);

        EffectView.ShowSummonResult(summonData.MonsterToSummon);
        return summonData.Summoner;
    }

    public override void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillData skillData, BattleFlowContext context)
    {
        throw new NotImplementedException();
    }
}