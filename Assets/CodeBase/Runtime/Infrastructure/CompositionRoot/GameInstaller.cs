using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputService();
        BindSaveLoadService();
        BindSceneLoader();
        BindAssetProvider();
        BindGameStateMachine();
    }

    private void BindInputService() =>
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
    
    private void BindSaveLoadService() =>
        Container.BindInterfacesAndSelfTo<SaveLoadManager>().AsSingle();

    private void BindSceneLoader() =>
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

    private void BindAssetProvider() =>
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
    
    private void BindGameStateMachine()
    {
        Container.Bind<StateFactory>().AsSingle();
        Container.Bind<GameStateMachine>().AsSingle();
    }
}