using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Skill
{
    private readonly SkillData _skillData;
    private readonly EffectBase _effect;
    private readonly TargetSelectorBase _targetSelector;

    public Skill(SkillData skillData, EffectBase effect, TargetSelectorBase targetSelector)
    {
        _skillData = skillData;
        _effect = effect;
        _targetSelector = targetSelector;
    }

    public void Apply(UnitBase caster, int currentPlayerId, BoardManager board, TurnManager turnManager, View view)
    {
        // 1️⃣ Seleccionar objetivo
        UnitBase? target = _targetSelector.SelectTarget(caster, currentPlayerId);
        if (target == null)
            throw new ActionCanceledException();

        // 2️⃣ Registrar jugador y calcular hits
        PlayerRegistry.RegisterPlayer(currentPlayerId);
        int k = PlayerRegistry.GetSkillCount(currentPlayerId);
        int hits = CalculateHits(_skillData.Hits, k);

        _effect.SetRemainingHits(hits);

        // 3️⃣ Aplicar todos los hits
        for (int i = 0; i < hits; i++)
        {
            _effect.ApplyEffect(caster, target, _skillData);
        }

        PlayerRegistry.IncrementSkillCount(currentPlayerId);

        // 4️⃣ Calcular afinidad
        var element = AffinityMapper.Parse(_skillData.Type);
        var reaction = target.Affinity.GetAffinityReaction(element);
        var behavior = AffinityBehaviorFactory.Create(reaction);

        // meter dentro de effect
        var delta = turnManager.ApplyAffinityTurnEffect(behavior);
        view.WriteLine("----------------------------------------");
        view.WriteLine($"Se han consumido {delta.ConsumedFull} Full Turn(s) y {delta.ConsumedBlinking} Blinking Turn(s)");
        view.WriteLine($"Se han obtenido {delta.GainedBlinking} Blinking Turn(s)");

        // 6️⃣ Verificar muerte y limpiar del tablero si es necesario
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1; 
        HandleDeathIfNeeded(board, enemyPlayerId, target);
    }

    private static int CalculateHits(string hitsString, int k)
    {
        if (string.IsNullOrWhiteSpace(hitsString))
            return 1;

        // Caso fijo (por ejemplo "2")
        if (!hitsString.Contains('-'))
            return int.TryParse(hitsString, out int fixedHits) ? fixedHits : 1;

        // Caso variable (por ejemplo "1-3")
        var parts = hitsString.Split('-');
        if (parts.Length != 2) return 1;

        int a = int.Parse(parts[0]);
        int b = int.Parse(parts[1]);

        int offset = k % (b - a + 1);
        int result = a + offset;

        return result;
    }
    protected static void HandleDeathIfNeeded(BoardManager board, int enemyPlayerId, UnitBase target)
    {
        if (target.Stats.HP == 0)
        {
            board.HandleUnitDeath(enemyPlayerId, target);
        }
    }
}
