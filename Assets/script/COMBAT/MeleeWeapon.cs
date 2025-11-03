using System.Collections.Generic;

public class MeleeWeapon
{
    public WeaponType Type;
    public string Name;
    public float BaseDamage;
    public float CritDamage;
    public string Bonus;
    public string Malus;
    public List<RaceType> PreferredBy;

    public MeleeWeapon(WeaponType type, string name, float baseDamage, float critDamage, string bonus, string malus, List<RaceType> preferredBy = null)
    {
        Type = type;
        Name = name;
        BaseDamage = baseDamage;
        CritDamage = critDamage;
        Bonus = bonus;
        Malus = malus;
        PreferredBy = preferredBy ?? new List<RaceType>();
    }
}