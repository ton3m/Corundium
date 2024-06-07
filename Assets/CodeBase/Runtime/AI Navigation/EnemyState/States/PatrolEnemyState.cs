using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class PatrolEnemyState : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private readonly EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;

    private IEnumerator _corutine;
    private float _timeBeetweenWalk;
    
    public PatrolEnemyState(EnemyStateMachine enemyStateMachine, EnemyInstance enemyInstance)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyInstance = enemyInstance;
    }

    public void EnterState()
    {
        Debug.Log("Start Patrol");
        _corutine = PatrolCoroutine();
        _enemyInstance.StartCoroutine(_corutine);
    }
    
    public void ExitState()
    {
        Debug.Log("Stop Patrol");
        _enemyInstance.StopCoroutine(_corutine);
    }
    IEnumerator PatrolCoroutine()
    {
        while(true)
        {
            Patrol();
            _timeBeetweenWalk = Random.Range(3,10);
            yield return new WaitForSeconds(_timeBeetweenWalk);
        }
    }
    private void Patrol()
    {
        Vector3 randomVector = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        _enemyStateMachine.NavMeshAgent.SetDestination(randomVector + _enemyInstance.transform.position);
        
    }

    
}
