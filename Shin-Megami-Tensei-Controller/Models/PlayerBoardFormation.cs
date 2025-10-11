using System.Collections.Generic;

namespace Shin_Megami_Tensei;

public sealed record PlayerBoardFormation(
    Dictionary<string, UnitBase?> ActiveBoard,
    List<UnitBase> ReserveUnits);
    
    