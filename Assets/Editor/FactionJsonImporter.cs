using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// Utility editor: importa un JSON (come Design/factions/ghestard.json) e crea un asset FactionData in Assets/Data/Factions/\
/// Menu: Tools/Import Faction From JSON
/// </summary>
public class FactionJsonImporter : EditorWindow
{
    string jsonPath = "Design/factions/ghestard.json";
    [MenuItem("Tools/Import Faction From JSON")]
    public static void ShowWindow() => GetWindow<FactionJsonImporter>("Faction JSON Importer");

    void OnGUI()
    {
        GUILayout.Label("Importa Faction JSON -> FactionData Asset", EditorStyles.boldLabel);
        jsonPath = EditorGUILayout.TextField("Percorso JSON", jsonPath);

        if (GUILayout.Button("Importa e crea asset"))
        {
            ImportJson(jsonPath);
        }
    }

    static void ImportJson(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File JSON non trovato: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        var wrapper = JsonUtility.FromJson<FactionJsonWrapper>(json);
        if (wrapper == null)
        {
            Debug.LogError("Impossibile deserializzare JSON");
            return;
        }

        string outDir = "Assets/Data/Factions";
        if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

        var asset = ScriptableObject.CreateInstance<FactionData>();
        asset.factionId = wrapper.factionId;
        asset.displayName = wrapper.displayName;
        asset.description = wrapper.description;
        asset.capitalCity = wrapper.capitalCity;
        asset.techLevel = (TechLevel)System.Enum.Parse(typeof(TechLevel), wrapper.techLevel);
        asset.alignment = (Alignment)System.Enum.Parse(typeof(Alignment), wrapper.alignment);

        Color col;
        if (ColorUtility.TryParseHtmlString(wrapper.primaryColor, out col)) asset.primaryColor = col;
        if (ColorUtility.TryParseHtmlString(wrapper.secondaryColor, out col)) asset.secondaryColor = col;

        asset.militaryStrength = wrapper.militaryStrength;
        asset.wealthIndex = wrapper.wealthIndex;
        asset.spawnPriority = wrapper.spawnPriority;

        asset.allies = new System.Collections.Generic.List<string>(wrapper.allies);
        asset.enemies = new System.Collections.Generic.List<string>(wrapper.enemies);
        asset.neutrals = new System.Collections.Generic.List<string>(wrapper.neutrals);

        var biomes = new System.Collections.Generic.List<BiomeType>();
        foreach (var b in wrapper.primaryBiomes)
        {
            if (System.Enum.TryParse(b, true, out BiomeType bt)) biomes.Add(bt);
        }
        asset.primaryBiomes = biomes.ToArray();

        asset.keyNPCs = new System.Collections.Generic.List<FactionNPC>();
        foreach (var n in wrapper.keyNPCs)
        {
            asset.keyNPCs.Add(new FactionNPC { role = n.role, npcName = n.npcName, shortDescription = n.shortDescription });
        }

        string assetPath = $"{outDir}/{asset.factionId}.asset";
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("FactionData asset creato in: " + assetPath);
    }

    [System.Serializable]
    class FactionJsonWrapper
    {
        public string factionId;
        public string displayName;
        public string description;
        public string capitalCity;
        public string[] primaryBiomes;
        public string techLevel;
        public string alignment;
        public string[] allies;
        public string[] enemies;
        public string[] neutrals;
        public string primaryColor;
        public string secondaryColor;
        public int militaryStrength;
        public int wealthIndex;
        public float spawnPriority;
        public SimpleNPC[] keyNPCs;
    }

    [System.Serializable]
    class SimpleNPC { public string role; public string npcName; public string shortDescription; }
}