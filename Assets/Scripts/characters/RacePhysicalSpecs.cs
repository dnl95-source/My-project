using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Definisce le specifiche fisiche dettagliate per ogni razza del gioco.
/// Include altezza, corporatura, colori, caratteristiche scheletriche speciali.
/// </summary>
public static class RacePhysicalSpecs
{
    [System.Serializable]
    public class PhysicalSpec
    {
        public string RaceName;
        public float MinHeight; // cm
        public float MaxHeight; // cm
        public List<Sex> AvailableGenders;
        public BodyType BodyType;
        public List<string> SkinColors;
        public List<string> HairColors;
        public List<string> EyeColors;
        public HairLength HairLength;
        public bool HasPointedEars;
        public bool HasHorns;
        public bool HasSaberTeeth;
        public bool HasLongNose;
        public string SpecialFeatures;
        public string Description;
    }

    public enum BodyType
    {
        Thin,           // Magro
        ThinToned,      // Magro e tonificato
        Athletic,       // Atletico
        Muscular,       // Muscoloso
        VeryMuscular,   // Molto muscoloso
        Stocky,         // Robusto
        Fat,            // Grasso
        FatMuscular     // Grasso e muscoloso
    }

    public enum HairLength
    {
        None,           // Senza capelli
        Short,          // Corti
        Long,           // Lunghi
        Variable        // Vario
    }

    private static Dictionary<RaceType, PhysicalSpec> _specs;

    static RacePhysicalSpecs()
    {
        InitializeSpecs();
    }

