using System;
using UnityEngine;
using Zenject;

public class ServiceLocator : MonoInstaller
{
    [SerializeField]
    private BaseLevelConfig _levelConfig;
    
    public override void InstallBindings()
    {
        var gameFactory = new GameFactory(_levelConfig);
        Container.Bind<IGameFactory>().FromInstance(gameFactory);
    }
}
