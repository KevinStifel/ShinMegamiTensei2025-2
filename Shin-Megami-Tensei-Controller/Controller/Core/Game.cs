using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Game
{
    private readonly BattleManager _battleManager;
    private readonly TeamValidator _teamValidator;
    private readonly List<TeamUnitRaw> _playerOneRawTeam;
    private readonly List<TeamUnitRaw> _playerTwoRawTeam;

    public Game(View view, string teamsFolder)
    {
        var fileSelector = new FileSelector(view, teamsFolder);
        var teamFileLoader = new TeamFileLoader();
        var repository = new UnitRepository();
        var teamFactory = new TeamFactory(repository);
        _teamValidator = new TeamValidator(view);

        var selectedTeamFilePath = fileSelector.SelectTeamFilePath();
        (_playerOneRawTeam, _playerTwoRawTeam) = teamFileLoader.LoadRawTeams(selectedTeamFilePath);

        var playerOneUnitList = teamFactory.BuildTeam(_playerOneRawTeam);
        var playerTwoUnitList = teamFactory.BuildTeam(_playerTwoRawTeam);

        var board = new Board(playerOneUnitList, playerTwoUnitList);
        _battleManager = new BattleManager(board, view);
    }

    public void Play()
    {
        if (!_teamValidator.AreRawTeamsValid(_playerOneRawTeam, _playerTwoRawTeam)) return;
        _battleManager.StartBattle();
    }
}