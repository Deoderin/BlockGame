using System;
using UnityEngine;

public class GameLoopState : IState
{
    public static Action startLevel;
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;    
    private IUpdateableServices _updateableServices;
    private ShapePusher _shapePusher;
    
    public GameLoopState(GameStateMachine gameStateMachine)
    {
        _stateMachine = gameStateMachine;
        Shape.levelCompleted += LevelCompleted;
    }

    ~GameLoopState() => Shape.levelCompleted -= LevelCompleted;

    public void Exit()
    {
        _updateableServices.UnRegisterUpdatableSystem(_shapePusher);
    }

    public void Enter()
    {
        _gameFactory = GameFactory.instance; //ToDo inject
        _updateableServices = UpdatableServices.instance; //ToDo inject
        
        Camera camera = Camera.main;

        var cameraFollow = camera.GetComponent<CameraFollow>(); //ToDo deleted this and fix inject
        var shape = _gameFactory.GetShape();
        
        shape.RebuildShape();
        _shapePusher = new ShapePusher();
        _shapePusher.SetShape();
        _updateableServices.RegisterUpdatableSystem(_shapePusher);
        
        cameraFollow.SetTargets(shape.transform); //ToDo deleted this and fix inject
        
        startLevel?.Invoke();
    }

    private void LevelCompleted()
    {
        _stateMachine.Enter<FinishLevelState>();
    }
}