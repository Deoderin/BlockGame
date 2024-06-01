public class LoadProgressState : IState
{
    /*
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressServices _persistentProgress;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressServices persistentProgress, ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _persistentProgress = persistentProgress;
        _saveLoadService = saveLoadService;
    }
*/
    public void Enter()
    {
        LoadProgressOrInitNew();
        //_gameStateMachine.Enter<LoadLevelState, string>(_persistentProgress.Progress.worldData.PositionOnLevel.Level);
    }

    public void Exit() { }

    private void LoadProgressOrInitNew()
    {
        //_persistentProgress.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
        return new PlayerProgress(0);
    }
}