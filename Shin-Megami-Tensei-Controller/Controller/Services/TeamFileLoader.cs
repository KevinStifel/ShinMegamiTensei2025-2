namespace Shin_Megami_Tensei;
public class TeamFileLoader
{
    public (List<TeamUnitRaw> playerOneRawTeam, List<TeamUnitRaw> playerTwoRawTeam) LoadRawTeams(string filePath)
    {
        var playerOneRawTeam = new List<TeamUnitRaw>();
        var playerTwoRawTeam = new List<TeamUnitRaw>();
        int currentPlayer = 0;

        foreach (var line in ReadLines(filePath))
        {
            if (IsHeader(line, "Player 1 Team")) { currentPlayer = 1; continue; }
            if (IsHeader(line, "Player 2 Team")) { currentPlayer = 2; continue; }
            if (string.IsNullOrWhiteSpace(line)) continue;

            AddUnitToCorrectTeam(line, currentPlayer, playerOneRawTeam, playerTwoRawTeam);
        }

        return (playerOneRawTeam, playerTwoRawTeam);
    }
    private IEnumerable<string> ReadLines(string filePath) =>
        File.ReadLines(filePath).Select(fileLine => fileLine.Trim());

    private bool IsHeader(string line, string expectedHeader) =>
        line.Equals(expectedHeader, StringComparison.OrdinalIgnoreCase);

    private void AddUnitToCorrectTeam(
        string line,
        int currentPlayer,
        List<TeamUnitRaw> player1Team,
        List<TeamUnitRaw> player2Team
    )
    {
        TeamUnitRaw teamUnit = line.StartsWith("[Samurai]")
            ? ParseSamurai(line)
            : new TeamUnitRaw(false, line, new List<string>());

        if (currentPlayer == 1) player1Team.Add(teamUnit);
        if (currentPlayer == 2) player2Team.Add(teamUnit);
    }

    private TeamUnitRaw ParseSamurai(string samuraiLine)
    {
        var samuraiContent = samuraiLine.Replace("[Samurai]", "").Trim();

        if (!samuraiContent.Contains('('))
            return new TeamUnitRaw(true, samuraiContent, new List<string>());

        var samuraiName = samuraiContent[..samuraiContent.IndexOf('(')].Trim();
        var skillsText = samuraiContent[(samuraiContent.IndexOf('(') + 1)..].TrimEnd(')');
        var skillNames = skillsText.Split(',').Select(skillName => skillName.Trim()).ToList();

        return new TeamUnitRaw(true, samuraiName, skillNames);
    }
}
