using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _stopDistance = 1.1f;
    [SerializeField] private bool _isPlayerVisibility=false;
    private Collider[] _players=new Collider[1];
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _surface.BuildNavMesh();
        InitComponentLinks();
        PickNewPatrolPoint();
    }
    private void Update()
    {
        PlayerVisibilityUpdate();
    }
    private void PickNewPatrolPoint()
    {
        //_navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }
    private void PatrolUpdate()
    { 
        _navMeshAgent.destination = Vector3.up * 10;
        //if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && !_isPlayerVisibility)
        //{
        //}
    }
    private void InitComponentLinks() => _navMeshAgent = GetComponent<NavMeshAgent>();
    private void PlayerVisibilityUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _players, _layerMask);

        if (_players[0] != null)
        {
            FollowsThePlayer();
        }
        else if (_playerTransform != null)
        {
            NotFollowsThePlayer();
        }
        else
        {
            PatrolUpdate();
        }
    }
   private void FollowsThePlayer()
    {
        _playerTransform = _players[0].transform;
        var playerPosition = _playerTransform.position;
        var direction = playerPosition - transform.position;

        if (Vector3.Angle(transform.forward, direction) < _viewAngle)
        {
            _isPlayerVisibility = true;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
            if (distanceToPlayer <= _stopDistance)
            {
                _navMeshAgent.destination = playerPosition;
                _navMeshAgent.isStopped = false;
            }
            else
            {
                PatrolUpdate();
            }

        }
        else
        {
            PatrolUpdate();
        }
    }
    private void NotFollowsThePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer > _stopDistance)
        {
            PatrolUpdate();
            _playerTransform = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}
