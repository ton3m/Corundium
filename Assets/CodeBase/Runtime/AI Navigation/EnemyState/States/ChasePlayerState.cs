using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class ChasePlayerState : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private Vector3 _playerPosition;
    private NavMeshAgent _navMeshAgent;
    private bool _isState;
    public ChasePlayerState(EnemyInstance enemyInstance)
    {
        _enemyInstance = enemyInstance;
    }

    public void EnterState()
    {
        StartChase();
    }
    public void ExitState()
    {
        StopChase();
    }
    
    private void StartChase()
    {
        _enemyInstance.DoChase = true;
    }
    private void StopChase()
    {
        _enemyInstance.DoChase = false;
    }
}
