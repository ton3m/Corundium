using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeItems : NetworkBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private float _maxDistanceRaycast = 1.5f;
    private int _countStone;
    private RaycastHit _hit;
    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        _inputHandler.TakeInputPressed += Take;
    }
    private void OnDisable()
    {
        _inputHandler.TakeInputPressed -= Take;
    }
    private void Take()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit, _maxDistanceRaycast))
        {
            if (_hit.transform.TryGetComponent(out IResource resource))
            {
                _countStone += resource.Quantity;
                Destroy(_hit.transform.gameObject);
            }
        }
    }
    
}