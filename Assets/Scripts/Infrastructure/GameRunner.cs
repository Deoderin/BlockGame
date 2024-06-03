using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
#if UNITY_EDITOR
    private const string InitialScene = "Bootstrap";

    private void Awake()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if(!bootstrapper)
        {
            SceneManager.LoadSceneAsync(InitialScene);
        }
    }
#endif
}