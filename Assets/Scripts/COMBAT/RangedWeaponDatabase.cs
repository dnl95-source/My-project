using System.Collections.Generic;

public static class RangedWeaponDatabase
{
    public static List<RangedWeapon> Weapons = new List<RangedWeapon>
    {
        new RangedWeapon(
            RangedWeaponType.Bow, "Arco", 30,
            new List<ProjectileType>{ ProjectileType.WoodenArrow, ProjectileType.IronArrow, ProjectileType.PoisonedIronArrow }
        ),
        new RangedWeapon(
            RangedWeaponType.ForestElvenBow, "Arco elfico della foresta", 30,
            new List<ProjectileType>{ ProjectileType.WoodenArrow, ProjectileType.IronArrow, ProjectileType.PoisonedIronArrow },
            new List<RaceType>{ RaceType.ElfNature }
        ),
        new RangedWeapon(
            RangedWeaponType.LongBow, "Arco lungo", 50,
            new List<ProjectileType>{ ProjectileType.IronArrow, ProjectileType.FireArrow }
        ),
        new RangedWeapon(
            RangedWeaponType.Crossbow, "Balestra", 40,
            new List<ProjectileType>{ ProjectileType.IronBolt }
        ),
        new RangedWeapon(
            RangedWeaponType.DwarvenMasterCrossbow, "Balestra dei maestri nani", 50,
            new List<ProjectileType>{ ProjectileType.IronBolt },
            new List<RaceType>{ RaceType.Dwarf }
        ),
        new RangedWeapon(
            RangedWeaponType.ExplosiveGrenade, "Granata esplosiva", 10,
            new List<ProjectileType>{ ProjectileType.Explosive }
        ),
        new RangedWeapon(
            RangedWeaponType.GoblinBomb, "Bomba goblin", 15,
            new List<ProjectileType>{ ProjectileType.Bomb },
            new List<RaceType>{ RaceType.Goblin }
        )
    };
}

public static class ProjectileDatabase
{
    public static List<Projectile> Projectiles = new List<Projectile>
    {
        // Arco
        new Projectile(ProjectileType.WoodenArrow, "Freccia di legno", 3, 7, false, 30),
        new Projectile(ProjectileType.IronArrow, "Freccia di ferro", 7, 14, true),
        new Projectile(ProjectileType.PoisonedIronArrow, "Freccia di ferro avvelenata", 7, 14, true,
            isPoisoned: true, poisonDPS: 2, poisonDuration: 60), // 1 min, max 15 danni (gestione a parte)

        // Arco elfico della foresta
        new Projectile(ProjectileType.WoodenArrow, "Freccia di legno (elfica)", 5, 9, false, 30),
        new Projectile(ProjectileType.IronArrow, "Freccia di ferro (elfica)", 10, 18, true),
        new Projectile(ProjectileType.PoisonedIronArrow, "Freccia di ferro avvelenata (elfica)", 10, 18, true,
            isPoisoned: true, poisonDPS: 2, poisonDuration: 60),

        // Arco lungo
        new Projectile(ProjectileType.IronArrow, "Freccia di ferro (arco lungo)", 9, 15, true),
        new Projectile(ProjectileType.FireArrow, "Freccia infuocata", 10, 18, false, 0,
            isFire: true, fireDPS: 2, fireDuration: 60), // max 25 danni fuoco

        // Balestra
        new Projectile(ProjectileType.IronBolt, "Dardo di ferro", 12, 18, true),

        // Balestra maestri nani
        new Projectile(ProjectileType.IronBolt, "Dardo di ferro (nano)", 15, 20, true),

        // Granata esplosiva
        new Projectile(ProjectileType.Explosive, "Granata esplosiva", 0, 0, false, 0,
            isExplosive: true,
            explosionRadii: new List<ExplosionRadiusDamage> {
                new ExplosionRadiusDamage(4, 25),
                new ExplosionRadiusDamage(5, 15),
                new ExplosionRadiusDamage(6, 7)
            }
        ),
        // Bomba goblin
        new Projectile(ProjectileType.Bomb, "Bomba goblin", 0, 0, false, 0,
            isExplosive: true,
            explosionRadii: new List<ExplosionRadiusDamage> {
                new ExplosionRadiusDamage(5, 35),
                new ExplosionRadiusDamage(7, 28),
                new ExplosionRadiusDamage(10, 15)
            }
        )
    };
}