using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Skill
{
    private readonly SkillData _skillData;
    private readonly EffectBase _effect;

    public Skill(SkillData skillData, EffectBase effect)
    {
        _skillData = skillData;
        _effect = effect;
    }

    public void Apply(UnitBase caster, UnitBase target, int currentPlayerId)
    {
        // Asegurar registro del jugador
        PlayerRegistry.RegisterPlayer(currentPlayerId);

        // Obtener el contador K actual
        int k = PlayerRegistry.GetSkillCount(currentPlayerId);

        // Calcular cantidad de hits basada en la regla del enunciado
        int hits = CalculateHits(_skillData.Hits, k);
        Console.WriteLine($"[DEBUG] {_skillData.Name} realizará {hits} hit(s) (K={k})");

        // Aplicar daño o efecto tantas veces como hits
        ApplySkillMultipleTimes(hits, caster, target);
        
        // Incrementar el contador global del jugador
        PlayerRegistry.IncrementSkillCount(currentPlayerId);
    }

    /// <summary>
    /// Aplica la habilidad varias veces según la cantidad de hits calculada.
    /// </summary>
    private void ApplySkillMultipleTimes(int hits, UnitBase caster, UnitBase target)
    {
        for (int i = 0; i < hits; i++)
        {
            _effect.ApplyEffect(caster, target, _skillData);
        }
    }

    /// <summary>
    /// Calcula el número de hits en base al formato del campo Hits (por ejemplo, "1-3" o "2").
    /// </summary>
    private int CalculateHits(string hitsString, int k)
    {
        if (string.IsNullOrWhiteSpace(hitsString))
            return 1;

        // Caso fijo: "2"
        if (!hitsString.Contains('-'))
            return int.TryParse(hitsString, out int fixedHits) ? fixedHits : 1;

        // Caso variable: "1-3"
        var parts = hitsString.Split('-');
        if (parts.Length != 2) return 1;

        int a = int.Parse(parts[0]);
        int b = int.Parse(parts[1]);

        int offset = k % (b - a + 1);
        int result = a + offset;

        Console.WriteLine($"[DEBUG] Hits calculados: {result} (A={a}, B={b}, K={k}, Offset={offset})");
        return result;
    }
}