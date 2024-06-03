using UnityEngine;

public class PendingUIState : IState
{
    private GameStateMachine _stateMachine;
    private CameraFollow _cameraFollow;
    private GameGUI _gameGUI;
    
    public PendingUIState(GameStateMachine gameStateMachine)
    {
        _stateMachine = gameStateMachine;

        SetupState();
    }
    
    ~PendingUIState()
    {
        GameGUI.play -= StartGameLoopState;
    }

    private void SetupState()
    {
        GameGUI.play += StartGameLoopState;
    }

    private void StartGameLoopState() => _stateMachine.Enter<GameLoopState>();
    
    public void Exit()
    {
        _gameGUI.CloseUI();
    }

    public void Enter()
    {
        _gameGUI = _gameGUI == null ? GameGUI.instance : _gameGUI;
        _gameGUI.OpenUI();

        //_cameraFollow = _cameraFollow == null ? Camera.main.GetComponent<CameraFollow>() : _cameraFollow;
        //_cameraFollow.OpenUI();
        //_stateMachine.Enter<GameLoopState>();
    }
}