using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class ChasePlayerState : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private readonly EnemyStateMachine _enemyStateMachine;
    private Vector3 _playerPosition;
    private NavMeshAgent _navMeshAgent;
    private bool _isState;
    public ChasePlayerState(EnemyStateMachine enemyStateMachine, EnemyInstance enemyInstance)
    {
        _enemyInstance = enemyInstance;
        _enemyStateMachine = enemyStateMachine;
    }

    public void EnterState()
    {
        StartChase();
    }
    public void ExitState()
    {
        _enemyStateMachine.Animator.SetBool("ChaseEnemy", false);
        StopChase();
    }
    
    private void StartChase()
    {
        _enemyInstance.DoChase = true;
        SetAnimation();
    }
    private void StopChase()
    {
        _enemyInstance.DoChase = false;
        SetAnimation();
    }

    private void SetAnimation()
    {
        _enemyStateMachine.Animator.SetBool("ChaseEnemy", _enemyInstance.DoChase);
    }
}
