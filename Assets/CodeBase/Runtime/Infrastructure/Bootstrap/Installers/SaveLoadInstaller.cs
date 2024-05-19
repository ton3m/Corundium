using Zenject;

public class SaveLoadInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SaveLoadManager saveLoadManager = new SaveLoadManager();
 
        Container.Bind<ISaveLoadManager>().To<SaveLoadManager>().FromInstance(saveLoadManager).AsSingle();
    }
}