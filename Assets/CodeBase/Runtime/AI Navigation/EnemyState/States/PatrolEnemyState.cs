using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PatrolEnemyState : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private readonly EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;
    
    private float _timeBeetweenWalk = 0f;

    public PatrolEnemyState(EnemyStateMachine enemyStateMachine, EnemyInstance enemyInstance)
    {
        _enemyStateMachine = enemyStateMachine;
        _enemyInstance = enemyInstance;
    }

    public void EnterState()
    {
        Debug.Log("Start Patrol");
        _enemyInstance.StartCoroutine(PatrolCoroutine());
    }

    public void ExitState()
    {
        Debug.Log("Stop Patrol");
        _enemyInstance.StopCoroutine(PatrolCoroutine());
    }
    IEnumerator PatrolCoroutine()
    {
        while(true)
        {
            Patrol();
            yield return new WaitForSeconds(_timeBeetweenWalk);
        }
    }
    public void Patrol()
    {
        Vector3 randomVector = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        _enemyStateMachine.NavMeshAgent.SetDestination(randomVector + _enemyInstance.transform.position);
        _timeBeetweenWalk = Random.Range(2,8);
    }

    
}
