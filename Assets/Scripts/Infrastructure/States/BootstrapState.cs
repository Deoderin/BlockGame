using UnityEngine;
using Zenject;

public class BootstrapState : IState
{
    private const string GameSceneName = "Game";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly SceneContext _serviceLocator;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, SceneContext serviceLocator)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _serviceLocator = serviceLocator;
    }

    public void Enter()
    {
        RegisterServices();
        
        _sceneLoader.Load(GameSceneName, EnterLoadLevel);
    }

    public void Exit()
    {
        
    }

    private void RegisterServices()
    {
        _serviceLocator.Install();
    }

    private void EnterLoadLevel() => _stateMachine.Enter<CreateWorldState>();
}