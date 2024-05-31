using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    public NavMeshAgent NavMeshAgent { get; }
    public Transform TargetEnemy { get; }
    
    private Dictionary<Type, IEnemyState> _enemyStates;
    private IEnemyState _currentEnemyState;
    
    public EnemyStateMachine(NavMeshAgent navMeshAgent, Transform targetEnemy, EnemyInstance enemyInstance)
    {
        NavMeshAgent = navMeshAgent;
        TargetEnemy = targetEnemy;
        
        _enemyStates = new Dictionary<Type, IEnemyState>()
        {
            [typeof(PatrolEnemyState)] = new PatrolEnemyState(this, enemyInstance),
            [typeof(ChasePlayerState)] = new ChasePlayerState(this),
            [typeof(AttackPlayer)] = new AttackPlayer(this)
        };
    }

    public void EnterIn<TState>() where TState : IEnemyState
    {
        if (_enemyStates.TryGetValue(typeof(TState), out IEnemyState state) && _currentEnemyState != state)
        {
            _currentEnemyState?.ExitState();
            Debug.Log("_currentEnemyState = " + _currentEnemyState);
            _currentEnemyState = state;
            Debug.Log(_currentEnemyState);
            _currentEnemyState.EnterState();
        }
    }
}