using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

/// <summary>
/// Representa el contexto de combate actual.  
/// Contiene referencias comunes que las acciones, habilidades y efectos necesitan.
/// </summary>
public sealed class BattleContext
{
    public int CurrentPlayerId { get; }
    public BoardManager BoardManager { get; }
    public TurnManager TurnManager { get; }
    public View View { get; }
    public CombatActionView ActionView { get; }

    public BattleContext(int currentPlayerId, BoardManager boardManager, TurnManager turnManager, View view)
    {
        CurrentPlayerId = currentPlayerId;
        BoardManager = boardManager;
        TurnManager = turnManager;
        View = view;
        ActionView = new CombatActionView(view);
    }

    public UnitBase GetAttackingUnit() => TurnManager.GetAttackerOnTurn();
    public int GetEnemyPlayerId() => CurrentPlayerId == 1 ? 2 : 1;
}