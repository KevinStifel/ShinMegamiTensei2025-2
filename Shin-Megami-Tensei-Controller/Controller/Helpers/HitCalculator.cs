namespace Shin_Megami_Tensei;

public static class HitCalculator
{
    public static int CalculateHits(string hitsPattern, int skillUseCount)
    {
        if (IsEmptyPattern(hitsPattern))
            return 1;

        if (IsFixedHitPattern(hitsPattern))
            return ParseFixedHits(hitsPattern);

        return ParseRangeHits(hitsPattern, skillUseCount);
    }
    
    private static bool IsEmptyPattern(string hitsPattern)
        => string.IsNullOrWhiteSpace(hitsPattern);

    private static bool IsFixedHitPattern(string hitsPattern)
        => !hitsPattern.Contains('-');

    private static int ParseFixedHits(string hitsPattern)
        => int.TryParse(hitsPattern, out int fixedHitCount) ? fixedHitCount : 1;

    private static int ParseRangeHits(string hitsPattern, int skillUseCount)
    {
        string[] rangeParts = hitsPattern.Split('-');
        bool hasValidRangeFormat = rangeParts.Length == 2;
        if (!hasValidRangeFormat)
            return 1;

        int minHits = int.Parse(rangeParts[0]);
        int maxHits = int.Parse(rangeParts[1]);
        int rangeSpan = maxHits - minHits + 1;

        int offset = skillUseCount % rangeSpan;
        return minHits + offset;
    }
}