namespace Shin_Megami_Tensei;

public class TeamFactory
{
    private readonly UnitRepository _repository;

    public TeamFactory(UnitRepository repository)
    {
        _repository = repository;
    }

    private Samurai BuildSamurai(TeamUnitRaw rawUnit)
    {
        var samuraiDto = _repository.GetSamurai(rawUnit.Name);
        var skills = BuildSkills(rawUnit.SkillNames);

        return new Samurai(
            samuraiDto.Name,
            MapStats(samuraiDto.Stats),
            new Affinity(samuraiDto.Affinity),
            skills
        );
    }


    private Monster BuildMonster(TeamUnitRaw rawUnit)
    {
        var monsterDto = _repository.GetMonster(rawUnit.Name);
        var skills = BuildSkills(monsterDto.Skills);
        
        return new Monster(
            monsterDto.Name,
            MapStats(monsterDto.Stats),
            new Affinity(monsterDto.Affinity),
            skills
        );
    }
    public List<UnitBase> BuildTeam(List<TeamUnitRaw> rawTeam)
    {
        var assembledTeamUnits = new List<UnitBase>();

        foreach (var rawUnit in rawTeam)
        {
            if (rawUnit.IsSamurai)
                assembledTeamUnits.Add(BuildSamurai(rawUnit));
            else
                assembledTeamUnits.Add(BuildMonster(rawUnit));
        }
        return assembledTeamUnits;
    }

    private Stats MapStats(StatsJsonDto dtoStats)
    {
        return new Stats(
            dtoStats.HP,
            dtoStats.MP,
            dtoStats.Str,
            dtoStats.Skl,
            dtoStats.Mag,
            dtoStats.Spd,
            dtoStats.Lck
        );
    }
    
    private List<SkillData> BuildSkills(List<string> skillNames)
    {
        return skillNames
            .Select(skillName => _repository.GetSkill(skillName))
            .Select(skillDto => new SkillData(
                skillDto.Name,
                skillDto.Type,
                skillDto.Cost,
                skillDto.Power,
                skillDto.Target,
                skillDto.Hits,
                skillDto.Effect
            ))
            .ToList();
    }

}
