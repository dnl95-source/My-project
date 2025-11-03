public static class RacialHealthTable
{
    public static int GetHealthByRace(RaceType race)
    {
        switch (race)
        {
            case RaceType.Human: return 120;
            case RaceType.Dwarf: return 90;
            case RaceType.ElfNature: return 70;
            case RaceType.Goblin: return 60;
            case RaceType.Orc: return 140;
            case RaceType.WhiteElf: return 115;
            case RaceType.DarkElf: return 150;
            case RaceType.Ogre: return 200;
            default: return 100;
        }
    }
}