using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartFirstLevel() => SceneManager.LoadSceneAsync(1);

    public void LoadMainMenu() => SceneManager.LoadSceneAsync(0);
}
