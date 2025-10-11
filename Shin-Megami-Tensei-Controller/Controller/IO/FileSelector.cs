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

        for (int index = 0; index < teamFiles.Length; index++)
        {
            string fileName = Path.GetFileName(teamFiles[index]);
            _fileSelectorView.ShowFileOption(index, fileName);
        }

        string input = _fileSelectorView.ReadUserSelection();
        int selectedIndex = int.Parse(input);
        return teamFiles[selectedIndex];
    }
}