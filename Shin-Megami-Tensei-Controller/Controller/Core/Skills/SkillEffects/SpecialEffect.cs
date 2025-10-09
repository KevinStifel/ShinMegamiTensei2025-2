using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager board)
    {
     // ✨ 2️⃣ Delegar la lógica de invocación al SummonEffect
        var summonEffect = new SummonEffect(View);

        summonEffect.ApplyEffect(
            caster: caster,
            targets: targets,
            skillData: skillData,
            turnManager: turnManager,
            currentPlayerId: currentPlayerId,
            board: board
        );

        // ✨ 3️⃣ (Opcional) — si Sabbatma debe hacer algo extra, puedes hacerlo aquí.
        // Por ejemplo, si el monstruo invocado ataca inmediatamente:
        /*
        var summonedUnit = targets.FirstOrDefault();
        if (summonedUnit != null)
        {
            var actionView = new CombatActionView(View);
            actionView.ShowAvailableTargets(summonedUnit, board.GetAliveUnits(GetEnemyPlayerId(currentPlayerId)));
            // ... podrías encadenar aquí un ataque automático si lo pide el flujo
        }
        */
    }
}