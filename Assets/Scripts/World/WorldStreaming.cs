using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

/// <summary>
/// Semplice manager di streaming a chunk + floating origin.
/// - chunkPrefab: prefab che rappresenta un chunk di mondo (terrain + spawner + navmesh surface optional)
/// - chunkSize: dimensione di un chunk in unità world (x/z)
/// - loadRadiusInChunks: raggio in chunk da caricare attorno al player (1 -> 3x3)
/// - floatingOriginThreshold: distanza dal centro oltre la quale viene ricentratto il mondo
/// </summary>
public class WorldStreaming : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject chunkPrefab; // prefab del chunk (in produzione usare Addressables)

    [Header("Chunk settings")]
    public int chunkSize = 512;
    public int loadRadiusInChunks = 1; // 1 => 3x3
    public float floatingOriginThreshold = 10000f;

    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;
        UpdateLoadedChunks(true);
    }

    void Update()
    {
        if (player == null) return;
        UpdateLoadedChunks(false);
        CheckFloatingOrigin();
    }

    private void UpdateLoadedChunks(bool force)
    {
        Vector3 p = player.position;
        Vector2Int center = new Vector2Int(Mathf.FloorToInt(p.x / chunkSize), Mathf.FloorToInt(p.z / chunkSize));

        int r = loadRadiusInChunks;
        HashSet<Vector2Int> shouldBeLoaded = new HashSet<Vector2Int>();
        for (int x = -r; x <= r; x++)
            for (int z = -r; z <= r; z++)
                shouldBeLoaded.Add(new Vector2Int(center.x + x, center.y + z));

        // unload chunks not necessary
        var toUnload = new List<Vector2Int>();
        foreach (var kvp in loadedChunks)
            if (!shouldBeLoaded.Contains(kvp.Key))
                toUnload.Add(kvp.Key);

        foreach (var key in toUnload)
        {
            var go = loadedChunks[key];
            loadedChunks.Remove(key);
            StartCoroutine(UnloadChunkRoutine(go));
        }

        // load missing chunks
        foreach (var coord in shouldBeLoaded)
            if (!loadedChunks.ContainsKey(coord))
                StartCoroutine(LoadChunkRoutine(coord));
    }

    private IEnumerator LoadChunkRoutine(Vector2Int coord)
    {
        // In prototype: istanzia prefab. In produzione: Addressables/SceneManager.LoadSceneAsync(additive)
        Vector3 worldPos = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        var go = Instantiate(chunkPrefab, worldPos, Quaternion.identity);
        go.name = $"Chunk_{coord.x}_{coord.y}";
        loadedChunks[coord] = go;

        // Se il chunk contiene uno spawner, inizializzalo
        var spawner = go.GetComponentInChildren<NPCSpawner>();
        if (spawner != null)
        {
            // opzionale: personalizza parametri spawner dal chunk (es. densità in base a bioma)
        }

        // Se il chunk ha NavMeshSurface (NavMeshComponents), prova a rebuild locale (costoso!)
        var nav = go.GetComponentInChildren<NavMeshSurface>();
        if (nav != null)
        {
            // build sync semplice (per prototipo)
            nav.BuildNavMesh();
        }

        yield return null;
    }

    private IEnumerator UnloadChunkRoutine(GameObject chunkRoot)
    {
        // TODO: salvare lo stato del chunk (inventari, oggetti raccolti, NPC importanti) prima di distruggere
        yield return null;
        Destroy(chunkRoot);
    }

    private void CheckFloatingOrigin()
    {
        Vector3 p = player.position;
        if (Mathf.Abs(p.x) > floatingOriginThreshold || Mathf.Abs(p.z) > floatingOriginThreshold)
        {
            Vector3 offset = new Vector3(p.x, p.y, p.z);
            // sposta tutti i chunk caricati
            foreach (var kvp in loadedChunks)
                if (kvp.Value != null)
                    kvp.Value.transform.position -= offset;

            // sposta altri root del mondo se necessario (EnvironmentRoot, spawners, ecc.)
            // Nota: se usi rigidbody o physics, ricordati di spostare anche i Rigidbody per evitare glitch

            // ricentra il player
            player.position -= offset;
            Debug.Log($"Floating origin recentered by {offset}");
        }
    }
}