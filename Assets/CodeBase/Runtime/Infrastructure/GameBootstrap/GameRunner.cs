using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void InitBootstrapScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        
        SceneManager.LoadScene(0);
    }
}
