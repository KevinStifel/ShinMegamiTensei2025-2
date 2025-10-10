using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        BattleFlowContext battleFlowContext)
    {
        var boardManager = battleFlowContext.BoardManager;
        var turnManager = battleFlowContext.TurnManager;
        int currentPlayerId = battleFlowContext.CurrentPlayerId;

        var monsterToSummon = targets.First();
        var summonEffect = new SummonEffect(View);

        // Recupera la información preparada del tablero (posición y unidad reemplazada)
        var (boardPosition, replacedUnit) = boardManager.GetPreparedSummonData(currentPlayerId);
        var placement = new SummonPlacement(boardPosition, replacedUnit);

        var playerBoardFormation = new PlayerBoardFormation(
            boardManager.SelectMutableBoard(currentPlayerId),
            boardManager.GetReserveUnitsForPlayer(currentPlayerId)
        );

        summonEffect.ApplySamuraiSummon(monsterToSummon, playerBoardFormation, placement);

        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, replacedUnit);

        var turnChange = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(turnChange);
    }
}