using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Editor tool per generare documentazione delle specifiche ragdoll.
/// Crea file di testo con tutte le specifiche per la modellazione in Blender/Maya.
/// </summary>
public class RagdollSpecsGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Ragdoll Specifications")]
    public static void ShowWindow()
    {
        GetWindow<RagdollSpecsGenerator>("Ragdoll Specs Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generatore Specifiche Ragdoll", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.Label("Questo tool genera la documentazione completa per creare", EditorStyles.wordWrappedLabel);
        GUILayout.Label("i modelli 3D e ragdoll di tutte le razze in Blender/Maya.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);

        if (GUILayout.Button("Genera Documentazione Completa", GUILayout.Height(40)))
        {
            GenerateFullDocumentation();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Genera Report Ragdoll in Console", GUILayout.Height(30)))
        {
            string report = RagdollConfiguration.GenerateRagdollSpecificationReport();
            Debug.Log(report);
            EditorUtility.DisplayDialog("Report Generato", "Il report è stato stampato nella Console di Unity.", "OK");
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Genera Specifiche Singola Razza", GUILayout.Height(30)))
        {
            GenericMenu menu = new GenericMenu();
            foreach (RaceType race in System.Enum.GetValues(typeof(RaceType)))
            {
                menu.AddItem(new GUIContent(race.ToString()), false, () => GenerateRaceSpecification(race));
            }
            menu.ShowAsContext();
        }
    }

    private static void GenerateFullDocumentation()
    {
        string path = EditorUtility.SaveFilePanel(
            "Salva Documentazione Ragdoll",
            "",
            "RagdollSpecifications.txt",
            "txt"
        );

        if (string.IsNullOrEmpty(path))
            return;

        string content = GenerateFullDocumentationContent();
        File.WriteAllText(path, content);

        EditorUtility.DisplayDialog(
            "Documentazione Generata",
            $"Documentazione completa salvata in:\n{path}",
            "OK"
        );

        Debug.Log($"Documentazione ragdoll generata: {path}");
    }

    private static string GenerateFullDocumentationContent()
    {
        System.Text.StringBuilder doc = new System.Text.StringBuilder();
        
        doc.AppendLine("================================================================================");
        doc.AppendLine("          DOCUMENTAZIONE COMPLETA RAGDOLL - PROGETTO UNITY FANTASY");
        doc.AppendLine("================================================================================");
        doc.AppendLine();
        doc.AppendLine("Questo documento contiene tutte le specifiche per creare i modelli 3D");
        doc.AppendLine("e le strutture ragdoll per ogni razza del gioco in Blender o Maya.");
        doc.AppendLine();
        doc.AppendLine($"Generato il: {System.DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        doc.AppendLine();
        doc.AppendLine("================================================================================");
        doc.AppendLine();

        // Specifiche ragdoll
        doc.AppendLine(RagdollConfiguration.GenerateRagdollSpecificationReport());
        doc.AppendLine();

        // Guida dettagliata per ogni razza
        doc.AppendLine("\n\n================================================================================");
        doc.AppendLine("                     GUIDA DETTAGLIATA PER RAZZA");
        doc.AppendLine("================================================================================\n");

        foreach (RaceType race in System.Enum.GetValues(typeof(RaceType)))
        {
            doc.AppendLine(GenerateDetailedRaceGuide(race));
            doc.AppendLine();
        }

        // Istruzioni generali
        doc.AppendLine("\n\n================================================================================");
        doc.AppendLine("                     ISTRUZIONI PER BLENDER/MAYA");
        doc.AppendLine("================================================================================\n");
        doc.AppendLine(GetBlenderMayaInstructions());

        return doc.ToString();
    }

    private static string GenerateDetailedRaceGuide(RaceType race)
    {
        var physSpecs = RacePhysicalSpecs.GetSpecsForRace(race);
        var ragdollSetup = RagdollConfiguration.GetRagdollSetupForRace(race);
        
        System.Text.StringBuilder guide = new System.Text.StringBuilder();
        
        guide.AppendLine($"┌─────────────────────────────────────────────────────────────────┐");
        guide.AppendLine($"│  {race.ToString().ToUpper().PadRight(60)}│");
        guide.AppendLine($"└─────────────────────────────────────────────────────────────────┘");
        guide.AppendLine();
        
        guide.AppendLine($"Nome Razza: {physSpecs.RaceName}");
        guide.AppendLine($"Descrizione: {physSpecs.Description}");
        guide.AppendLine();
        
        guide.AppendLine("DIMENSIONI FISICHE:");
        guide.AppendLine($"  • Altezza: {physSpecs.MinHeight}cm - {physSpecs.MaxHeight}cm");
        guide.AppendLine($"  • Massa corporea: {ragdollSetup.TotalMass}kg");
        guide.AppendLine($"  • Corporatura: {physSpecs.BodyType}");
        guide.AppendLine();
        
        guide.AppendLine("GENERI DISPONIBILI:");
        foreach (var gender in physSpecs.AvailableGenders)
        {
            guide.AppendLine($"  • {gender}");
        }
        guide.AppendLine();
        
        guide.AppendLine("CARATTERISTICHE VISIVE:");
        guide.AppendLine($"  • Colori pelle: {string.Join(", ", physSpecs.SkinColors)}");
        guide.AppendLine($"  • Colori capelli: {(physSpecs.HairColors.Count > 0 ? string.Join(", ", physSpecs.HairColors) : "Nessuno (calvo)")}");
        guide.AppendLine($"  • Colori occhi: {string.Join(", ", physSpecs.EyeColors)}");
        guide.AppendLine($"  • Lunghezza capelli: {physSpecs.HairLength}");
        guide.AppendLine();
        
        guide.AppendLine("CARATTERISTICHE SCHELETRICHE:");
        guide.AppendLine($"  • Orecchie a punta: {(physSpecs.HasPointedEars ? "SÌ" : "NO")}");
        guide.AppendLine($"  • Corna: {(physSpecs.HasHorns ? "SÌ - Aggiungere ossa per corna al teschio" : "NO")}");
        guide.AppendLine($"  • Denti a sciabola: {(physSpecs.HasSaberTeeth ? "SÌ - Aggiungere ossa dentali lunghe" : "NO")}");
        guide.AppendLine($"  • Naso lungo: {(physSpecs.HasLongNose ? "SÌ - Modificare teschio per naso allungato" : "NO")}");
        guide.AppendLine($"  • Teschio personalizzato: {(ragdollSetup.HasCustomSkull ? "SÌ" : "NO")}");
        guide.AppendLine();
        
        if (ragdollSetup.CustomBones.Count > 0)
        {
            guide.AppendLine("OSSA SPECIALI DA CREARE:");
            foreach (var bone in ragdollSetup.CustomBones)
            {
                guide.AppendLine($"  • {bone}");
            }
            guide.AppendLine();
        }
        
        guide.AppendLine("NOTE SPECIALI:");
        guide.AppendLine($"  {physSpecs.SpecialFeatures}");
        guide.AppendLine();
        
        return guide.ToString();
    }

    private static void GenerateRaceSpecification(RaceType race)
    {
        string content = GenerateDetailedRaceGuide(race);
        Debug.Log($"\n{content}");
        
        string path = EditorUtility.SaveFilePanel(
            $"Salva Specifiche {race}",
            "",
            $"Specs_{race}.txt",
            "txt"
        );

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, content);
            EditorUtility.DisplayDialog("Specifiche Salvate", $"Specifiche per {race} salvate in:\n{path}", "OK");
        }
    }

    private static string GetBlenderMayaInstructions()
    {
        return @"
ISTRUZIONI PER CREARE I MODELLI 3D E RAGDOLL:

1. MODELLAZIONE IN BLENDER/MAYA:
   ─────────────────────────────────
   a) Crea il mesh base del corpo seguendo le proporzioni indicate
   b) Usa le altezze min/max come riferimento
   c) Modella le caratteristiche speciali (orecchie a punta, corna, ecc.)
   d) Mantieni una topologia pulita per il rigging

