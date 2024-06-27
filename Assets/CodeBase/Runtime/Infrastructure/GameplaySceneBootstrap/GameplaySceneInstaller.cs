using CodeBase.Runtime.Core.Actor.SuperStates;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneStateMachine();    
        BindStateFactory();  // i don't have a clue why i need to register this shit twice, please shoot me
        BindPlayerMovementStateMachine();
    }

    private void BindSceneStateMachine() => 
        Container.Bind<SceneStateMachine>().AsSingle();

    private void BindStateFactory() =>
        Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

    private void BindPlayerMovementStateMachine() =>
        Container.Bind<PlayerMovementStateMachine>().AsSingle();
}
