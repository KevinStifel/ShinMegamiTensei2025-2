using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class FileSelectorView : AbstractView
{
    public FileSelectorView(View view) : base(view) { }

    public void ShowTitle() =>
        _view.WriteLine("Elige un archivo para cargar los equipos");

    public void ShowFileOption(int index, string fileName) =>
        _view.WriteLine($"{index}: {fileName}");

    public string ReadUserSelection() =>
        _view.ReadLine();
}