using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class AbstractView(View view)
{
    protected readonly View View = view;
    public string ReadUserSelection() =>
        View.ReadLine();
    protected static bool IsCancelOption(int zeroBasedIndex, int itemsCount)
        => zeroBasedIndex == itemsCount;

    public void ShowSeparator()
        => View.WriteLine("----------------------------------------");
}
