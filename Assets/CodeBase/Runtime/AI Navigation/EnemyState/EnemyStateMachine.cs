using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    public NavMeshAgent NavMeshAgent { get; }
    public Animator Animator { get; }
    
    private readonly Dictionary<Type, IEnemyState> _enemyStates;
    private IEnemyState _currentEnemyState;
    
    public EnemyStateMachine(NavMeshAgent navMeshAgent, Animator animator, EnemyInstance enemyInstance)
    {
        NavMeshAgent = navMeshAgent;
        Animator = animator;

        _enemyStates = new Dictionary<Type, IEnemyState>()
        {
            [typeof(PatrolEnemyState)] = new PatrolEnemyState(this, enemyInstance),
            [typeof(ChasePlayerState)] = new ChasePlayerState(this, enemyInstance),
            [typeof(AttackPlayer)] = new AttackPlayer(this, enemyInstance)
        };
    }

    public void EnterIn<TState>() where TState : IEnemyState
    {
        if (_enemyStates.TryGetValue(typeof(TState), out IEnemyState state) && _currentEnemyState != state)
        {
            _currentEnemyState?.ExitState();
            _currentEnemyState = state;
            Debug.Log(_currentEnemyState);
            _currentEnemyState.EnterState();
        }
    }
}