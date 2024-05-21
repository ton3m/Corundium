using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Zenject;

public class PlayerWeaponController : NetworkBehaviour
{
    // [SerializeField] private GameObject _axeModelPart;
    // private ITool _currentTool;
    // private ITool _baseTool;
    // private bool _isToolInHandle;
    //
    // public bool IsToolInHandle => _isToolInHandle;
    //
    // private IInputHandler _inputHandler;
    // [Inject]
    // public void Construct(IInputHandler inputHandler) =>
    //     inputHandler.NumberPressed += OnNumberButtonPressed; // и не забыть отписку!
    //
    // private void Start()
    // {
    //     _baseTool = new DefaultTool(10);
    // }
    // private void OnDisable(IInputHandler inputHandler)
    // {
    //     inputHandler.NumberPressed -= OnNumberButtonPressed;
    // }
    //
    // public int CalculateDamage(Type hitObjectType)
    // {
    //     _currentTool.CalculateDamage(hitObjectType);
    // }
    //
    // private void OnNumberButtonPressed(int pressedNumberIndex)
    // {
    //     if(pressedNumberIndex == _currentTool.Index)
    //     {
    //         _isToolInHandle = !_isToolInHandle;
    //         _tool.SetActive(_isToolInHandle);
    //         return;
    //     }
    //         
    //     if(pressedNumberIndex == 1)
    //         ChangeWeaponToAxe();
    // }
    //
    // private void ChangeWeaponToAxe() // если обобщить до одного метода который сам считает какой инструмент надо положить то будет круто
    // {
    //     // change Current Tool
    //     _currentTool = new Axe(_baseTool);
    //     _axeModelPart.SetActive(true);
    // }
}



