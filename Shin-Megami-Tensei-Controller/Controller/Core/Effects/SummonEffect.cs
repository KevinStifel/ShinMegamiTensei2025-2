using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view) : base(view) { }

    public UnitBase? ApplySamuraiSummon(UnitBase monsterToSummon, PlayerBoardFormation formation, SummonPlacement placement)
    {
        formation.ActiveBoard[placement.BoardPosition] = monsterToSummon;
        formation.ReserveUnits.Remove(monsterToSummon);

        if (placement.ReplacedUnit != null)
            formation.ReserveUnits.Insert(0, placement.ReplacedUnit);

        EffectView.ShowSummonResult(monsterToSummon);
        return placement.ReplacedUnit;
    }

    public UnitBase ApplyMonsterSummon(SummonData summonData, PlayerBoardFormation formation)
    {
        var summonerPosition = formation.ActiveBoard.First(kvp => ReferenceEquals(kvp.Value, summonData.Summoner)).Key;
        formation.ActiveBoard[summonerPosition] = summonData.MonsterToSummon;

        formation.ReserveUnits.Remove(summonData.MonsterToSummon);
        formation.ReserveUnits.Insert(0, summonData.Summoner);

        EffectView.ShowSummonResult(summonData.MonsterToSummon);
        return summonData.Summoner;
    }

    public override void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillExecutionContext skillExecutionContext)
    {
        throw new NotImplementedException();
    }
}