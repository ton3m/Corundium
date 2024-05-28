using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class EnemyInstance: MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private EnemyStateMachine _enemyStateMachine;
    
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private float _viewRadius = 6f;
    
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, this);
        _surface.BuildNavMesh();
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
    }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position, _viewRadius);
    // }
}
