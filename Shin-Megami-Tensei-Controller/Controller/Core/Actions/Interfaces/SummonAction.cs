using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
    {
        var summoner = turnManager.GetAttackerOnTurn();

        var summonSkill = new SkillData(
            name: "Summon",
            type: "Special",
            cost: 0,
            power: 0,
            target: "Self",
            hits: "1",
            effect: "Summon"
        );

        var summonEffect = new SummonEffect(View);

        summonEffect.ApplyEffect(
            caster: summoner,
            targets: [],
            skillData: summonSkill,
            turnManager: turnManager,
            currentPlayerId: currentPlayerId,
            board: board
        );
    }
}