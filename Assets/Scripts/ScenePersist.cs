using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    private int _startingSceneIndex;

    private void Awake()
    {
        var numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != _startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
