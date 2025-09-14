using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Game
{
    private readonly BattleManager _battleManager;

    public Game(View view, string teamsFolder)
    {
        // Inicializamos variables para el contexto del juego
        var fileSelector = new FileSelector(view, teamsFolder);
        var teamFileLoader = new TeamFileLoader();
        var teamValidator = new TeamValidator(view);
        var repository = new UnitRepository();
        var teamFactory = new TeamFactory(repository);

        var selectedTeamFilePath = fileSelector.SelectTeamFilePath();
        var (playerOneRawTeam, playerTwoRawTeam) = teamFileLoader.LoadRawTeams(selectedTeamFilePath);

        if (!teamValidator.ValidateRawTeams(playerOneRawTeam, playerTwoRawTeam)) return;
        //DebugPrinter.PrintTeam("Player 1 Team", playerOneUnits);
        //DebugPrinter.PrintTeam("Player 2 Team", playerTwoUnits);

        var playerOneUnitList = teamFactory.BuildTeam(playerOneRawTeam);
        var playerTwoUnitList = teamFactory.BuildTeam(playerTwoRawTeam);

        var board = new Board(playerOneUnitList, playerTwoUnitList);
        _battleManager = new BattleManager(board, playerOneUnitList, playerTwoUnitList, view);
    }

    public void Play()
    {
        _battleManager.StartBattle();
    }
}