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
    public int Selection;
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
        // _playerWeaponController = playerWeaponController;
    }

    private void Start()
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
        if (_playerWeaponController._isToolInHandle)
        {
            _radialMenuRoot.SetActive(true);
            LockCursor();
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
        CalculateElement();
    }

    private void CalculateElement()
    {
        NormalMousePos = new Vector2(UnityEngine.Input.mousePosition.x - Screen.width / 2,
            UnityEngine.Input.mousePosition.y - Screen.height / 2);
        CurrentAngle = Mathf.Atan2(NormalMousePos.y, NormalMousePos.x) * Mathf.Rad2Deg;

        CurrentAngle = (CurrentAngle + 360 + _offSet) % 360;

        Selection = (int)CurrentAngle / 120;

        //засунуть в отдельный метод и добавить в старт и проверить будет ли изначально выбираться что то
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

    private void LockCursor()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
