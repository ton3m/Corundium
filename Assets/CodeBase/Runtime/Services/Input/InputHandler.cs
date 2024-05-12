using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> RotateInputChanged = delegate { };
    public event Action<Vector2> MoveInputChanged = delegate { };
    public event Action JumpInputPressed = delegate { };
    private  Vector2 _rotate;
    private  Vector2 _inputmove;
    private bool _jump;

    public Vector2 Rotate =>  _rotate;
    public Vector2 Move =>  _inputmove;
    public bool Jump =>  _jump;

    private Input _input;
    private Input Input => _input ??= new Input();

    void Start()
    {
        Input.Gameplay.Rotate.performed += ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Rotate.canceled += ctx => OnRotateInputChanged(Vector2.zero); // Q test

        Input.Gameplay.Move.performed += ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled += ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed += ctx => _jump = true; //JumpInputPressed?.Invoke();
        Input.Gameplay.Jump.canceled += ctx => _jump = false;  // JumpInputPressed?.Invoke();

        Input.Gameplay.Jump.performed += ctx => JumpInputPressed?.Invoke();
        //Input.Gameplay.Jump.canceled += ctx => JumpInputPressed?.Invoke();

        Input.Enable();
    }

    private void OnRotateInputChanged(Vector2 direction)
    {
        RotateInputChanged?.Invoke(direction);
        _rotate  = direction;
    }

    private void OnMoveInputChanged(Vector2 direction)
    {
        MoveInputChanged?.Invoke(direction);
        _inputmove = direction;
    }
}
