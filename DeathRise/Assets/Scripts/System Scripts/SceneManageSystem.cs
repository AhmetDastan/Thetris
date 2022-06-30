using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageSystem : MonoBehaviour
{
    public static SceneManageSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
