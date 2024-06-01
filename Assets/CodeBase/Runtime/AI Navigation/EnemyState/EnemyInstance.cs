using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class EnemyInstance: MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
    private EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private Vector3 _playerPosition;
    
    [SerializeField] private LayerMask _layerMaskPlayer;
    [SerializeField] private float _viewRadius = 6f;
    private bool _isPlayerVisible;
    private Collider[] _noticedTarget = new Collider[1];
    private readonly float _viewAngle = 90f;
    public bool DoChase;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, this);
        
        _navMeshSurface.BuildNavMesh();
        
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
    }

    private void FixedUpdate()
    {
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
        
        float distanceToThePlayer = Vector3.Distance(_playerPosition, transform.position);
        if (distanceToThePlayer >= _viewRadius)
        {
            _target = null;
            _noticedTarget[0] = null;
            _isPlayerVisible = false;
        }
        
        float angle = Vector3.Angle(transform.forward, direction);
        
        if (!_isPlayerVisible && angle < _viewAngle)
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
