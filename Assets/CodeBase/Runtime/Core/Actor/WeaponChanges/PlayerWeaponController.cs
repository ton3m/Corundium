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
    private Mesh _meshToolModule;
    private bool _isToolInHandle = false;
    
    private ITool[] _availableModule;
    private ITool _currentTool;
    private ITool _baseTool;
    private int _selectToolModuleId;
    
    
    private IInputHandler _inputHandler;
    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    private void Start()
    {
        _baseTool = new DefaultTool(10);
        _availableModule = new ITool [] { new Axe(_baseTool), new PickAxe(_baseTool), new Key(_baseTool) };
        
        _inputHandler.GetToolPerformed += GetTool; 
    }
    private void OnDisable(IInputHandler inputHandler)
    {
        inputHandler.GetToolPerformed -= GetTool;
    }
    public void SetToolModuleId(int id)
    {
        _selectToolModuleId = id;
    }
    private void GetTool()
    {
        _isToolInHandle = !_isToolInHandle;
        _ToolObject.SetActive(_isToolInHandle);
    }
    public int CalculateDamage(Type hitObjectType)
    {
        return _currentTool.CalculateDamage(hitObjectType); 
    }

    private void ChangeModule(int selected)
    {
        _currentTool = _availableModule[selected - 1];
        //_meshToolModule.Fil
    }
    
    
}



