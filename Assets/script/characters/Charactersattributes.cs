using UnityEngine;

public enum RaceType { Human, Elf, ElfNature, WhiteElf, DarkElf, Orc, Goblin, Dwarf, Ogre }
public enum Sex { Male, Female, None }
public enum Kingdom { NatureElves, HumanKingdom1, HumanKingdom2, Dwarves, WhiteElves, DarkElves, Orcs, Goblins, Ogres, Godrik }

[System.Serializable]
public class PhysicalAttributes
{
    public float Height; // In centimetri
    public float Weight; // In kg
    public float PenisSize;
    public float BreastSize;
    public float Muscles; // 0-100
    public float BodyFat; // 0-100
    
    // Colori e caratteristiche visive
    public string SkinColor;
    public string HairColor;
    public string EyeColor;
    public string HairStyle;
    
    // Caratteristiche scheletriche speciali
    public bool HasPointedEars;
    public bool HasHorns;
    public bool HasSaberTeeth;
    public bool HasLongNose;
    
    // Tipo di corporatura
    public RacePhysicalSpecs.BodyType BodyType;
    public RacePhysicalSpecs.HairLength HairLength;
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