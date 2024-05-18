using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseBootstrap : MonoBehaviour
{
    [SerializeField] private LightHouseData _data;
    [SerializeField] private LightHouseView _view;
    [SerializeField] private MeshFilter _filter;

    void Start()
    {
        Debug.Log("Light House Init");
        LightHouseStateMachine stateMachine = new(_data, _filter);
        LightHousePresenter presenter = new LightHousePresenter(stateMachine);
    
        _view.Init(presenter);
    }
}
