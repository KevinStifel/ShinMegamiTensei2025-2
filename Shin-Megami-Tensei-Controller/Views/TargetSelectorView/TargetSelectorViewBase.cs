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

    public abstract void ShowAvailableTargets(UnitBase casterUnit, List<UnitBase> candidates);

    public int ReadTargetIndex(int totalOptions)
    {
        return InputValidator.ReadValidatedIndex(View, totalOptions);
    }

    public void ShowSeparator()
    {
        View.WriteLine("----------------------------------------");
    }
}