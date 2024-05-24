using UnityEngine;
using Zenject;

public class LightHouseBootstrap : MonoBehaviour
{
    [SerializeField] private LightHouseView _view;
    [SerializeField] private MeshFilter _filter;
    private LightHouseData _data;

    [Inject]
    public void Construct(IAssetProvider assetProvider)
    {
        _data = assetProvider.Load<LightHouseData>(GameAssetPaths.ScriptableObjectPath + "/LightHouse/LightHouseData");
        Debug.Log("Data injected " + _data);
    }

    void Start()
    {
        LightHouseStateMachine stateMachine = new(_data, _filter);
        LightHousePresenter presenter = new LightHousePresenter(stateMachine);
    
        _view.Init(presenter);
    }
}
