using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class EnemyInstance: MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    private EnemyStateMachine _enemyStateMachine;
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    
    [SerializeField] private LayerMask _layerMaskPlayer;
    private Collider[] _noticedTarget = new Collider[1];
    [SerializeField] private float _viewRadius = 6f;
    private float _viewAngle = 90f;

    public float distance;
    public float angle;
    private float _distancePlayersToEnemyX, _distancePlayersToEnemyZ;
    public bool _isPlayerVisible;
    public bool _playerIsWithinRange=false;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, _target, this);
        
        _surface.BuildNavMesh();
        
        _enemyStateMachine.EnterIn<PatrolEnemyState>();
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _noticedTarget, _layerMaskPlayer);
        _target = _noticedTarget[0]?.transform;

        if (_noticedTarget[0] is null)
        {
            _isPlayerVisible = false;
        }
        if (_noticedTarget[0] is not null)
        {
            Vector3 playerPosition = _target.position;
            Vector3 direction = playerPosition - transform.position;
            angle = Vector3.Angle(transform.forward, direction);

            if (angle < _viewAngle)
            {
                _isPlayerVisible = true;
                distance = Vector3.Distance(transform.position, playerPosition);
            }
            else
            {
                _isPlayerVisible = false;
            }
            if (distance < _viewRadius && !_isPlayerVisible)
            {
                _playerIsWithinRange = true;
            }
            else if (distance > _viewRadius)
            {
                _noticedTarget[0] = null;
                _playerIsWithinRange = false;
            }

            if (_isPlayerVisible || _playerIsWithinRange)
            {
                _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, _target, this);
                _enemyStateMachine.EnterIn<ChasePlayerState>();
            }
            else
            {
                _enemyStateMachine.EnterIn<PatrolEnemyState>();
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}
