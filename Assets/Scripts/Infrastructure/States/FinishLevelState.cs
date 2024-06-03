public class FinishLevelState : IState
{
    private GameStateMachine _gameStateMachine;
    private IGameFactory _gameFactory;
    private Shape _shape;
    
    public FinishLevelState(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        BriefingPopUp.nextLevel += NextLevelLoad;
    }

    ~FinishLevelState()
    {
        BriefingPopUp.nextLevel -= NextLevelLoad;
    }
    
    public void Exit()
    {
        
    }

    private void NextLevelLoad()
    {
        _shape.SetStartPosition(false);
        _gameStateMachine.Enter<LoadNewLevelState>();
    }

    public void Enter()
    {
        _gameFactory = GameFactory.instance; //ToDo inject
        _shape = _gameFactory.GetShape();
        _shape.CollectBlock();
    }
}