    private static void InitializeSpecs()
    {
        _specs = new Dictionary<RaceType, PhysicalSpec>
        {
            // ELFI DELLA NATURA (ElfNature)
            {
                RaceType.ElfNature,
                new PhysicalSpec
                {
                    RaceName = "Elfo della Natura",
                    MinHeight = 145f,
                    MaxHeight = 160f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.Thin,
                    SkinColors = new List<string> { "White", "PaleWhite" },
                    HairColors = new List<string> { "Black", "Brown", "Blonde", "Red", "White" },
                    EyeColors = new List<string> { "Blue", "Green", "Brown", "Grey" },
                    HairLength = HairLength.Long,
                    HasPointedEars = true,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umanoide, orecchie a punta. Maschi con aspetto femboy (molto femminili), femmine carine.",
                    Description = "Elfi della natura: altezza 145-160cm, magri, carnagione bianca, capelli lunghi di vari colori, occhi vari colori, orecchie a punta."
                }
            },

            // GOBLIN
            {
                RaceType.Goblin,
                new PhysicalSpec
                {
                    RaceName = "Goblin",
                    MinHeight = 140f,
                    MaxHeight = 150f,
                    AvailableGenders = new List<Sex> { Sex.Male },
                    BodyType = BodyType.Thin,
                    SkinColors = new List<string> { "Green" },
                    HairColors = new List<string> { },
                    EyeColors = new List<string> { "Red" },
                    HairLength = HairLength.None,
                    HasPointedEars = true,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = true,
                    SpecialFeatures = "Scheletro umanoide con teschio modificato: orecchie lunghe e naso lungo. Senza capelli.",
                    Description = "Goblin: solo maschi, altezza 140-150cm, magri, pelle verde, occhi rossi, senza capelli, naso e orecchie lunghe."
                }
            },

            // ORCHI (Orc)
            {
                RaceType.Orc,
                new PhysicalSpec
                {
                    RaceName = "Orco",
                    MinHeight = 180f,
                    MaxHeight = 200f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.VeryMuscular,
                    SkinColors = new List<string> { "DarkGreen", "DarkGreenWithRedStripes" },
                    HairColors = new List<string> { "Black" },
                    EyeColors = new List<string> { "Black" },
                    HairLength = HairLength.Variable, // Corti per maschi, lunghi per femmine
                    HasPointedEars = false,
                    HasHorns = true,
                    HasSaberTeeth = true,
                    HasLongNose = false,
                    SpecialFeatures = "Teschio con ossa aggiuntive per le corna e denti a sciabola lunghi. Fascia rossa su petto e schiena. Capelli corti (M), lunghi (F).",
                    Description = "Orchi: maschi e femmine, altezza 180-200cm, molto muscolosi, pelle verde scuro con fascia rossa, corna, denti a sciabola, occhi e capelli neri."
                }
            },

            // ELFI BIANCHI (WhiteElf)
            {
                RaceType.WhiteElf,
                new PhysicalSpec
                {
                    RaceName = "Elfo Bianco",
                    MinHeight = 175f,
                    MaxHeight = 190f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.ThinToned,
                    SkinColors = new List<string> { "White", "PaleWhite" },
                    HairColors = new List<string> { "Blonde", "LightBlonde" },
                    EyeColors = new List<string> { "Blue", "LightBlue" },
                    HairLength = HairLength.Long,
                    HasPointedEars = true,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umano con orecchie a punta elfiche. Capelli lunghi e biondi per entrambi i sessi.",
                    Description = "Elfi Bianchi: maschi e femmine, altezza 175-190cm, magri e tonificati, carnagione bianca, capelli lunghi biondi, occhi azzurri, orecchie a punta."
                }
            },

            // ELFI OSCURI (DarkElf)
            {
                RaceType.DarkElf,
                new PhysicalSpec
                {
                    RaceName = "Elfo Oscuro",
                    MinHeight = 180f,
                    MaxHeight = 210f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.Thin,
                    SkinColors = new List<string> { "DarkBlue" },
                    HairColors = new List<string> { "Red", "DarkRed" },
                    EyeColors = new List<string> { "Yellow", "GoldenYellow" },
                    HairLength = HairLength.Long,
                    HasPointedEars = true,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umanoide con orecchie a punta elfiche. Bocca rossa, capelli rossi e lunghi.",
                    Description = "Elfi Oscuri: maschi e femmine, altezza 180-210cm, magri, carnagione blu scura, capelli rossi lunghi, occhi gialli, orecchie a punta, bocca rossa."
                }
            },

            // NANI (Dwarf)
            {
                RaceType.Dwarf,
                new PhysicalSpec
                {
                    RaceName = "Nano",
                    MinHeight = 100f,
                    MaxHeight = 125f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.Stocky,
                    SkinColors = new List<string> { "PaleSkin", "Tan" },
                    HairColors = new List<string> { "Brown", "Black" },
                    EyeColors = new List<string> { "Black", "DarkBrown" },
                    HairLength = HairLength.Variable,
                    HasPointedEars = false,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umanoide robusto, corporatura compatta.",
                    Description = "Nani: maschi e femmine, altezza 100-125cm, robusti, capelli castani o neri, occhi neri."
                }
            },

            // UMANI (Human)
            {
                RaceType.Human,
                new PhysicalSpec
                {
                    RaceName = "Umano",
                    MinHeight = 160f,
                    MaxHeight = 195f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.Variable, // Può essere qualsiasi
                    SkinColors = new List<string> { "PaleSkin", "Tan", "Brown", "DarkBrown" },
                    HairColors = new List<string> { "Blonde", "Brown", "Black", "Red", "Auburn" },
                    EyeColors = new List<string> { "Black", "Blue", "Green", "Brown", "Grey" },
                    HairLength = HairLength.Variable,
                    HasPointedEars = false,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umanoide standard. Corporature varie: magri, muscolosi, grassi.",
                    Description = "Umani: maschi e femmine, altezza 160-195cm, corporature varie, capelli e occhi di vari colori."
                }
            },

            // OGRE
            {
                RaceType.Ogre,
                new PhysicalSpec
                {
                    RaceName = "Ogre",
                    MinHeight = 210f,
                    MaxHeight = 300f,
                    AvailableGenders = new List<Sex> { Sex.None },
                    BodyType = BodyType.FatMuscular,
                    SkinColors = new List<string> { "AquaGreen", "PaleGreen" },
                    HairColors = new List<string> { },
                    EyeColors = new List<string> { "Red" },
                    HairLength = HairLength.None,
                    HasPointedEars = false,
                    HasHorns = false,
                    HasSaberTeeth = true,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro massiccio con denti a sciabola come gli orchi. Asessuato.",
                    Description = "Ogre: asessuati, altezza 210-300cm, grassi e muscolosi, pelle verde acqueo, denti a sciabola, occhi rossi."
                }
            },

            // ELF (generico)
            {
                RaceType.Elf,
                new PhysicalSpec
                {
                    RaceName = "Elfo",
                    MinHeight = 160f,
                    MaxHeight = 180f,
                    AvailableGenders = new List<Sex> { Sex.Male, Sex.Female },
                    BodyType = BodyType.Thin,
                    SkinColors = new List<string> { "White", "PaleSkin" },
                    HairColors = new List<string> { "Blonde", "Brown", "Black" },
                    EyeColors = new List<string> { "Blue", "Green", "Grey" },
                    HairLength = HairLength.Long,
                    HasPointedEars = true,
                    HasHorns = false,
                    HasSaberTeeth = false,
                    HasLongNose = false,
                    SpecialFeatures = "Scheletro umanoide con orecchie a punta elfiche.",
                    Description = "Elfo generico: maschi e femmine, altezza 160-180cm, magri, orecchie a punta."
                }
            }
        };
    }

