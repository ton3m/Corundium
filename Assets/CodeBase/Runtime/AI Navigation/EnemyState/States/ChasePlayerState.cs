using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;

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
        NavMeshAgent navMeshAgent = _enemyStateMachine.NavMeshAgent;
        Vector3 playerPosition = _enemyStateMachine.TargetEnemy.position;
        
        navMeshAgent.SetDestination(playerPosition);
    }

    public void ExitState()
    {
        
    }
}
