using System;
using Mirror;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public struct PlayerID : NetworkMessage
{
    public int ID;
}

public class CustomNetworkManager : NetworkManager
{
    private GameStateMachine _stateMachine;

    [Inject]
    public void Construct(GameStateMachine  stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Start()
    {
        base.Start();
        autoCreatePlayer = false;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PlayerID>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        var randomPlayerID = new PlayerID()
        {
            ID = Random.Range(1, 1000)
        };

        NetworkClient.Send(randomPlayerID);
        _stateMachine.EnterIn<GamePlayLoopState>();
    }

    void OnCreateCharacter(NetworkConnectionToClient conn, PlayerID id)
    {
        if (playerPrefab == null)
            throw new NullReferenceException("PLAYER prefab is empty");

        GameObject playerObject = Instantiate(playerPrefab); 

        NetworkServer.AddPlayerForConnection(conn, playerObject);
    }
}