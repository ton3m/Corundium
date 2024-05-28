using UnityEngine;
using Zenject;

public class GamePrefabsInstaller : MonoInstaller
{
    [SerializeField] private CustomNetworkManager _networkManagerPrefab;
    [SerializeField] private Curtain _loadingCurtain;

    public override void InstallBindings()
    {
        BindNetworkManager();
        BindLoadingCurtain();
    }

    private void BindNetworkManager()
    {
        CustomNetworkManager networkManager = Container.InstantiatePrefabForComponent<CustomNetworkManager>(_networkManagerPrefab);
        Container.Bind<CustomNetworkManager>().FromInstance(networkManager).AsSingle();
    }

    private void BindLoadingCurtain()
    {
        var curtainObject = Container.InstantiatePrefabForComponent<Curtain>(_loadingCurtain);
        Container.Bind<ILoadingCurtain>().To<Curtain>().FromInstance(curtainObject).AsSingle();
    }
}