    /// <summary>
    /// Ottiene le specifiche fisiche per una razza.
    /// </summary>
    public static PhysicalSpec GetSpecsForRace(RaceType race)
    {
        if (_specs.ContainsKey(race))
            return _specs[race];
        
        Debug.LogWarning($"Specifiche fisiche non trovate per la razza {race}. Usando specifiche di default.");
        return _specs[RaceType.Human]; // Default
    }

    /// <summary>
    /// Genera un'altezza casuale per una razza specifica.
    /// </summary>
    public static float GenerateRandomHeight(RaceType race)
    {
        var spec = GetSpecsForRace(race);
        return Random.Range(spec.MinHeight, spec.MaxHeight);
    }

    /// <summary>
    /// Genera un colore di pelle casuale per una razza.
    /// </summary>
    public static string GenerateRandomSkinColor(RaceType race)
    {
        var spec = GetSpecsForRace(race);
        if (spec.SkinColors.Count == 0) return "Default";
        return spec.SkinColors[Random.Range(0, spec.SkinColors.Count)];
    }

    /// <summary>
    /// Genera un colore di capelli casuale per una razza.
    /// </summary>
    public static string GenerateRandomHairColor(RaceType race)
    {
        var spec = GetSpecsForRace(race);
        if (spec.HairColors.Count == 0) return "None";
        return spec.HairColors[Random.Range(0, spec.HairColors.Count)];
    }

    /// <summary>
    /// Genera un colore degli occhi casuale per una razza.
    /// </summary>
    public static string GenerateRandomEyeColor(RaceType race)
    {
        var spec = GetSpecsForRace(race);
        if (spec.EyeColors.Count == 0) return "Default";
        return spec.EyeColors[Random.Range(0, spec.EyeColors.Count)];
    }

    /// <summary>
    /// Verifica se un genere è disponibile per una razza.
    /// </summary>
    public static bool IsGenderAvailable(RaceType race, Sex gender)
    {
        var spec = GetSpecsForRace(race);
        return spec.AvailableGenders.Contains(gender);
    }

    /// <summary>
    /// Ottiene un genere casuale valido per una razza.
    /// </summary>
    public static Sex GetRandomGender(RaceType race)
    {
        var spec = GetSpecsForRace(race);
        if (spec.AvailableGenders.Count == 0) return Sex.None;
        return spec.AvailableGenders[Random.Range(0, spec.AvailableGenders.Count)];
    }
}
