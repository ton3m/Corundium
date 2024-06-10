using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class RadialMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _radialMenuRoot;
    public List<RadialMenuElements> Elements = new();
    
    public Vector2 NormalMousePos;
    public float CurrentAngle;
    public int Selection=2;
    public int PrevSelection;
    [SerializeField] private int _offSet = -30;

    [SerializeField]private MeshFilter _meshToolModule;
    [SerializeField]private Mesh[] _availableMesh;
    
    [SerializeField] private PlayerWeaponController _playerWeaponController;
    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        
    }

    private void Start()
    {
        // if (!isLocalPlayer)
        //     return;
        
        //Selection = (int)CurrentAngle / 120;
        Elements[Selection].Selected();
        //PrevSelection = Selection;
        
        _inputHandler.RadialMenuPerformed += OpenRadialMenu; 
        _inputHandler.RadialMenuClosed += CloseRadialMenu;
        
    }
    
    private void Update()
    {
        
        if (!_playerWeaponController.IsHavePickAxe)
        {
            Elements[1].DeSeleted();
        }
        if (!_playerWeaponController.IsHaveSword)
        {
            Elements[0].DeSeleted();
        }
        SetElement();
    }

    private void OnDisable()
    {
        _inputHandler.RadialMenuPerformed -= OpenRadialMenu;
        _inputHandler.RadialMenuClosed -= CloseRadialMenu;
    }

    private void OpenRadialMenu()
    {
        if (_playerWeaponController.IsToolInHand && _playerWeaponController.IsHaveAxe)
        {
            _radialMenuRoot.SetActive(true);
            UnLockCursor();
        }
    }

    private void CloseRadialMenu()
    {
        _radialMenuRoot.SetActive(false);
        LockCursor();
        if (!_playerWeaponController.IsHaveSword && Selection == 0)
        {
            Elements[PrevSelection].Selected();
        }
        else if (!_playerWeaponController.IsHavePickAxe && Selection == 1)
        {
            Elements[PrevSelection].Selected();
        }
        else
        {
            SelectModule();
        }
    }

    

    private void SetElement()
    {
        CalculateAngle();
        
        SetSelection();
    }

    private void CalculateAngle()
    {
        NormalMousePos = new Vector2(UnityEngine.Input.mousePosition.x - Screen.width / 2,
            UnityEngine.Input.mousePosition.y - Screen.height / 2);
        CurrentAngle = Mathf.Atan2(NormalMousePos.y, NormalMousePos.x) * Mathf.Rad2Deg;

        CurrentAngle = (CurrentAngle + 360 + _offSet) % 360;
        
        Selection = (int)CurrentAngle / 120;
    }

    private void SetSelection()
    {
        
        if (Selection != PrevSelection)
        {
            Elements[Selection].Selected();
            Elements[PrevSelection].DeSeleted();
            PrevSelection = Selection;
        }
    }

    private void SelectModule()
    {
        _playerWeaponController.SetToolModuleId(Selection);
        _meshToolModule.mesh = _availableMesh[Selection];
        //_playerWeaponController.SetToolModuleId(Selection);
    }

    private void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
}
