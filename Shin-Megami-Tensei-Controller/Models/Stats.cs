using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class Stats
{
    // Valores actuales
    public int HP { get; private set; }
    public int MP { get; private set; }

    // Valores máximos
    public int MaxHP { get; }
    public int MaxMP { get; }

    // Stats de combate
    public int Str { get; }
    public int Skl { get; }
    public int Mag { get; }
    public int Spd { get; }
    public int Lck { get; }

    [JsonConstructor]
    public Stats(int hp, int mp, int str, int skl, int mag, int spd, int lck)
    {
        MaxHP = hp;
        MaxMP = mp;
        HP = hp;
        MP = mp;
        Str = str;
        Skl = skl;
        Mag = mag;
        Spd = spd;
        Lck = lck;
    }
    
    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }
}