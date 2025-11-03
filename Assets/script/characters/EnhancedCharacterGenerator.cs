using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generatore avanzato di personaggi con supporto completo per le specifiche fisiche delle razze.
/// Integra RacePhysicalSpecs e RagdollConfiguration per creare personaggi completi.
/// </summary>
public static class EnhancedCharacterGenerator
{
    /// <summary>
    /// Genera un personaggio completo con tutte le caratteristiche fisiche per una razza.
    /// </summary>
    public static CharacterAttributes GenerateCharacterWithPhysics(RaceType race, Sex? preferredSex = null)
    {
        var specs = RacePhysicalSpecs.GetSpecsForRace(race);
        
        // Determina il sesso
        Sex sex = preferredSex ?? RacePhysicalSpecs.GetRandomGender(race);
        
        // Se il sesso preferito non è disponibile, usa uno valido
        if (!RacePhysicalSpecs.IsGenderAvailable(race, sex))
        {
            sex = RacePhysicalSpecs.GetRandomGender(race);
        }

        var character = new CharacterAttributes
        {
            Name = GenerateRandomName(race, sex),
            Race = race,
            Sex = sex,
            Kingdom = GetDefaultKingdom(race),
            Age = GenerateAge(race),
            Physical = GeneratePhysicalAttributes(race, sex, specs),
            Description = GenerateDescription(race, sex),
            IsSlave = false,
            Libido = Random.Range(20, 80)
        };

        return character;
    }

    /// <summary>
    /// Genera attributi fisici dettagliati basati sulla razza e sesso.
    /// </summary>
    private static PhysicalAttributes GeneratePhysicalAttributes(RaceType race, Sex sex, RacePhysicalSpecs.PhysicalSpec specs)
    {
        var physical = new PhysicalAttributes
        {
            Height = RacePhysicalSpecs.GenerateRandomHeight(race),
            SkinColor = RacePhysicalSpecs.GenerateRandomSkinColor(race),
            HairColor = RacePhysicalSpecs.GenerateRandomHairColor(race),
            EyeColor = RacePhysicalSpecs.GenerateRandomEyeColor(race),
            HasPointedEars = specs.HasPointedEars,
            HasHorns = specs.HasHorns,
            HasSaberTeeth = specs.HasSaberTeeth,
            HasLongNose = specs.HasLongNose,
            BodyType = specs.BodyType,
            HairLength = specs.HairLength
        };

        // Calcola peso basato su altezza e corporatura
        physical.Weight = CalculateWeight(physical.Height, specs.BodyType, race);

        // Genera muscoli e grasso corporeo basato sul tipo di corporatura
        GenerateBodyComposition(physical, specs.BodyType);

        // Caratteristiche sessuali
        if (sex == Sex.Male)
        {
            physical.PenisSize = Random.Range(8f, 20f); // cm
            physical.BreastSize = 0f;
        }
        else if (sex == Sex.Female)
        {
            physical.PenisSize = 0f;
            physical.BreastSize = Random.Range(70f, 100f); // Cup size simulato
        }
        else // None (Ogre)
        {
            physical.PenisSize = 0f;
            physical.BreastSize = 0f;
        }

        // Hair style basato su lunghezza capelli e sesso
        physical.HairStyle = GenerateHairStyle(race, sex, specs.HairLength);

        return physical;
    }

    /// <summary>
    /// Calcola il peso in base all'altezza e corporatura.
    /// </summary>
    private static float CalculateWeight(float heightCm, RacePhysicalSpecs.BodyType bodyType, RaceType race)
    {
        float heightM = heightCm / 100f;
        float baseWeight = 0f;

        // BMI base per tipo di corporatura
        switch (bodyType)
        {
            case RacePhysicalSpecs.BodyType.Thin:
                baseWeight = 18f * (heightM * heightM); // BMI 18
                break;
            case RacePhysicalSpecs.BodyType.ThinToned:
                baseWeight = 20f * (heightM * heightM); // BMI 20
                break;
            case RacePhysicalSpecs.BodyType.Athletic:
                baseWeight = 22f * (heightM * heightM); // BMI 22
                break;
            case RacePhysicalSpecs.BodyType.Muscular:
                baseWeight = 25f * (heightM * heightM); // BMI 25
                break;
            case RacePhysicalSpecs.BodyType.VeryMuscular:
                baseWeight = 28f * (heightM * heightM); // BMI 28
                break;
            case RacePhysicalSpecs.BodyType.Stocky:
                baseWeight = 26f * (heightM * heightM); // BMI 26
                break;
            case RacePhysicalSpecs.BodyType.Fat:
                baseWeight = 30f * (heightM * heightM); // BMI 30
                break;
            case RacePhysicalSpecs.BodyType.FatMuscular:
                baseWeight = 32f * (heightM * heightM); // BMI 32
                break;
            default:
                baseWeight = 22f * (heightM * heightM);
                break;
        }

        // Modificatore per razza (densità ossea, struttura)
        switch (race)
        {
            case RaceType.Orc:
                baseWeight *= 1.3f; // Ossa più dense, più muscolosi
                break;
            case RaceType.Ogre:
                baseWeight *= 1.5f; // Massicci
                break;
            case RaceType.Dwarf:
                baseWeight *= 1.2f; // Compatti e robusti
                break;
            case RaceType.ElfNature:
            case RaceType.Elf:
            case RaceType.WhiteElf:
            case RaceType.DarkElf:
                baseWeight *= 0.9f; // Struttura più leggera
                break;
        }

        return baseWeight;
    }

