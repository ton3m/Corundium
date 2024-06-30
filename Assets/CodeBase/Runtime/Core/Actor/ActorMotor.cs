using CodeBase.Runtime.Core.Actor.SuperStates;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


public class ActorMotor : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Camera _camera;
    [SerializeField] private Renderer _model;
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _smoothMoveDeltaTime;
    
    private IInputHandler _inputHandler;
    private PlayerMovementStateMachine _movementStateMachine;
    private IState _currentState;
    private Transform _motorObject;
    private Vector3 _currentMoveDirection;
    private Vector3 _newMoveDirection;
    private Vector3 _currentVelocity;
    private float _yRotation;
    private float _jumpForce;
    private bool _isJumpActive;
    private bool _isMoveActive;

    [Inject]
    public void Construct(IInputHandler inputHandler, PlayerMovementStateMachine movementStateMachine)
    {
        _movementStateMachine = movementStateMachine;
        _inputHandler = inputHandler;
    }

    private void Awake() // потом заменить на вызов курсор сервиса в глобальной стейт машине
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Start()
    {       
=        _isMoveActive = true;
>        _motorObject = transform;

        OnEnable();
        
        if (!isLocalPlayer)
        {
            _camera.gameObject.SetActive(false);
        }
        if (isLocalPlayer)
        {
            _model.enabled = false;
        }
        
        
    }

    private void OnEnable()
    {
        if (_inputHandler == null)
            return;
        
        _inputHandler.RotateInputChanged += SetRotationDirection;
        _inputHandler.PlayerMoveInputChanged += SetMoveDirection;
        _inputHandler.JumpInputPressed += SetJumpActive;
        _movementStateMachine.StateChanged += UpdateMovementState;
    }
    
    private void OnDisable()
    {
        _inputHandler.RotateInputChanged -= SetRotationDirection;
        _inputHandler.PlayerMoveInputChanged -= SetMoveDirection;
        _inputHandler.JumpInputPressed -= SetJumpActive;
        _movementStateMachine.StateChanged -= UpdateMovementState;
    }


    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        float velocity = _controller.velocity.magnitude;
        
        _animator.SetFloat("Velocity", velocity);
        
        UpdateGravity();

<       if (!_isMoveActive)
            return;

        if(_isJumpActive)
>            AddJumpForce();
            
        }

        Vector3 moveVector = transform.TransformDirection(new Vector3(_newMoveDirection.x, 0, _newMoveDirection.y)).normalized;

        _currentMoveDirection.y = _jumpForce;
        _currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection, moveVector * _moveSpeed, ref _currentVelocity, _smoothMoveDeltaTime);
        
        _controller.Move(_currentMoveDirection * Time.deltaTime);     
    }

    
    
    private void SetRotationDirection(Vector2 rotation)
    {
        rotation = rotation * _rotateSpeed * Time.deltaTime;

        _yRotation -= rotation.y;
        _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        _motorObject.Rotate(Vector3.up * rotation.x);
    }

    private void SetMoveDirection(Vector2 moveDirection)
    {
        _newMoveDirection = moveDirection;
    }

    private void SetJumpActive(bool isJumpActive)
    {
        _isJumpActive = isJumpActive;
    }

    private void AddJumpForce()
    {
        if(_controller.isGrounded)
        {
            _jumpForce = _jumpHeight;
        }
    }

    private void UpdateGravity()
    {
        if (_jumpForce > _gravity)
        {
            _animator.SetBool("IsnotGrounded", true);
            _jumpForce += _gravity * Time.deltaTime;
        }
        else
        {
            _animator.SetBool("IsnotGrounded", false);
        }
    }

    private void UpdateMovementState(IState state)
    {
        Debug.Log("Trying Update movement state to: " + state);
        
        if (state == _currentState)
            return;

        if (state is PlayerOnFootState)
            EnableMove();
        else
            DisableMove();
        
        _currentState = state;
    }
    
    public void EnableMove()
    {
        _isMoveActive = true;
        _controller.enabled = true;
    }

    public void DisableMove()
    {
        _isMoveActive = false;
        _controller.enabled = false;
    }
}
