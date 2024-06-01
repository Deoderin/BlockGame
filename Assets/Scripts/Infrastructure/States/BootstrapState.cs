public class BootstrapState : IState
{
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(Initial, EnterLoadLevel);
    }

    public void Exit() { }

    private void EnterLoadLevel()
    {
        _stateMachine.Enter<LoadProgressState>();
    }

    private void RegisterServices()
    {
        
        //_services.RegisterSingle<IAsset>(new AssetProvider());
        //_services.RegisterSingle<IPersistentProgressServices>(new PersistentProgressServices());
        //_services.RegisterSingle<IBoardServices>(new ChessBoardService(AllServices.Container.Single<IAsset>()));
        //_services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>(), AllServices.Container.Single<IBoardServices>()));
        //_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(AllServices.Container.Single<PersistentProgressServices>(), AllServices.Container.Single<IGameFactory>()));
        //_services.RegisterSingle<IInteractableService>(new GetInteractableObject());
    }
}