    /// <summary>
    /// Genera composizione corporea (muscoli e grasso).
    /// </summary>
    private static void GenerateBodyComposition(PhysicalAttributes physical, RacePhysicalSpecs.BodyType bodyType)
    {
        switch (bodyType)
        {
            case RacePhysicalSpecs.BodyType.Thin:
                physical.Muscles = Random.Range(10f, 30f);
                physical.BodyFat = Random.Range(5f, 15f);
                break;
            case RacePhysicalSpecs.BodyType.ThinToned:
                physical.Muscles = Random.Range(40f, 60f);
                physical.BodyFat = Random.Range(10f, 18f);
                break;
            case RacePhysicalSpecs.BodyType.Athletic:
                physical.Muscles = Random.Range(50f, 70f);
                physical.BodyFat = Random.Range(12f, 20f);
                break;
            case RacePhysicalSpecs.BodyType.Muscular:
                physical.Muscles = Random.Range(70f, 85f);
                physical.BodyFat = Random.Range(15f, 22f);
                break;
            case RacePhysicalSpecs.BodyType.VeryMuscular:
                physical.Muscles = Random.Range(85f, 100f);
                physical.BodyFat = Random.Range(12f, 18f);
                break;
            case RacePhysicalSpecs.BodyType.Stocky:
                physical.Muscles = Random.Range(60f, 80f);
                physical.BodyFat = Random.Range(20f, 30f);
                break;
            case RacePhysicalSpecs.BodyType.Fat:
                physical.Muscles = Random.Range(30f, 50f);
                physical.BodyFat = Random.Range(30f, 45f);
                break;
            case RacePhysicalSpecs.BodyType.FatMuscular:
                physical.Muscles = Random.Range(70f, 90f);
                physical.BodyFat = Random.Range(25f, 35f);
                break;
        }
    }

    /// <summary>
    /// Genera uno stile di capelli appropriato.
    /// </summary>
    private static string GenerateHairStyle(RaceType race, Sex sex, RacePhysicalSpecs.HairLength hairLength)
    {
        if (hairLength == RacePhysicalSpecs.HairLength.None)
            return "Bald";

        List<string> styles = new List<string>();

        switch (hairLength)
        {
            case RacePhysicalSpecs.HairLength.Short:
                styles.AddRange(new[] { "Buzz Cut", "Short Crop", "Military Cut", "Pixie" });
                break;
            case RacePhysicalSpecs.HairLength.Long:
                styles.AddRange(new[] { "Long Straight", "Long Wavy", "Braided", "Flowing" });
                break;
            case RacePhysicalSpecs.HairLength.Variable:
                if (sex == Sex.Male)
                    styles.AddRange(new[] { "Short", "Medium", "Ponytail" });
                else
                    styles.AddRange(new[] { "Long", "Medium", "Braided", "Updo" });
                break;
        }

        // Stili specifici per razza
        switch (race)
        {
            case RaceType.Orc:
                if (sex == Sex.Male)
                    return "Short Spiked";
                else
                    return "Long Warrior Braids";
            case RaceType.Dwarf:
                if (sex == Sex.Male)
                    return "Beard and Hair";
                break;
        }

        return styles.Count > 0 ? styles[Random.Range(0, styles.Count)] : "Default";
    }

