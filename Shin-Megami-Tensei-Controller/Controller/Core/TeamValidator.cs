using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class TeamValidator
{
    private readonly TeamValidatorView _teamValidatorView;

    public TeamValidator(View view)
    {
        _teamValidatorView = new TeamValidatorView(view);
    }

    public bool AreRawTeamsValid(List<TeamUnitRaw> playerOneRawTeam, List<TeamUnitRaw> playerTwoRawTeam)
    {
        if (IsTeamValid(playerOneRawTeam) && IsTeamValid(playerTwoRawTeam))
            return true;

        _teamValidatorView.ShowInvalidTeams();
        return false;
    }

    private bool IsTeamValid(List<TeamUnitRaw> teamUnits) =>
        HasSamuraiUnit(teamUnits) &&
        HasExactlyOneSamurai(teamUnits) &&
        IsWithinMaxUnitLimit(teamUnits) &&
        HasUniqueUnitNames(teamUnits) &&
        IsWithinSamuraiSkillLimit(teamUnits) &&
        HasUniqueSamuraiSkills(teamUnits);

    private bool HasSamuraiUnit(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits.Any(unit => unit.IsSamurai);
    }

    private bool HasExactlyOneSamurai(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits.Count(unit => unit.IsSamurai) == 1;
    }

    private bool IsWithinMaxUnitLimit(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits.Count <= GameConstants.MaxUnitsPerTeam;
    }

    private bool HasUniqueUnitNames(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits
            .Select(unit => unit.Name)
            .Distinct()
            .Count() == teamUnits.Count;
    }

    private bool IsWithinSamuraiSkillLimit(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits
            .Where(unit => unit.IsSamurai)
            .All(samurai => samurai.SkillNames.Count <= GameConstants.MaxSkillsPerSamurai);
    }

    private bool HasUniqueSamuraiSkills(List<TeamUnitRaw> teamUnits)
    {
        return teamUnits
            .Where(unit => unit.IsSamurai)
            .All(samurai => samurai.SkillNames.Distinct().Count() == samurai.SkillNames.Count);
    }
}