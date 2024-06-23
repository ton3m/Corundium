using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

[Serializable]
public class AssetReferenceLightHouseData : AssetReferenceT<LightHouseData>
{
    public AssetReferenceLightHouseData(string guid) : base(guid) {}
}

public class LightHouseBootstrap : MonoBehaviour
{
    [SerializeField] private LightHouseView _view;
    [SerializeField] private MeshFilter _filter;
    [SerializeField] private AssetReferenceLightHouseData _lightHouseDataReference;
    private IAssetProvider _assetProvider;
    private LightHouseData _data;

    [Inject]
    public void Construct(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    private async void Start()
    {
        _data = await _assetProvider.Load<LightHouseData>(_lightHouseDataReference);
        LightHouseStateMachine stateMachine = new(_data, _filter);
    
        _view.Init(stateMachine);
    }
}
