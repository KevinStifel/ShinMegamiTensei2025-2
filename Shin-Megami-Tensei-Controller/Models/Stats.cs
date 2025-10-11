using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class Stats
{
    public int HP { get; private set; }
    public int MP { get; private set; }

    public int MaxHP { get; }
    public int MaxMP { get; }

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

    public bool HasEnoughMP(int cost)
    {
        return MP >= cost;
    }

    public void UseMP(int cost)
    {
        MP = Math.Max(0, MP - cost);
    }

    public void RestoreMP(int amount)
    {
        MP = Math.Min(MaxMP, MP + amount);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }

    public void Heal(int amount)
    {
        HP = Math.Min(MaxHP, HP + amount);
    }
    public override string ToString()
    {
        return $"HP:{HP}/{MaxHP} MP:{MP}/{MaxMP}";
    }
}