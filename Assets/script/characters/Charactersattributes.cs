using UnityEngine;

public enum RaceType { Human, Elf, ElfNature, WhiteElf, DarkElf, Orc, Goblin, Dwarf, Ogre }
public enum Sex { Male, Female, None }
public enum Kingdom { NatureElves, HumanKingdom1, HumanKingdom2, Dwarves, WhiteElves, DarkElves, Orcs, Goblins, Ogres, Godrik }

[System.Serializable]
public class PhysicalAttributes
{
    public float Height;
    public float Weight;
    public float PenisSize;
    public float BreastSize;
    public float Muscles;
    public float BodyFat;
}

[System.Serializable]
public class CharacterAttributes
{
    public string Name;
    public RaceType Race;
    public Sex Sex;
    public Kingdom Kingdom;
    public PhysicalAttributes Physical;
    public string Description;
    public int Age;
    public bool IsSlave;
    public int Libido;
    // Altri attributi...
}