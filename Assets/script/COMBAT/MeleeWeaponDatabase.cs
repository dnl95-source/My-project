using System.Collections.Generic;

public static class MeleeWeaponDatabase
{
    public static List<MeleeWeapon> Weapons = new List<MeleeWeapon>
    {
        new MeleeWeapon(WeaponType.Knife, "Coltello di ferro", 6, 10, "Nessuno", "Arma piccola, colpire da vicino", new List<RaceType>{ RaceType.Human, RaceType.Orc, RaceType.Goblin, RaceType.ElfNature, RaceType.Dwarf, RaceType.WhiteElf, RaceType.DarkElf, RaceType.Ogre }),
        new MeleeWeapon(WeaponType.ForestDefenderDagger, "Daga dei difensori del bosco", 13, 18, "Veloce e agile, più colpi", "Arma piccola, colpire da vicino", new List<RaceType>{ RaceType.ElfNature }),
        new MeleeWeapon(WeaponType.Dagger, "Daga", 10, 15, "Veloce e agile, più colpi", "Arma piccola, colpire da vicino", new List<RaceType>{ RaceType.Human, RaceType.Goblin }),
        new MeleeWeapon(WeaponType.ShortSword, "Spada corta", 15, 20, "Veloce e agile, più colpi", "Arma piccola, colpire da vicino", new List<RaceType>{ RaceType.Human, RaceType.Goblin }),
        new MeleeWeapon(WeaponType.Sword, "Spada", 18, 22, "Colpire da distanza intermedia", "Due fendenti a volta", new List<RaceType>{ RaceType.Human }),
        new MeleeWeapon(WeaponType.SacredKnightSword, "Spada sacra del cavaliere", 24, 30, "Lunga, colpo critico maggiore", "Due fendenti a volta", new List<RaceType>{ RaceType.Human }),
        new MeleeWeapon(WeaponType.BlackTemplarSword, "Spada nera del templare", 24, 30, "Lunga, colpo critico maggiore", "Due fendenti a volta", new List<RaceType>{ RaceType.Human }),
        new MeleeWeapon(WeaponType.LongSword, "Spada lunga", 24, 28, "Colpire da distanza avanzata", "Un colpo alla volta, ricarica maggiore", new List<RaceType>{ RaceType.Human, RaceType.Orc }),
        new MeleeWeapon(WeaponType.Spear, "Lancia", 23, 30, "Colpire da distanze lunghe", "Un colpo alla volta", new List<RaceType>{ RaceType.Human, RaceType.Orc, RaceType.Goblin }),
        new MeleeWeapon(WeaponType.ElvenSword, "Spada elfica", 22, 30, "Colpire da distanza intermedia, critico maggiore", "Due fendenti a volta", new List<RaceType>{ RaceType.WhiteElf }),
        new MeleeWeapon(WeaponType.BerserkerAxe, "Ascia del berserker", 25, 32, "Colpire da distanza intermedia, critico maggiore", "Un fendente a volta", new List<RaceType>{ RaceType.Orc }),
        new MeleeWeapon(WeaponType.RhonkHammer, "Martello Rhonk", 20, 30, "Critico maggiore", "Due fendenti, distanza ravvicinata", new List<RaceType>{ RaceType.Dwarf }),
        new MeleeWeapon(WeaponType.SlaverWhip, "Frusta dello schiavista", 10, 10, "Aumenta obbedienza", "Nessuno", new List<RaceType>{ RaceType.Human, RaceType.Orc, RaceType.Goblin }),
    };
}