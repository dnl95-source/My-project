using UnityEngine;

/// <summary>
/// Singleton minimale per compatibilità con chiamate TutorialManager.Instance
/// - ora espone StartTutorial() e StartTutorial(object) per compatibilità con chiamate che passano argomenti.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = GameObject.Find("TutorialManager");
                if (go == null)
                {
                    go = new GameObject("TutorialManager");
                    _instance = go.AddComponent<TutorialManager>();
                    DontDestroyOnLoad(go);
                }
                else
                {
                    _instance = go.GetComponent<TutorialManager>() ?? go.AddComponent<TutorialManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) { _instance = this; DontDestroyOnLoad(gameObject); }
        else if (_instance != this) Destroy(gameObject);
    }

    // Existing no-arg StartTutorial
    public void StartTutorial()
    {
        Debug.Log("TutorialManager.StartTutorial() called (stub).");
    }

    // Overload that accepts one arbitrary parameter for compatibility
    public void StartTutorial(object data)
    {
        Debug.Log($"TutorialManager.StartTutorial(object) called with: {data?.ToString() ?? "null"} (stub).");
        // You can route/interpret 'data' as needed by your GameManager calls
        StartTutorial();
    }

    // Optional: varargs overload (if some code calls with multiple args)
    public void StartTutorial(params object[] args)
    {
        Debug.Log($"TutorialManager.StartTutorial(params) called with {args?.Length ?? 0} args (stub).");
        StartTutorial();
    }

    public bool IsTutorialActive()
    {
        return false;
    }
}