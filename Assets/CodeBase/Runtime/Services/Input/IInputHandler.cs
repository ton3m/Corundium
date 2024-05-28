using System;
using UnityEngine;

public interface IInputHandler
{
    event Action<Vector2> RotateInputChanged;
    event Action<Vector2> MoveInputChanged;
    event Action<bool> JumpInputPressed;
    event Action AttackPerformed;
    event Action InteractPerformed;
    event Action OpenRadialMenuPerformed;
    event Action OpenRadialMenuClosed;
    event Action EscPerformed;
}
