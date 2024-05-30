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
        
        if (_noticedTarget[0] is null)
        {
            _enemyStateMachine.EnterIn<PatrolEnemyState>();
            _isPlayerVisible = false;
            return;
        }
        
        _target = _noticedTarget[0].transform;
        Vector3 playerPosition = _target.position;
        Vector3 direction = playerPosition - transform.position;
        
        float angle = Vector3.Angle(transform.forward, direction);
        
        if (!_isPlayerVisible && angle < _viewAngle)
        {
            _isPlayerVisible = true;
        }
        
        if (_isPlayerVisible)
        {
            _enemyStateMachine = new EnemyStateMachine(_navMeshAgent, _target, this);
            _enemyStateMachine.EnterIn<ChasePlayerState>();
        }
        else
        {
            _enemyStateMachine.EnterIn<PatrolEnemyState>();
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}
