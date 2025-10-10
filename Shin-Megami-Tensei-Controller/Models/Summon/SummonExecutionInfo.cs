// Models/SummonExecutionInfo.cs
using System.Collections.Generic;

namespace Shin_Megami_Tensei;

public sealed record SummonExecutionInfo(
    SummonData SummonData,
    PlayerBoardFormation PlayerBoardFormation,
    SummonPlacement Placement);