using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CharacterAttributes PlayerCharacter;
    public GameObject PlayerPrefab;
    private GameObject playerInstance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame(CharacterAttributes charAttr)
    {
        PlayerCharacter = charAttr;
        SpawnPlayer();
        TutorialManager.Instance.StartTutorial(PlayerCharacter);
    }

    void SpawnPlayer()
    {
        if (playerInstance != null) Destroy(playerInstance);
        Vector3 spawnPoint = KingdomSpawnPoints.GetSpawnPoint(PlayerCharacter.Kingdom);
        playerInstance = Instantiate(PlayerPrefab, spawnPoint, Quaternion.identity);
        playerInstance.GetComponent<PlayerController>().Init(PlayerCharacter);
    }
}