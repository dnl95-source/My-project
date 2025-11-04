using System.Collections.Generic;

public static class RacialDamageTable
{
    public static Dictionary<RaceType, Dictionary<RaceType, float>> Damage = new Dictionary<RaceType, Dictionary<RaceType, float>>
    {
        { RaceType.Orc, new Dictionary<RaceType, float> {
            { RaceType.Dwarf, 10 }, { RaceType.ElfNature, 12 }, { RaceType.WhiteElf, 9 },
            { RaceType.Human, 7 }, { RaceType.Orc, 3 }, { RaceType.Goblin, 12 },
            { RaceType.DarkElf, 5 }, { RaceType.Ogre, 2 }
        }},
        { RaceType.ElfNature, new Dictionary<RaceType, float> {
            { RaceType.Orc, 0.2f }, { RaceType.Dwarf, 5 }, { RaceType.Goblin, 2 }, { RaceType.Human, 1 },
            { RaceType.WhiteElf, 0.7f }, { RaceType.DarkElf, 0.3f }, { RaceType.Ogre, 0.1f }
        }},
        { RaceType.Goblin, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 6 }, { RaceType.Dwarf, 4 }, { RaceType.WhiteElf, 3 },
            { RaceType.Human, 4 }, { RaceType.Orc, 1 }, { RaceType.Goblin, 5 },
            { RaceType.DarkElf, 1.2f }, { RaceType.Ogre, 0.3f }
        }},
        { RaceType.WhiteElf, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 8 }, { RaceType.WhiteElf, 5 }, { RaceType.Dwarf, 6 },
            { RaceType.Human, 6 }, { RaceType.Orc, 3 }, { RaceType.Goblin, 7 },
            { RaceType.DarkElf, 2 }, { RaceType.Ogre, 1 }
        }},
        { RaceType.DarkElf, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 10 }, { RaceType.Dwarf, 8 }, { RaceType.WhiteElf, 3 },
            { RaceType.Human, 7 }, { RaceType.Orc, 4 }, { RaceType.Goblin, 8 },
            { RaceType.DarkElf, 5 }, { RaceType.Ogre, 1 }
        }},
        { RaceType.Ogre, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 20 }, { RaceType.Dwarf, 15 }, { RaceType.WhiteElf, 14 },
            { RaceType.Human, 12 }, { RaceType.Orc, 9 }, { RaceType.Goblin, 15 },
            { RaceType.DarkElf, 11 }, { RaceType.Ogre, 6 }
        }},
        { RaceType.Human, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 10 }, { RaceType.Dwarf, 7 }, { RaceType.WhiteElf, 5 },
            { RaceType.Human, 6 }, { RaceType.Orc, 3 }, { RaceType.Goblin, 9 },
            { RaceType.DarkElf, 4 }, { RaceType.Ogre, 1 }
        }},
        { RaceType.Dwarf, new Dictionary<RaceType, float> {
            { RaceType.ElfNature, 8 }, { RaceType.Dwarf, 6 }, { RaceType.WhiteElf, 3 },
            { RaceType.Human, 4 }, { RaceType.Orc, 2 }, { RaceType.Goblin, 7 },
            { RaceType.DarkElf, 1 }, { RaceType.Ogre, 0.5f }
        }},
    };
}