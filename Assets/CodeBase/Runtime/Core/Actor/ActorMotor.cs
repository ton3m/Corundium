using UnityEngine;

public class ActorMotor : MonoBehaviour
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

    private float _yRotation;
    private Transform _motorObject;
    private Vector3 _currentMoveDirection;
    private Vector3 _newMoveDirection;
    private Vector3 _jumpDirection;
    private float _jumpForce;

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
        _inputHandler.JumpInputPressed += SetJump;
    }

    private void OnDisable()
    {
        _inputHandler.RotateInputChanged -= SetRotationDirection;
        _inputHandler.MoveInputChanged -= SetMoveDirection;
        _inputHandler.JumpInputPressed -= SetJump;
    }

    private void Update()
    {
        UpdateGravity();

        _currentMoveDirection = _motorObject.right * _newMoveDirection.x + _motorObject.forward * _newMoveDirection.y;
        _currentMoveDirection = _currentMoveDirection.normalized * Time.deltaTime;

        _controller.Move(Vector3.up * _jumpForce * Time.deltaTime);

        if(_currentMoveDirection != _newMoveDirection)
        {
            _controller.Move(_currentMoveDirection * _moveSpeed);
        }
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

    private void SetJump()
    {
        _jumpForce = _jumpHeight;
    }

    private void UpdateGravity()
    {
        if(_jumpForce > _gravity)
        {
            _jumpForce += _gravity * Time.deltaTime;
        } 
    }
}