using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnemySelector : TargetSelectorBase
{
    public EnemySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new EnemySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase caster, int currentPlayerId, SkillData skillData)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> enemies = Board.GetAliveUnits(enemyPlayerId);
        
        SelectorView.ShowAvailableTargets(caster, enemies);
        
        int selectedIndex  = ReadTargetIndex(enemies);
        if (WasCanceledSelection(selectedIndex))
            throw new ActionCanceledException();
        
        SelectorView.ShowSeparator();
        
        UnitBase selectedTarget  = enemies[selectedIndex];

        string hitsPattern = skillData.Hits;

        int skillUsageCount = Board.GetSkillUseCount(currentPlayerId);
        int totalHits = CalculateHits(hitsPattern, skillUsageCount);
        
        List<UnitBase> repeatedTargets = [];
        for (int hitIndex = 0; hitIndex < totalHits; hitIndex++)
            repeatedTargets.Add(selectedTarget);
        
        return repeatedTargets;
    }

    private static int CalculateHits(string hitsPattern, int skillUsageCount)
    {
        if (string.IsNullOrWhiteSpace(hitsPattern))
            return 1;

        bool isFixedHitValue = !hitsPattern.Contains('-');
        if (isFixedHitValue)
            return int.TryParse(hitsPattern, out int fixedHitCount) ? fixedHitCount : 1;

        string[] rangeParts = hitsPattern.Split('-');
        bool isInvalidRangeFormat = rangeParts.Length != 2;
        if (isInvalidRangeFormat)
            return 1;

        int minHits = int.Parse(rangeParts[0]);
        int maxHits = int.Parse(rangeParts[1]);
        int rangeWidth = maxHits - minHits + 1;

        int offset = skillUsageCount % rangeWidth;
        return minHits + offset;
    }
}
