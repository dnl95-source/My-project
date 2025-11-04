using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int npcCount = 10;

    void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            var npcGO = Instantiate(npcPrefab, pos, Quaternion.identity);
            var npc = npcGO.GetComponent<NPCController>();
            npc.Init(NPCGenerator.Instance.GenerateNPC(
                (RaceType)Random.Range(0, 7),
                (Sex)Random.Range(0, 2),
                (Kingdom)Random.Range(0, 9)
            ));
        }
    }
}