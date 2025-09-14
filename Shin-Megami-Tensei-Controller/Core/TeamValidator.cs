using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class TeamValidator
{
    private readonly TeamValidatorView _teamValidatorView;

    public TeamValidator(View view)
    {
        _teamValidatorView = new TeamValidatorView(view);
    }

    public bool ValidateRawTeams(List<TeamUnitRaw> playerOneRawTeam, List<TeamUnitRaw> playerTwoRawTeam)
    {
        if (ValidateTeam(playerOneRawTeam) && ValidateTeam(playerTwoRawTeam))
            return true;

        _teamValidatorView.ShowInvalidTeams();
        return false;
    }

    private bool ValidateTeam(List<TeamUnitRaw> units) =>
        HasSamurai(units) &&
        OnlyOneSamurai(units) &&
        MaxUnits(units) &&
        NoDuplicateUnits(units) &&
        SamuraiMaxSkills(units) &&
        NoDuplicateSamuraiSkills(units);

    private bool HasSamurai(List<TeamUnitRaw> units)
    {
        return units.Any(unit => unit.IsSamurai);
    }

    private bool OnlyOneSamurai(List<TeamUnitRaw> units)
    {
        return units.Count(unit => unit.IsSamurai) == 1;
    }

    private bool MaxUnits(List<TeamUnitRaw> units)
    {
        return units.Count <= GameConstants.MaxUnitsPerTeam;
    }

    private bool NoDuplicateUnits(List<TeamUnitRaw> units)
    {
        return units
            .Select(unit => unit.Name)
            .Distinct()
            .Count() == units.Count;
    }

    private bool SamuraiMaxSkills(List<TeamUnitRaw> units)
    {
        return units
            .Where(unit => unit.IsSamurai)
            .All(samurai => samurai.SkillNames.Count <= GameConstants.MaxSkillsPerSamurai);
    }

    private bool NoDuplicateSamuraiSkills(List<TeamUnitRaw> units)
    {
        return units
            .Where(unit => unit.IsSamurai)
            .All(samurai => samurai.SkillNames.Distinct().Count() == samurai.SkillNames.Count);
    }
}