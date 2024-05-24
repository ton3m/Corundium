using UnityEngine;
using Zenject;

public class GameBootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly InputHandler _inputHandler;

    [Inject]
    public GameBootstrapState(GameStateMachine gameStateMachine, InputHandler inputHandler)
    {
        _gameStateMachine = gameStateMachine;
       _inputHandler = inputHandler;
    }

    public void Enter()
    {
        Debug.Log("Boostrap State");
        // init services 
        _inputHandler.Enable();

        _gameStateMachine.EnterIn<GameMenuState>();
    }

    public void Exit()
    {
        
    }
}
