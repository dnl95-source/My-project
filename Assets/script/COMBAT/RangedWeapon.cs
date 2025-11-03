using System.Collections.Generic;

public class RangedWeapon
{
    public RangedWeaponType Type;
    public string Name;
    public float MaxRange; // metri
    public List<ProjectileType> SupportedProjectiles;
    public List<RaceType> ExclusiveTo; // opzionale

    public RangedWeapon(RangedWeaponType type, string name, float maxRange, List<ProjectileType> supportedProjectiles, List<RaceType> exclusiveTo = null)
    {
        Type = type;
        Name = name;
        MaxRange = maxRange;
        SupportedProjectiles = supportedProjectiles;
        ExclusiveTo = exclusiveTo ?? new List<RaceType>();
    }
}