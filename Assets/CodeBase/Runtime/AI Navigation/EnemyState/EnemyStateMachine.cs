using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    public NavMeshAgent NavMeshAgent { get; }
    
    private Dictionary<Type, IEnemyState> _enemyStates;
    private IEnemyState _currentEnemyState;
    
    public EnemyStateMachine(NavMeshAgent navMeshAgent, EnemyInstance enemyInstance)
    {
        NavMeshAgent = navMeshAgent;
        
        _enemyStates = new Dictionary<Type, IEnemyState>()
        {
            [typeof(PatrolEnemyState)] = new PatrolEnemyState(this, enemyInstance),
            [typeof(ChasePlayerState)] = new ChasePlayerState(enemyInstance),
            [typeof(AttackPlayer)] = new AttackPlayer(this)
        };
    }

    public void EnterIn<TState>() where TState : IEnemyState
    {
        if (_enemyStates.TryGetValue(typeof(TState), out IEnemyState state) && _currentEnemyState != state)
        {
            _currentEnemyState?.ExitState();
            _currentEnemyState = state;
            _currentEnemyState.EnterState();
        }
    }
}