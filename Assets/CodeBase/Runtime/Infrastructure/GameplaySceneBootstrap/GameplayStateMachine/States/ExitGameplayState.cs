using Mirror;
using UnityEngine;
using Zenject;

public class ExitGameplayState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly CustomNetworkManager _networkManager;

    [Inject]
    public ExitGameplayState(GameStateMachine stateMachine, CustomNetworkManager networkManager)
    {
        _stateMachine = stateMachine;
        _networkManager = networkManager;
    }

    public void Enter()
    {
        // save game
        // clear subscriptions
        // release the addressables assets

        MainStopServer();
        _stateMachine.EnterIn<GameMenuState>();
    }

    public void Exit()
    {
    }

    private void MainStopServer()
    {
        if (NetworkClient.active)
            _networkManager.StopClient();
        if (NetworkServer.active) // и добавить что ты хост
            NetworkServer.Shutdown();
        
        Debug.Log("stop client/host, Client: " + NetworkClient.active + " Server: "  + NetworkServer.active);
    }
}
