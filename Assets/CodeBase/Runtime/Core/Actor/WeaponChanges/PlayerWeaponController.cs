using System;
using Mirror;
using UnityEngine;
using Zenject;

public class PlayerWeaponController : NetworkBehaviour
{
    [SerializeField] private GameObject _ToolObject;

    public ITool _currentTool;
    private ITool[] _availableModule;

    public bool IsToolInHand = false;
    
    public bool IsHaveSword = false;
    public bool IsHavePickAxe = false;
    public bool IsHaveAxe = false;

    private IInputHandler _inputHandler;

    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Start()
    {
        var tool = new DefaultTool(10);

        _availableModule = new ITool[] { new Key(tool), new PickAxe(tool), new Axe(tool) };
    }

    private void OnEnable() => 
        _inputHandler.GetToolPerformed += GetTool;

    private void OnDisable() =>
        _inputHandler.GetToolPerformed -= GetTool;

    private void GetTool()
    {
        if (IsHaveAxe)
        {
            IsToolInHand = !IsToolInHand;
            _ToolObject.SetActive(IsToolInHand);
        }
        //SetToolModuleId(_radialMenuController.Selection);
    }

    public void SetToolModuleId(int selected)
    {
        _currentTool = _availableModule[selected];
        Debug.Log(_currentTool);
    }

    public float CalculateDamage(Type hitObjectType)
    {
        return _currentTool.CalculateDamage(hitObjectType);
    }

    public bool SetToolAvailable(Type type)
    {
        if (type == typeof(ItemSword) && !IsHaveSword)
        {
            IsHaveSword = true;
            return true;
        }

        if (type == typeof(ItemPickAxe) && !IsHavePickAxe)
        {
            IsHavePickAxe = true;
            return true;
        }

        if (type == typeof(ItemAxe) && !IsHaveAxe)
        {
            IsHaveAxe = true;
            return true;
        }

        return false;
    }
}

public class ToolPicker
{

}