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
        
        int index = ReadTargetIndex(enemies);
        if (WasCanceledSelection(index))
            throw new ActionCanceledException();
        
        View.WriteLine("----------------------------------------");
        
        UnitBase target = enemies[index];

        string hitsString = skillData.Hits;

        int k = Board.GetSkillUseCount(currentPlayerId);
        int hits = CalculateHits(hitsString, k);
        
        List<UnitBase> repeatedTargets = [];
        for (int i = 0; i < hits; i++)
            repeatedTargets.Add(target);
        return repeatedTargets;
    }

    private static int CalculateHits(string hitsString, int k)
    {
        if (string.IsNullOrWhiteSpace(hitsString))
            return 1;

        if (!hitsString.Contains('-'))
            return int.TryParse(hitsString, out int fixedHits) ? fixedHits : 1;

        var parts = hitsString.Split('-');
        if (parts.Length != 2)
            return 1;

        int a = int.Parse(parts[0]);
        int b = int.Parse(parts[1]);

        int offset = k % (b - a + 1);
        return a + offset;
    }
}
