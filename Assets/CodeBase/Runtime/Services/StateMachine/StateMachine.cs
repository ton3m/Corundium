using System.Collections.Generic;

public class StateMachine : IStateMachine
{
    private Dictionary<System.Type, IState> _states;
    private IState _currentState;

    public StateMachine()
    {
        _states = new();
    }

    public void EnterIn<TState>() where TState : IState
    {
        if(_states.TryGetValue(typeof(TState), out IState state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }

    public void RegisterState<TState>(TState state) where TState : IState
    {
        _states.Add(typeof(TState), state);
    }
}
