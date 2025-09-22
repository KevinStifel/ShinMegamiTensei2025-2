namespace Shin_Megami_Tensei
{
    public sealed class ActionOptions
    {
        public IReadOnlyDictionary<string, string> Map { get; }

        public ActionOptions(IReadOnlyDictionary<string, string> map)
        {
            Map = map;
        }

        public string GetActionKeyFor(string menuSelection) => Map[menuSelection]; // asumimos válido
    }
}