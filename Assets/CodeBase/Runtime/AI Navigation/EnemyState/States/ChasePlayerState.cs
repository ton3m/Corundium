using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;
    private Vector3 _playerPosition;
    private NavMeshAgent _navMeshAgent;
    public ChasePlayerState(EnemyStateMachine enemyStateMachine)
    {
        _enemyStateMachine = enemyStateMachine;
    }

    public void EnterState()
    {
        ChasePlayer();
    }
    
    private void ChasePlayer()
    {
        _navMeshAgent = _enemyStateMachine.NavMeshAgent;
        _playerPosition = _enemyStateMachine.TargetEnemy.position;
        
        _navMeshAgent.SetDestination(_playerPosition);
    }

    public void ExitState()
    {
        _navMeshAgent.ResetPath();
    }
}
