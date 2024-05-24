
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    private InputHandler _inputHandler;

    public override void InstallBindings()
    {
        Debug.Log("Init Input");
        _inputHandler = new InputHandler();
        _inputHandler.Enable();

        Container.Bind<IInputHandler>().To<InputHandler>().FromInstance(_inputHandler).AsSingle();
    }

    private void OnDestroy()
    {
        _inputHandler?.Disable();
    }
}