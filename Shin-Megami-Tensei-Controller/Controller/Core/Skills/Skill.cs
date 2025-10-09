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
        PlayerRegistry.RegisterPlayer(currentPlayerId);
        int k = PlayerRegistry.GetSkillCount(currentPlayerId);
        int hits = CalculateHits(_skillData.Hits, k);
        
        ApplySkillMultipleTimes(hits, caster, target);
        
        PlayerRegistry.IncrementSkillCount(currentPlayerId);
    }
    private void ApplySkillMultipleTimes(int hits, UnitBase caster, UnitBase target)
    {
        _effect.SetRemainingHits(hits);
        for (int i = 0; i < hits; i++)
        {
            _effect.ApplyEffect(caster, target, _skillData);
        }
    }

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

        //Console.WriteLine($"[DEBUG] Hits calculados: {result} (A={a}, B={b}, K={k}, Offset={offset})");
        return result;
    }
}