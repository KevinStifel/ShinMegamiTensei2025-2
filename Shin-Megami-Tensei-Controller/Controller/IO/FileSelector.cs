using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class FileSelector
{
    private readonly FileSelectorView _fileSelectorView;
    private readonly string _teamsFolder;

    public FileSelector(View view, string teamsFolder)
    {
        _fileSelectorView = new FileSelectorView(view);
        _teamsFolder = teamsFolder;
    }

    public string SelectTeamFilePath()
    {
        _fileSelectorView.ShowTitle();

        var teamFiles = Directory.GetFiles(_teamsFolder, "*.txt");

        for (int i = 0; i < teamFiles.Length; i++)
        {
            string fileName = Path.GetFileName(teamFiles[i]);
            _fileSelectorView.ShowFileOption(i, fileName);
        }

        string input = _fileSelectorView.ReadUserSelection();
        int selectedIndex = int.Parse(input);
        return teamFiles[selectedIndex];
    }
}