using System;
using System.Collections.Generic;
using CodeBase.ItemsSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RadialMenuSelector : MonoBehaviour
{
    [SerializeField] private GameObject _radialMenuRoot;

    [SerializeField] private List<RadialMenuSectorView> _sectors = new();

    public Vector2 NormalMousePos;
    public float CurrentAngle;
    public int Selection = 2;
    public int PrevSelection;
    [SerializeField] private int _offSet = -30;

    [SerializeField] private MeshFilter _meshToolModule;
    [SerializeField] private Mesh[] _availableMesh;

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
        _sectors[Selection].Select();
        //PrevSelection = Selection;

        _inputHandler.RadialMenuPerformed += OpenRadialMenu;
        _inputHandler.RadialMenuClosed += CloseRadialMenu;
    }

    private void Update()
    {
        if (!_playerWeaponController.IsHavePickAxe)
        {
            _sectors[1].Deselect();
        }

        if (!_playerWeaponController.IsHaveSword)
        {
            _sectors[0].Deselect();
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
            _sectors[PrevSelection].Select();
        }
        else if (!_playerWeaponController.IsHavePickAxe && Selection == 1)
        {
            _sectors[PrevSelection].Select();
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
            _sectors[Selection].Select();
            _sectors[PrevSelection].Deselect();
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

    public class RadialMenu
    {
        //private List<RadialMenuSector> _sectors;

        public RadialMenu()
        {
        }
    }
}

public class RadialMenu
{
    private RadialMenuView _view;
    private List<ItemData> _items;

    public void Construct() =>
        _items = new List<ItemData>(_view.SectorsCount);

    public void SetItem(int index, ItemData item)
    {
        ValidateIndex(index);

        if (_items[index] is null)
            ChangeItem(index, item);
    }


    public ItemData RemoveItem(int index)
    {
        ValidateIndex(index);

        if (_items[index] is not null)
        {
            ItemData removed = new ItemData(Id);
            ChangeItem(index, null);
        }

        return null;
    }

    private void ChangeItem(int index, ItemData item)
    {
        ItemData current = _items[index];
        current = item;

        if (current is null)
            _view.ClearSector(index);
        else
            _view.SetSector(index, current);
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= _items.Count)
            throw new IndexOutOfRangeException();
    }
}