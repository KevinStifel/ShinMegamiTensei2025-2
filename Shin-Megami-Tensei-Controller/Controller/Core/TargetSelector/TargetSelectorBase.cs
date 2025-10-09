using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class TargetSelectorBase
{
    protected readonly View View;
    protected readonly BoardManager Board;

    protected TargetSelectorBase(View view, BoardManager board)
    {
        View = view;
        Board = board;
    }

    public UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        var candidates = GetCandidates(caster, currentPlayerId);

        if (candidates == null || candidates.Count == 0)
            return null;

        int index = View.ReadTargetIndex(candidates);
        return index < 0 ? null : candidates[index];
    }

    // 🔹 Método abstracto que define qué unidades se muestran
    protected abstract List<UnitBase> GetCandidates(UnitBase caster, int currentPlayerId);
}