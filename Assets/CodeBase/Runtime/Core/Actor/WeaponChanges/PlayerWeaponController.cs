using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerWeaponController : NetworkBehaviour
{
    [SerializeField]private GameObject _ToolObject;
    public bool _isToolInHand = false;
    
    private ITool _baseTool;
    public ITool _currentTool;
    private ITool[] _availableModule;
    
    private IInputHandler _inputHandler;
    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    private void Start()
    {
        _baseTool = new DefaultTool(10);
        
        _availableModule = new ITool [] {new Key(_baseTool), new PickAxe(_baseTool), new Axe(_baseTool)};
        _inputHandler.GetToolPerformed += GetTool; 
    }

    private void OnDisable()
    {
        _inputHandler.GetToolPerformed -= GetTool;
    }
    private void GetTool()
    {
        _isToolInHand = !_isToolInHand;
        _ToolObject.SetActive(_isToolInHand);
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
}



