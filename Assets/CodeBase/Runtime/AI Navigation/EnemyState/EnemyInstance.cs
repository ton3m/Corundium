using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class EnemyInstance: MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private EnemyStateMachine _enemyStateMachine;
    private Transform _target;
    [SerializeField] private float _viewRadius = 6f;
    
    [SerializeField] private NavMeshSurface _surface;
    private Collider[] _noticedTarget = new Collider[1];
    [SerializeField] private LayerMask _layerMaskPlayer;
    private double _viewAngle;
    private bool _isPlayerVisible;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, _target, this);
        
        _surface.BuildNavMesh();
        
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
    }

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _noticedTarget, _layerMaskPlayer);
        
        //добавить проверку на то что бы нельзя было входить в одно состояние несколько раз
        //да, нихуя не работает еще и ломается все, состояние дрочится
        if (_noticedTarget[0] == null)
        {
            //_enemyStateMachine.EnterIn<PatrolEnemyState>();
            _isPlayerVisible = false;
            return;
        }
        //досюда даже не доходит без переключения, не обрабатывает от иф сверху
        Debug.Log("player is sphere");
        _target = _noticedTarget[0].transform;
        Vector3 playerPosition = _target.position;
        Vector3 direction = playerPosition  - transform.position;
        
        float angle = Vector3.Angle(_enemyStateMachine.TargetEnemy.forward, direction);
        
        _isPlayerVisible = angle < _viewAngle;

        if (_isPlayerVisible)
        {
            _enemyStateMachine.EnterIn<ChasePlayerState>();
        }
        else
        {
            _enemyStateMachine.EnterIn<PatrolEnemyState>();
        }
        
    }
}