    /// <summary>
    /// Genera un nome casuale per la razza e sesso.
    /// </summary>
    private static string GenerateRandomName(RaceType race, Sex sex)
    {
        // Liste di nomi fantasy per ogni razza
        Dictionary<RaceType, List<string>> maleNames = new Dictionary<RaceType, List<string>>
        {
            { RaceType.Human, new List<string> { "Marcus", "John", "William", "Alexander", "David" } },
            { RaceType.ElfNature, new List<string> { "Thalion", "Elrond", "Legolas", "Galadriel", "Celeborn" } },
            { RaceType.WhiteElf, new List<string> { "Aerendil", "Finrod", "Glorfindel", "Thranduil" } },
            { RaceType.DarkElf, new List<string> { "Drizzt", "Malekith", "Valen", "Zaknafein" } },
            { RaceType.Orc, new List<string> { "Grom", "Thrall", "Durotan", "Grommash", "Blackhand" } },
            { RaceType.Goblin, new List<string> { "Gizmo", "Snitch", "Grub", "Snarl", "Runt" } },
            { RaceType.Dwarf, new List<string> { "Thorin", "Gimli", "Balin", "Dwalin", "Bombur" } },
            { RaceType.Ogre, new List<string> { "Shrek", "Grunk", "Mog", "Thok", "Grog" } }
        };

        Dictionary<RaceType, List<string>> femaleNames = new Dictionary<RaceType, List<string>>
        {
            { RaceType.Human, new List<string> { "Emma", "Sarah", "Elizabeth", "Margaret", "Catherine" } },
            { RaceType.ElfNature, new List<string> { "Arwen", "Galadriel", "Luthien", "Idril", "Nessa" } },
            { RaceType.WhiteElf, new List<string> { "Celestia", "Lunara", "Auriel", "Sylvara" } },
            { RaceType.DarkElf, new List<string> { "Viconia", "Briza", "Yvonnel", "Qilue" } },
            { RaceType.Orc, new List<string> { "Garona", "Draka", "Aggra", "Geyah" } },
            { RaceType.Dwarf, new List<string> { "Dis", "Katrin", "Hilda", "Greta" } }
        };

        if (sex == Sex.Male && maleNames.ContainsKey(race))
        {
            var names = maleNames[race];
            return names[Random.Range(0, names.Count)];
        }
        else if (sex == Sex.Female && femaleNames.ContainsKey(race))
        {
            var names = femaleNames[race];
            return names[Random.Range(0, names.Count)];
        }

        return $"{race}_{Random.Range(1000, 9999)}";
    }

    /// <summary>
    /// Genera un'età appropriata per la razza.
    /// </summary>
    private static int GenerateAge(RaceType race)
    {
        switch (race)
        {
            case RaceType.ElfNature:
            case RaceType.Elf:
            case RaceType.WhiteElf:
            case RaceType.DarkElf:
                return Random.Range(100, 800); // Elfi vivono molto più a lungo
            case RaceType.Dwarf:
                return Random.Range(40, 250);
            case RaceType.Human:
                return Random.Range(18, 70);
            case RaceType.Orc:
                return Random.Range(15, 50); // Vita più breve
            case RaceType.Goblin:
                return Random.Range(10, 40);
            case RaceType.Ogre:
                return Random.Range(20, 100);
            default:
                return Random.Range(18, 50);
        }
    }

    /// <summary>
    /// Ottiene il regno di default per una razza.
    /// </summary>
    private static Kingdom GetDefaultKingdom(RaceType race)
    {
        switch (race)
        {
            case RaceType.ElfNature:
                return Kingdom.NatureElves;
            case RaceType.WhiteElf:
                return Kingdom.WhiteElves;
            case RaceType.DarkElf:
                return Kingdom.DarkElves;
            case RaceType.Orc:
                return Kingdom.Orcs;
            case RaceType.Goblin:
                return Kingdom.Goblins;
            case RaceType.Dwarf:
                return Kingdom.Dwarves;
            case RaceType.Ogre:
                return Kingdom.Ogres;
            case RaceType.Human:
            default:
                return Random.value > 0.5f ? Kingdom.HumanKingdom1 : Kingdom.HumanKingdom2;
        }
    }

    /// <summary>
    /// Genera una descrizione del personaggio.
    /// </summary>
    private static string GenerateDescription(RaceType race, Sex sex)
    {
        var specs = RacePhysicalSpecs.GetSpecsForRace(race);
        return $"Un{(sex == Sex.Female ? "a" : "")} {specs.RaceName.ToLower()} di bell'aspetto. {specs.Description}";
    }

    /// <summary>
    /// Crea un personaggio completo e lo istanzia in Unity con ragdoll configurato.
    /// </summary>
    public static GameObject InstantiateCharacterWithRagdoll(RaceType race, Sex? preferredSex = null, Vector3 position = default)
    {
        var character = GenerateCharacterWithPhysics(race, preferredSex);
        
        // Crea GameObject
        GameObject go = new GameObject($"{character.Name}_{character.Race}");
        go.transform.position = position;
        
        // Aggiungi componente per gli attributi
        var attrComp = go.AddComponent<CharacterAttributesComponent>();
        attrComp.Attributes = character;
        
        // Log: le ossa devono essere create in Blender/Maya e importate
        Debug.Log($"Personaggio {character.Name} creato. Per aggiungere il ragdoll, importa il modello 3D da Blender/Maya con la struttura scheletrica appropriata.");
        Debug.Log($"Ossa speciali richieste per {race}: {string.Join(", ", RagdollConfiguration.GetRagdollSetupForRace(race).CustomBones)}");
        
        return go;
    }
}

/// <summary>
/// Componente per mantenere gli attributi del personaggio nel GameObject.
/// </summary>
public class CharacterAttributesComponent : MonoBehaviour
{
    public CharacterAttributes Attributes;
}
