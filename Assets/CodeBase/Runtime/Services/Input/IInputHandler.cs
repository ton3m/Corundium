using System;
using UnityEngine;

public interface IInputHandler
{
    event Action<Vector2> RotateInputChanged;
    event Action<Vector2> MoveInputChanged;
    event Action<bool> JumpInputPressed;
    event Action AttackPerformed;
    event Action InteractPerformed;
    event Action RadialMenuPerformed;
    event Action RadialMenuClosed;
    event Action EscPerformed;
}
