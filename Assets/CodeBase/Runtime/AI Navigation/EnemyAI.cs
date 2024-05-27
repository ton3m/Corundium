using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private LayerMask _layerMaskPlayer;
    [SerializeField] private LayerMask _layerMaskAll;
    [SerializeField] private float _viewAngle = 60f; // Установим значение по умолчанию
    [SerializeField] private float _viewRadius = 10f; // Установим значение по умолчанию
    [SerializeField] private float _stopDistanceToPlayers, _stopDistanceToPoint;
    private bool _isPlayerVisible = false;
    private Collider[] _players = new Collider[1];
    private Collider[] _objectPointed = new Collider[10];
    private Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _moveEnemy;
    private Rotate _rotationEnemy;
    private int _randomRotation;
    private int _speedMoveEnemy=1;

    private void Start()
    {
        _surface.BuildNavMesh();
        InitComponentLinks();
        SetRandomPatrolPoint();
        PickNewPatrolPoint();
    }
    
    private void Update()
    {
        PlayerVisibilityUpdate();
        PatrolUpdate();
    }
    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void SetRandomPatrolPoint()
    {
        _randomRotation = Random.Range(0, 100);
        _rotationEnemy= new Rotate(_randomRotation);
        _moveEnemy = Vector3.forward*_randomRotation;
    }
    private void PickNewPatrolPoint()
    {
        var rnd = Random.RandomRange(0, 9);
        Physics.OverlapSphereNonAlloc(transform.position, 50, _objectPointed, _layerMaskAll);
        if (_objectPointed != null)
        {
            var Point = _objectPointed[rnd].transform.position;
            _navMeshAgent.destination = Point;
            float DirectionToPoint = Vector3.Distance(transform.position, Point);
            if (DirectionToPoint > _stopDistanceToPoint)
            {
                _objectPointed = null;
                //Point = _objectPointed[0].transform.position;
            }
        }
    }

    private void PatrolUpdate()
    {
        if (!_isPlayerVisible && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            Invoke("PickNewPatrolPoint", 0f);
        }
    }

    

    private void PlayerVisibilityUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _players, _layerMaskPlayer);

        if (_players[0] != null)
        {
            FollowsThePlayer();
        }
        else
        {
            NotFollowsThePlayer();
        }
    }

    private void FollowsThePlayer()
    {
        _playerTransform = _players[0].transform;
        var playerPosition = _playerTransform.position;
        var direction = playerPosition - transform.position;

        if (Vector3.Angle(transform.forward, direction) < _viewAngle)
        {
            _isPlayerVisible = true;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
            if (distanceToPlayer <= _viewRadius)
            {
                _navMeshAgent.destination = playerPosition;
                _navMeshAgent.isStopped = false;
            }
            else
            {
                _isPlayerVisible = false;
            }
        }
        else
        {
            _isPlayerVisible = false;
        }
    }

    private void NotFollowsThePlayer()
    {
        if (_playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
            if (distanceToPlayer > _viewRadius)
            {
                _isPlayerVisible = false;
                _playerTransform = null;
                _navMeshAgent.isStopped = true;
                PatrolUpdate();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _viewRadius);
    }
}