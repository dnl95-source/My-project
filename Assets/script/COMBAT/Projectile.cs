using System.Collections.Generic;

public class Projectile
{
    public ProjectileType Type;
    public string Name;
    public float BaseDamage;
    public float CritDamage;
    public bool HeadshotFatal; // true = headshot uccide
    public float HeadshotExtraDamage; // se non fatale
    public bool IsPoisoned;
    public float PoisonDPS;
    public float PoisonDuration; // in secondi
    public bool IsFire;
    public float FireDPS;
    public float FireDuration; // in secondi
    public bool IsExplosive;
    public List<ExplosionRadiusDamage> ExplosionRadii; // per bombe/granate

    public Projectile(ProjectileType type, string name, float baseDamage, float critDamage, bool headshotFatal = false, float headshotExtraDamage = 0,
        bool isPoisoned = false, float poisonDPS = 0, float poisonDuration = 0, bool isFire = false, float fireDPS = 0, float fireDuration = 0,
        bool isExplosive = false, List<ExplosionRadiusDamage> explosionRadii = null)
    {
        Type = type;
        Name = name;
        BaseDamage = baseDamage;
        CritDamage = critDamage;
        HeadshotFatal = headshotFatal;
        HeadshotExtraDamage = headshotExtraDamage;
        IsPoisoned = isPoisoned;
        PoisonDPS = poisonDPS;
        PoisonDuration = poisonDuration;
        IsFire = isFire;
        FireDPS = fireDPS;
        FireDuration = fireDuration;
        IsExplosive = isExplosive;
        ExplosionRadii = explosionRadii ?? new List<ExplosionRadiusDamage>();
    }
}