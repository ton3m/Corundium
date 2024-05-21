using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _viewAngel;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _stopDistance = 1.1f;
    [SerializeField] private bool _isPlayerNoticed;
    private bool _isEnemyMove=true;
    private NavMeshAgent _navMeshAgent;
    private Collider[] _players=new Collider[32];
    void Start()
    {
        _surface.BuildNavMesh();
        InitComponentLinks();
        PickNewPatrolPoint();
        _navMeshAgent.stoppingDistance = 0;
    }
    void Update()
    {
        NoticePlayerUpdate();
        DistanceToPlayer();
        PatrolUpdate();
    }
    void PickNewPatrolPoint()
    {
        //_navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }
    void PatrolUpdate()
    {

        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && !_isPlayerNoticed)
        {
            PickNewPatrolPoint();
        }
    }
    void InitComponentLinks() => _navMeshAgent = GetComponent<NavMeshAgent>();
    void NoticePlayerUpdate()
    {
        //var direction = player.transform.position - transform.position;
        //_isPlayerNoticed = false;

        //if (Vector3.Angle(transform.forward, direction) < viewAngel)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
        //    {
        //        if (hit.collider.gameObject == player.gameObject)
        //        {
        //            _isPlayerNoticed = true;
        //        }
        //        else
        //    }
        //}

        Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _players, _layerMask);
        if (_players[0]!=null&&_isEnemyMove)
        {
            _navMeshAgent.destination = _players[0].transform.position;
        }
        
    }
    void DistanceToPlayer()
    {
        if (_navMeshAgent.destination != null) return;
        var distance = _navMeshAgent.destination-transform.position;
        
        if(distance.magnitude<=5)
        {
            _isEnemyMove = false;
        }
        else
        {
            _isEnemyMove = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}
