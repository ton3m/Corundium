using System.Runtime.InteropServices;
using CodeBase.Runtime.Core.Inventory;
using UnityEngine;
using Zenject;

public class LightHouseView : MonoBehaviour
{
    private LightHouseStateMachine _model;
    
    
    public void Init(LightHouseStateMachine model)
    {
        _model = model;
    }

    public void Repair()
    {
        Debug.Log("REPAIR LIGHTHOUSE");
        _model.SetNewStateByID(1); // 1 - id for second state of lighthouse, where 1 is not repaired and 2 - repaired
    }
}
