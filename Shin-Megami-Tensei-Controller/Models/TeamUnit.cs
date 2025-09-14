namespace Shin_Megami_Tensei;

public class TeamUnit
{
    public Samurai Samurai { get; }
    public List<Monster> Monsters { get; }

    public TeamUnit(Samurai samurai, List<Monster> monsters)
    {
        Samurai = samurai;
        Monsters = monsters;
    }
}