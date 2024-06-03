using UnityEngine;
using Zenject;

public class ServiceLocator : MonoInstaller
{
    [SerializeField]
    private BaseLevelConfig _levelConfig;

    private UpdatableServices _updatableServices;
    public static ServiceLocator instance;
    
    public override void InstallBindings()
    {
        GameFactory gameFactory = new(_levelConfig);
        _updatableServices = new UpdatableServices();
        
        Container.Bind<IGameFactory>().FromInstance(gameFactory).AsSingle().NonLazy();
        Container.Bind<IUpdateableServices>().FromInstance(_updatableServices).AsSingle().NonLazy();
    }
    
    private void FixedUpdate() => _updatableServices?.FixedUpdate();
}