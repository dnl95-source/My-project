using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

/// <summary>
/// Script di helper da mettere sul prefab chunk.
/// - contiene riferimenti a NPCSpawner, NavMeshSurface, e metadata (bioma, factionTag)
/// - utile per inizializzare al load e per salvare stato al unload.
/// </summary>
public class ChunkInitializer : MonoBehaviour
{
    public string chunkId;
    public BiomeType chunkBiome = BiomeType.Plains;
    public string dominantFactionId; // es. "ghestard"
    public NPCSpawner[] localSpawners;

    void Awake()
    {
        // inizializza spawner locali se presenti
        if (localSpawners == null || localSpawners.Length == 0)
            localSpawners = GetComponentsInChildren<NPCSpawner>(true);
    }

    public void OnChunkLoaded()
    {
        // chiamato dopo il caricamento del chunk
        foreach (var s in localSpawners)
        {
            if (s != null)
                s.gameObject.SetActive(true);
        }

        // se trovi NavMeshSurface puoi decidere di rebuildarlo qui
        var nav = GetComponentInChildren<NavMeshSurface>();
        if (nav != null)
            nav.BuildNavMesh();
    }

    public void OnChunkUnloaded()
    {
        // salvare qui lo stato e disabilitare oggetti pesanti
        foreach (var s in localSpawners)
        {
            if (s != null)
                s.gameObject.SetActive(false);
        }
    }
}