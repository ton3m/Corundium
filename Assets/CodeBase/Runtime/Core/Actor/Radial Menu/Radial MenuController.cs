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
    public int Selection = 0;
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
        PrevSelection = Selection;
        
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
        if (_playerWeaponController._isToolInHand)
        {
            _radialMenuRoot.SetActive(true);
            UnLockCursor();
        }
    }

    private void CloseRadialMenu()
    {
        _radialMenuRoot.SetActive(false);
        LockCursor();
        SelectModule();
    }

    private void Update()
    {
        SetElement();
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
    }

    private void SetSelection()
    {
        Selection = (int)CurrentAngle / 120;
        
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
