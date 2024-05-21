using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Zenject;

public class RadialMenuController : MonoBehaviour
{
    public Transform Center;
    public Transform SelectObject;
    
    public GameObject RadialMenuRoot;

    //private ITool _moduleType;

    private int _currentWeapon;
    private string [] _nameModule = new string[]{"key","pickaxe","axe"};
    
    private IInputHandler _inputHandler;
    
    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    void Start()
    {
        // if (!isLocalPlayer)
        //     return;
        
        _inputHandler.OpenRadialMenuPerformed += OpenRadialMenu;
        _inputHandler.OpenRadialMenuClosed += CloseRadialMenu;
    }

    private void OnDisable()
    {
        _inputHandler.OpenRadialMenuPerformed -= OpenRadialMenu;
        _inputHandler.OpenRadialMenuClosed -= CloseRadialMenu;
        
    }

    private void OpenRadialMenu()
    {
        RadialMenuRoot.SetActive(true);
        CalculateAngle();
    }
    private void CloseRadialMenu()
    {
        SelectModule();
        RadialMenuRoot.SetActive(false);
    }
    
    private void CalculateAngle()
    {
        Vector2 delta = Center.position - UnityEngine.Input.mousePosition;
        float angle = Mathf.Atan2(delta.y, delta.x) + Mathf.Rad2Deg;
        angle += 180;
        int currentWeapon = 0;
        for (int i = 0; i < 360; i += 120)
        {
            if (angle >= i && angle < i + 120)
            {
                SelectObject.eulerAngles = new Vector3(0, 0, i);
            }
            _currentWeapon++;
        }
    }

    private void SelectModule()
    {
        //_moduleType = new Axe();
        Debug.Log("Selected = " + _nameModule[_currentWeapon]);
    }
}
