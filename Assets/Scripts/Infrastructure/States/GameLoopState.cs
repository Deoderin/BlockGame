using UnityEngine;

public class GameLoopState : IState
{
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;    
    private IUpdateableServices _updateableServices;
    private ShapePusher _shapePusher;
    
    public GameLoopState(GameStateMachine gameStateMachine)
    {
        _stateMachine = gameStateMachine;
    }

    public void Exit()
    {
        _updateableServices.UnRegisterUpdatableSystem(_shapePusher);
        
        Debug.LogError("KEKA");
        //_stateMachine.Enter<PendingUIState>();
    }

    public void Enter()
    {
        _gameFactory = GameFactory.instance; //ToDo inject
        _updateableServices = UpdatableServices.instance; //ToDo inject
        
        Camera camera = Camera.main;

        var cameraFollow = camera.GetComponent<CameraFollow>();
        var shape = _gameFactory.GetShape();
        
        _shapePusher = new ShapePusher();
        _shapePusher.SetShape();
        _updateableServices.RegisterUpdatableSystem(_shapePusher);
        
        cameraFollow.SetTargets(shape.transform); //ToDo deleted this and fix inject
    }
}