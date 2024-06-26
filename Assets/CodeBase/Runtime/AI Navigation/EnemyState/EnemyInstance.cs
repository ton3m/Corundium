using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class EnemyInstance: MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
    private Collider[] _noticedTarget = new Collider[1];
    [SerializeField] private Animator _animator;
    private EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private Vector3 _playerPosition;
    
    [SerializeField] private LayerMask _layerMaskPlayer;
    [SerializeField] private float _viewRadius = 6f;
    private bool _isPlayerVisible;
    private float _distanceToThePlayer;
    private readonly float _viewAngle = 90f;
    
    public bool DoChase;
    public bool IsCanAttack;
    private float _angle;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = new EnemyStateMachine(_navMeshAgent,_animator, this);
        
        //_navMeshSurface.BuildNavMesh();
    }

    private void Start()
    {
        _animator.SetBool(1,true);
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
    }

    private void FixedUpdate()
    {
        float currentVelocity = _navMeshAgent.velocity.magnitude;
        _animator.SetFloat("Speed", currentVelocity);
        
        OverlapSphereCheck();

        if (_target is not null)
        {
            VisibilityCheck();
        }
        else
        {
            SetDefaultState();
            return;
        }

        if (_isPlayerVisible)
        {
            _enemyStateMachine.EnterIn<ChasePlayerState>();
        }
        if (DoChase)
        {
            ChasePlayer();
        }
        if (_distanceToThePlayer <= 4 && _distanceToThePlayer != 0 && _isPlayerVisible)
        {
            transform.LookAt(_target);
            _enemyStateMachine.EnterIn<AttackPlayer>();
        }
    }

    private void OverlapSphereCheck()
    {
        if (_target is null)
        {
            Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _noticedTarget, _layerMaskPlayer);
        }

        _target = _noticedTarget[0]?.transform;
    }
    private void VisibilityCheck()
    {
        _playerPosition= _target.position;
        Vector3 direction = _playerPosition - transform.position;
        
        _distanceToThePlayer = Vector3.Distance(_playerPosition, transform.position);
        
        if (_distanceToThePlayer >= _viewRadius)
        {
            _target = null;
            _noticedTarget[0] = null;
            _isPlayerVisible = false;
            _distanceToThePlayer = 0;
        }
        
        _angle = Vector3.Angle(transform.forward, direction);
        
        if (!_isPlayerVisible && _angle < _viewAngle)
        {
            _isPlayerVisible = true;
        }
    }
    private void SetDefaultState()
    {
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
        _isPlayerVisible = false;
    }
    private void ChasePlayer()
    {
        _navMeshAgent.SetDestination(_playerPosition);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}
