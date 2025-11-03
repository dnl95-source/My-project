using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <summary>
/// Unity Editor window che cerca:
/// - duplicati di tipi (class/struct/enum) per nome
/// - file che non rispettano il naming class==file (warning)
/// - occorrenze di nomi specifici (utile per cercare CombatSystem, RaceType, ecc.)
/// </summary>
public class ScriptAnalyzerWindow : EditorWindow
{
    private Vector2 scroll;
    private string[] searchNames = new string[] { "CombatSystem", "RaceType", "CharacterGenerator", "NPC", "StatusEffectHandler" };
    private List<string> results = new List<string>();

    [MenuItem("Tools/Script Analyzer")]
    public static void OpenWindow()
    {
        GetWindow<ScriptAnalyzerWindow>("Script Analyzer");
    }

    void OnGUI()
    {
        GUILayout.Label("Script Analyzer", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Scansiona tutti i file .cs in Assets e cerca duplicati di tipi e mismatch nome file/classe. Aggiungi i nomi da cercare (uno per riga).", MessageType.Info);

        GUILayout.Label("Nomi da cercare (uno per riga):");
        string joined = string.Join("\n", searchNames);
        joined = EditorGUILayout.TextArea(joined, GUILayout.Height(80));
        searchNames = joined.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

        if (GUILayout.Button("Esegui scansione"))
        {
            results.Clear();
            ScanProject();
        }

        if (results.Count > 0)
        {
            GUILayout.Label("Risultati:", EditorStyles.boldLabel);
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var line in results)
            {
                EditorGUILayout.SelectableLabel(line, GUILayout.Height(16));
            }
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Apri cartella Assets"))
                EditorUtility.RevealInFinder(Application.dataPath);
        }
    }

    void ScanProject()
    {
        string[] files = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        var typePattern = new Regex(@"\b(public|internal|private|protected)?\s*(partial\s+)?(class|struct|enum)\s+([A-Za-z0-9_]+)", RegexOptions.Compiled);
        var fileTypeMap = new Dictionary<string, List<string>>(); // type -> files
        var fileClassMap = new Dictionary<string, string>(); // file -> first class

        foreach (var f in files)
        {
            string rel = "Assets" + f.Substring(Application.dataPath.Length).Replace("\\","/");
            string text = File.ReadAllText(f);
            foreach (Match m in typePattern.Matches(text))
            {
                string typeName = m.Groups[4].Value;
                if (!fileTypeMap.ContainsKey(typeName)) fileTypeMap[typeName] = new List<string>();
                fileTypeMap[typeName].Add(rel);

                if (!fileClassMap.ContainsKey(rel))
                    fileClassMap[rel] = typeName; // first found type in file
            }
        }

        // Duplicate types
        results.Add("=== Tipi duplicati trovati ===");
        bool foundDup = false;
        foreach (var kv in fileTypeMap.OrderBy(k => k.Key))
        {
            if (kv.Value.Count > 1)
            {
                foundDup = true;
                results.Add($"Tipo '{kv.Key}' definito in {kv.Value.Count} file:");
                foreach (var p in kv.Value) results.Add("  - " + p);
            }
        }
        if (!foundDup) results.Add("Nessun tipo duplicato trovato.");

        // File/Classe mismatch
        results.Add("");
        results.Add("=== File/Classe mismatch (file name vs first class found) ===");
        foreach (var kv in fileClassMap.OrderBy(k => k.Key))
        {
            string fileName = Path.GetFileNameWithoutExtension(kv.Key);
            if (!string.Equals(fileName, kv.Value, System.StringComparison.Ordinal))
            {
                results.Add($"WARN: {kv.Key} contiene classe '{kv.Value}' (file name = '{fileName}')");
            }
        }

        // Cerca nomi specifici e mostra occorrenze
        results.Add("");
        results.Add("=== Occorrenze per nomi di interesse ===");
        foreach (var name in searchNames)
        {
            var occ = files.SelectMany(f =>
            {
                var lines = File.ReadAllLines(f);
                var list = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(name))
                        list.Add($"{ "Assets" + f.Substring(Application.dataPath.Length).Replace("\\","/")} (riga {i+1}): {lines[i].Trim()}");
                }
                return list;
            }).Take(200).ToList();

            results.Add($"-- {name}: {occ.Count} occorrenze");
            foreach (var o in occ) results.Add("  " + o);
            results.Add("");
        }

        results.Add("Scansione completata.");
    }
}