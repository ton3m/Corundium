using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private readonly EnemyStateMachine _enemyStateMachine;
    private Transform _targetEnemy;
    private bool _isPlayerVisible;
    private float _viewAngle;
    private float _vievRadius;


    public ChasePlayerState(EnemyStateMachine enemyStateMachine, EnemyInstance enemyInstance)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyInstance = enemyInstance;
    }

    public void EnterState()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        NavMeshAgent navMeshAgent = _enemyStateMachine.NavMeshAgent;
        Vector3 targetEnemyPosition = _enemyStateMachine.TargetEnemy.position;

        Vector3 playerPosition = targetEnemyPosition;

        navMeshAgent.destination = targetEnemyPosition;
    }

    public void ExitState()
    {
        
    }
}
