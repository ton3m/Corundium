using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class RadialMenuController : MonoBehaviour
{
    public Transform Center;
    public Transform SelectObject;
    
    public GameObject RadialMenuRoot;

    [SerializeField] private int _offSet = 30;
    
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
        
        _inputHandler.RadialMenuPerformed += OpenRadialMenu;
        _inputHandler.RadialMenuClosed += CloseRadialMenu;
    }

    private void OnDisable()
    {
        _inputHandler.RadialMenuPerformed -= OpenRadialMenu;
        _inputHandler.RadialMenuClosed -= CloseRadialMenu;
    }

    private void OpenRadialMenu()
    {
        RadialMenuRoot.SetActive(true);
        
        UnlockCursor();
    }
    private void CloseRadialMenu()
    {
        RadialMenuRoot.SetActive(false);
        
        SelectModule();
    }

    private void Update()
    {
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        int oneStep = 120;
        
        Debug.Log("Center = " + Center.position);
        Vector2 delta = - Center.position + UnityEngine.Input.mousePosition;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        
        angle += (angle < 0) ? 360 : 0;
        //angle += _offSet;
        
        _currentWeapon = 0;
        
        for (int i = _offSet; i < _offSet + 360; i += oneStep)
        {
            if (angle >= i && angle < i + oneStep)
            {
                SelectObject.eulerAngles = new Vector3(0, 0, i-_offSet);
                Debug.Log(angle);
            }
            _currentWeapon++; 
        } 
    }

    private void UnlockCursor()
    {
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Cursor.visible = !Cursor.visible;
    }
    private void SelectModule()
    {
        //_moduleType = new Axe();
        //Debug.Log("Selected = " + _nameModule[_currentWeapon]);
    }
}
