using UnityEngine;
using Zenject;

public class TipShowerInstaller : MonoInstaller
{
    [SerializeField] private TipsShower _tipsShower;
    public override void InstallBindings()
    {
        Container.Bind<TipsShower>().FromInstance(_tipsShower).AsSingle();
    }
}