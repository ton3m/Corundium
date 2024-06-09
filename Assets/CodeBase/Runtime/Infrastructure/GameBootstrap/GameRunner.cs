using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void InitBootstrapScene()
    {
        if (!IsEnabled())
            return;
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;

        SceneManager.LoadScene(0);
    }

    private static bool IsEnabled()
    {
        string path = "";

        var config = Resources.LoadAll<GameRunnerConfig>(path)[0];
        
        return config is null || config.Enabled;
    }
}