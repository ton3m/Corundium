using Mirror;
using UnityEngine;


public class ActorMotor : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Camera _camera;
    [SerializeField] private InputHandler _inputHandler;

    [Header("Settings")]
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _smoothMoveDeltaTime;

    private float _yRotation;
    private Transform _motorObject;
    private Vector3 _currentMoveDirection;
    private Vector3 _newMoveDirection;
    private float _jumpForce;
    private bool _isJumpActive = false;
    private Vector3 _currentVelocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    private void Start()
    {
        _motorObject = transform;

        _inputHandler.RotateInputChanged += SetRotationDirection;
        _inputHandler.MoveInputChanged += SetMoveDirection;
        _inputHandler.JumpInputPressed += SetJumpActive;

        if (!isLocalPlayer)
            _camera.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _inputHandler.RotateInputChanged -= SetRotationDirection;
        _inputHandler.MoveInputChanged -= SetMoveDirection;
        _inputHandler.JumpInputPressed -= SetJumpActive;
    }


    private void Update()
    {
        if (!isLocalPlayer)
            return;

        UpdateGravity();

        if(_isJumpActive)
            AddJumpForce();

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
        if(_jumpForce > _gravity)
        {
            _jumpForce += _gravity * Time.deltaTime;
        } 
    }
}