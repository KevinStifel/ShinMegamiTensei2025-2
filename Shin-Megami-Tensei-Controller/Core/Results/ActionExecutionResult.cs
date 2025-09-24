public readonly struct ActionExecutionResult
{
    private ActionExecutionResult(ActionFlow flow)
    {
        Flow = flow;
    }

    private ActionFlow Flow { get; }
    public bool WasCancelled   => Flow == ActionFlow.StayInMenu;
    public bool DidAdvanceTurn => Flow == ActionFlow.AdvanceTurn;

    public static ActionExecutionResult StayInMenu()  => new(ActionFlow.StayInMenu);
    public static ActionExecutionResult AdvanceTurn() => new(ActionFlow.AdvanceTurn);
    public static ActionExecutionResult NoEffect()    => new(ActionFlow.NoEffect);
}

internal enum ActionFlow
{
    StayInMenu = 0,
    AdvanceTurn = 1,
    NoEffect = 2
}
