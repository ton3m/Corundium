using Zenject;

public class InputInstaller : MonoInstaller
{
    private InputHandler _inputHandler;

    public override void InstallBindings()
    {
        _inputHandler = new InputHandler();
        _inputHandler.Enable();

        Container.Bind<IInputHandler>().To<InputHandler>().FromInstance(_inputHandler).AsSingle();
    }

    private void OnDestroy()
    {
        _inputHandler?.Disable();
    }
}