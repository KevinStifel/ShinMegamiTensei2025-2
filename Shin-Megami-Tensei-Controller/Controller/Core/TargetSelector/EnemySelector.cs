using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnemySelector : TargetSelectorBase
{
    public EnemySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new EnemySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> enemies = Board.GetAliveUnits(enemyPlayerId);
        
        SelectorView.ShowAvailableTargets(activeUnit, enemies);
        
        int selectedIndex = ReadTargetIndex(enemies);
        if (IsSelectionCanceled(selectedIndex))
            throw new ActionCanceledException();
        
        SelectorView.ShowSeparator();
        
        UnitBase selectedTarget  = enemies[selectedIndex];
        
        int totalHits = GetTotalHits(skillData, currentPlayerId);
        return BuildRepeatedTargetList(selectedTarget, totalHits);
    }
    
    private int GetTotalHits(SkillData skillData, int currentPlayerId)
    {
        string hitsPattern = skillData.Hits;
        int skillUseCount = Board.GetSkillUseCount(currentPlayerId);
        return HitCalculator.CalculateHits(hitsPattern, skillUseCount);
    }

    private static List<UnitBase> BuildRepeatedTargetList(UnitBase targetUnit, int hitCount)
    {
        List<UnitBase> repeatedTargets = [];
        for (int hitIndex = 0; hitIndex < hitCount; hitIndex++)
            repeatedTargets.Add(targetUnit);
        return repeatedTargets;
    }
    
}