2. CREAZIONE DELLO SCHELETRO:
   ──────────────────────────────
   a) Crea la gerarchia di ossa seguendo la struttura standard umanoide
   b) Aggiungi le ossa speciali indicate per ogni razza:
      - Corna per Orchi
      - Denti a sciabola per Orchi e Ogre
      - Orecchie per Elfi
      - Naso lungo per Goblin
   c) Assicurati che i nomi delle ossa corrispondano a quelli nella documentazione
   d) Configura i vincoli articolari (limiti rotazione)

3. RIGGING (SKINNING):
   ───────────────────────
   a) Fai il binding del mesh allo scheletro
   b) Pesa i vertici accuratamente
   c) Testa le deformazioni con pose diverse
   d) Sistema eventuali artefatti

4. ESPORTAZIONE PER UNITY:
   ─────────────────────────
   a) Esporta in formato FBX
   b) Includi l'armatura (skeleton)
   c) Includi le animazioni se presenti
   d) Scala: 1 unità Blender/Maya = 1 metro Unity
   e) Forward: -Z, Up: Y (per Unity)

5. IMPORTAZIONE IN UNITY:
   ──────────────────────────
   a) Importa l'FBX nel progetto Unity
   b) Nelle impostazioni di importazione, seleziona ""Humanoid"" come Rig type
   c) Configura l'Avatar se necessario
   d) Usa lo script RagdollConfiguration per applicare il ragdoll
   e) Testa il ragdoll in Play Mode

6. CONFIGURAZIONE RAGDOLL IN UNITY:
   ─────────────────────────────────
   a) Seleziona il GameObject del personaggio
   b) Usa: GameObject → 3D Object → Ragdoll...
   c) Oppure usa lo script: RagdollConfiguration.ApplyRagdollToGameObject()
   d) Configura masse usando i valori nella documentazione
   e) Regola i collider per aderire al mesh
   f) Imposta i limiti articolari (Character Joints)

7. TESTING:
   ──────────
   a) Testa il ragdoll lasciando cadere il personaggio
   b) Verifica che tutte le articolazioni funzionino correttamente
   c) Controlla che non ci siano compenetrazioni
   d) Regola masse e limiti se necessario

NOTE IMPORTANTI:
  • Mantieni la gerarchia delle ossa coerente tra tutti i modelli
  • Usa convenzioni di naming consistenti
  • Le ossa opzionali (es. dita) possono essere omesse se non necessarie
  • Per razze molto diverse (es. Ogre), potresti aver bisogno di scaling manuale
  • Testa sempre in Unity prima di considerare completo il modello

RISORSE UTILI:
  • Unity Ragdoll Documentation: docs.unity3d.com/Manual/wizard-RagdollWizard.html
  • Blender to Unity FBX export guide
  • Maya to Unity export guide
";
    }
}
