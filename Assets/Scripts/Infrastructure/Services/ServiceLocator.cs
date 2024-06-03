using UnityEngine;
using Zenject;

public class ServiceLocator : MonoInstaller
{
    [SerializeField]
    private BaseLevelConfig _levelConfig;

    [SerializeField]
    private UpdatableLocator _updatableLocator;
    
    private UpdatableServices _updatableServices;
    public static ServiceLocator instance;
    
    public override void InstallBindings()
    {
        GameFactory gameFactory = new(_levelConfig);
        _updatableServices = new UpdatableServices();
        BuildSpatialTree buildSpatialTree = new();
        
        Container.Bind<IGameFactory>().FromInstance(gameFactory).AsSingle().NonLazy();
        Container.Bind<IUpdateableServices>().FromInstance(_updatableServices).AsSingle().NonLazy();
        Container.Bind<BuildSpatialTree>().FromInstance(buildSpatialTree).AsSingle().NonLazy();
        
        _updatableLocator.SetUpdateableServices(_updatableServices);
    }
}