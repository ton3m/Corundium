using System;
using UnityEngine;

public class InputHandler : IInputHandler
{
    public event Action<Vector2> RotateInputChanged;
    public event Action<Vector2> MoveInputChanged;
    public event Action<bool> JumpInputPressed;
    public event Action AttackPerformed;
    public event Action InteractPerformed;
    public event Action EscPerformed;

    private Input _input;
    private Input Input => _input ??= new Input();

    public void Enable()
    {
        Input.Gameplay.Rotate.performed += ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());

        Input.Gameplay.Move.performed += ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled += ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed += ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled += ctx => JumpInputPressed?.Invoke(false);

        Input.Gameplay.Attack.performed += ctx => AttackPerformed?.Invoke();
        Input.Gameplay.Interact.performed += ctx => InteractPerformed?.Invoke();

        Input.Gameplay.Esc.performed += ctx => EscPerformed?.Invoke();

        Input.Enable();
    }

    public void Disable()
    {
        Input.Disable();

        Input.Gameplay.Rotate.performed -= ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());

        Input.Gameplay.Move.performed -= ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled -= ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed -= ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled -= ctx => JumpInputPressed?.Invoke(false);
        
        Input.Gameplay.Attack.canceled -= ctx => AttackPerformed?.Invoke();
        Input.Gameplay.Interact.canceled -= ctx => InteractPerformed?.Invoke();

        Input.Gameplay.Esc.performed -= ctx => EscPerformed?.Invoke();
    }

    private void OnRotateInputChanged(Vector2 direction)
    {
        RotateInputChanged?.Invoke(direction);
    }

    private void OnMoveInputChanged(Vector2 direction)
    {
        MoveInputChanged?.Invoke(direction);
    }
}
