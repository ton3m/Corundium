using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class EnemyAiSipo : MonoBehaviour
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

    private float _time = 0;
    private float _repeatRate = 5;

    private void Start()
    {
        _surface.BuildNavMesh();
        InitComponentLinks();
        InvokeRepeating("Patrol", _time, _repeatRate);
    }
    
    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Patrol()
    {
        Vector3 randomVector = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        _navMeshAgent.SetDestination(randomVector + transform.position);
        _repeatRate = Random.Range(2, 8);
    }
}