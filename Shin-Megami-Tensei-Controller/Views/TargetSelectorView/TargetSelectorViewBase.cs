using Shin_Megami_Tensei;
using System.Collections.Generic;

namespace Shin_Megami_Tensei_View;

public abstract class TargetSelectorViewBase
{
    protected readonly View View;

    protected TargetSelectorViewBase(View view)
    {
        View = view;
    }

    public abstract void ShowAvailableTargets(UnitBase caster, List<UnitBase> candidates);
    private string ReadUserSelection() => View.ReadLine();

    public virtual int ReadTargetIndex(int totalOptions)
    {
        string input = ReadUserSelection();

        if (!int.TryParse(input, out int index))
            return -1;

        index -= 1;
        return index >= 0 && index < totalOptions ? index : -1;
    }

    public void ShowSeparator()
        => View.WriteLine("----------------------------------------");
}