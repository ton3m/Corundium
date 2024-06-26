using System;
using UnityEngine;
using Zenject;

public class InputHandler : IInputHandler
{
    public event Action<Vector2> RotateInputChanged = delegate { };
    public event Action<Vector2> MoveInputChanged = delegate { };
    public event Action<bool> JumpInputPressed = delegate { };
    public event Action AttackPerformed = delegate { };
    public event Action GetToolPerformed = delegate { };
    public event Action InteractPerformed = delegate { };
    public event Action RadialMenuPerformed = delegate { };
    public event Action RadialMenuClosed = delegate { };
    public event Action EscPerformed = delegate { };
    public event Action InventoryPerformed = delegate { };
    public event Action RespawnPerformed = delegate { };


    private Input _input;
    private Input Input => _input ??= new Input();
    private IPauseService _pauseService;

    [Inject]
    public void Construct(IPauseService pauseService)
    {
        _pauseService = pauseService;
    }

    public void Enable()
    {
        _pauseService.PauseActivated += Input.Disable;
        _pauseService.PauseDeActivated += Input.Enable;

        Input.Gameplay.Rotate.performed += ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());

        Input.Gameplay.Move.performed += ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled += ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed += ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled += ctx => JumpInputPressed?.Invoke(false);
        
        Input.Gameplay.Attack.performed += ctx => AttackPerformed?.Invoke();
        Input.Gameplay.GetTool.performed += ctx => GetToolPerformed?.Invoke();
        
        Input.Gameplay.Interact.performed += ctx => InteractPerformed?.Invoke();
        
        Input.Gameplay.OpenRadialMenu.performed += ctx => RadialMenuPerformed?.Invoke();
        Input.Gameplay.OpenRadialMenu.canceled += ctx => RadialMenuClosed?.Invoke();

        Input.Gameplay.Esc.performed += ctx => EscPerformed?.Invoke();

        Input.Gameplay.Inventory.performed += ctx => InventoryPerformed?.Invoke();
        Input.Death.Respawn.performed += ctx => RespawnPerformed?.Invoke();

        Input.Enable();
    }

    public void Disable()
    {
        Input.Disable();

        _pauseService.PauseActivated -= Input.Disable;
        _pauseService.PauseDeActivated -= Input.Enable;

        Input.Gameplay.Rotate.performed -= ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());

        Input.Gameplay.Move.performed -= ctx => OnMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled -= ctx => OnMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed -= ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled -= ctx => JumpInputPressed?.Invoke(false);
        
        Input.Gameplay.Attack.performed -= ctx => AttackPerformed?.Invoke();
        
        Input.Gameplay.GetTool.performed -= ctx => GetToolPerformed?.Invoke();
        
        Input.Gameplay.Interact.performed -= ctx => InteractPerformed?.Invoke();
        
        Input.Gameplay.OpenRadialMenu.performed -= ctx => RadialMenuPerformed?.Invoke();
        Input.Gameplay.OpenRadialMenu.canceled -= ctx => RadialMenuClosed?.Invoke();

        Input.Gameplay.Esc.performed -= ctx => EscPerformed?.Invoke();
        
        Input.Gameplay.Inventory.performed -= ctx => InventoryPerformed?.Invoke();
        Input.Death.Respawn.performed -= ctx => RespawnPerformed?.Invoke();
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
