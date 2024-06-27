using CodeBase.Runtime.Core.Actor.SuperStates;
using UnityEngine;
using Mirror;
using Zenject;

namespace CodeBase.Runtime.Core.Transport
{
    public class TransportMovementController : NetworkBehaviour, ITransport
    {
        [Header("Transport Parts")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform _driverPlace;
        
        [Header("Transport Settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("Physics Settings")] 
        [SerializeField] private float _drag = 2f;
        [SerializeField] private float _angularDrag = 2f;
        
        private PlayerMovementStateMachine _movementStateMachine;
        private IInputHandler _inputHandler;
        private Transform _driver;
        private Transform _motorObject;
        private Vector3 _currentMoveDirection;
        private bool _isPlayerInTransport;

        [Inject]
        public void Construct(PlayerMovementStateMachine movementStateMachine, IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _movementStateMachine = movementStateMachine;
        }

        private void Start()
        {
            _motorObject = transform;
            _isPlayerInTransport = false;

            _rb.drag = _drag;
            _rb.angularDrag = _angularDrag;
            _rb.constraints = (RigidbodyConstraints)84;
        }

        private void OnEnable() =>
            _inputHandler.TransportMoveInputChanged += SetMoveDirection;

        private void OnDisable() =>
            _inputHandler.TransportMoveInputChanged -= SetMoveDirection;
        
        private void SetMoveDirection(Vector2 newMoveDirection) => 
            _currentMoveDirection = newMoveDirection;

        public void Interact(Transform sender)
        {
            _isPlayerInTransport = !_isPlayerInTransport;

            if (_isPlayerInTransport)
                SitInTransport(sender);
            else
                StandUp(sender);
        }

        private void SitInTransport(Transform sender)
        {
            sender.parent = _driverPlace;
            sender.position = _driverPlace.position;
            sender.rotation = _driverPlace.localRotation;

            _movementStateMachine.EnterIn<PlayerInTransportState>();
        }

        private void StandUp(Transform sender)
        {
            sender.parent = null;

            _movementStateMachine.EnterIn<PlayerOnFootState>();
        }
        
        private void FixedUpdate()
        {
            _rb.AddTorque(0f, _currentMoveDirection.x * _rotationSpeed * Time.fixedDeltaTime, 0f);
            _rb.AddForce(_motorObject.forward * (_currentMoveDirection.y * _moveSpeed * Time.fixedDeltaTime));
        }
    }
}