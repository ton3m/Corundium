using UnityEngine;
using Zenject;

public class GameMenuState : IState
{
    private readonly ISceneLoader _sceneLoader;

    [Inject]
    public GameMenuState(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        Debug.Log("Menu  State");
        _sceneLoader.LoadScene(GameScenePaths.MenuLevelName);
    }

    public void Exit()
    {
    }
}
