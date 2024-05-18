using System;
using UnityEngine;
using Mirror;

public class InputHandler : NetworkBehaviour
{
    public event Action<Vector2> RotateInputChanged = delegate { };
    public event Action<Vector2> MoveInputChanged = delegate { };
    public event Action<bool> JumpInputPressed = delegate { };
    public event Action AttackPerformed = delegate { };
    public event Action InteractPerformed = delegate { };
    public event Action EscPerformed = delegate { };

    private Input _input;
    private Input Input => _input ??= new Input();

    void Start()
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

    private void OnDisable()
    {
        Input.Gameplay.Rotate.performed -= ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());

        Input.Gameplay.Move.performed -= ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled -= ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed -= ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled -= ctx => JumpInputPressed?.Invoke(false);
        
        Input.Gameplay.Attack.canceled -= ctx => AttackPerformed?.Invoke();
        Input.Gameplay.Interact.canceled -= ctx => InteractPerformed?.Invoke();
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
