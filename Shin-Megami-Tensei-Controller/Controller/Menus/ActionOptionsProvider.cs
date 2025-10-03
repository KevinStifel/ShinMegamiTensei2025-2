using System.Collections.Generic;

namespace Shin_Megami_Tensei
{
    public sealed class ActionOptionsProvider
    {
        private static readonly IReadOnlyDictionary<string, string> SamuraiMap =
            new Dictionary<string, string>
            {
                { "1", "attack" },
                { "2", "shoot" },
                { "3", "skill" },
                { "4", "summon" },
                { "5", "pass" },
                { "6", "surrender" }
            };

        private static readonly IReadOnlyDictionary<string, string> MonsterMap =
            new Dictionary<string, string>
            {
                { "1", "attack" },
                { "2", "skill" },
                { "3", "summon" },
                { "4", "pass" }
            };

        public static ActionOptions CreateMenuOptions(UnitBase unit)
            => unit is Samurai ? new ActionOptions(SamuraiMap)
                : new ActionOptions(MonsterMap);
    }
}