using UnityEngine;

public class LoadNewLevelState : IState
{
    private GameStateMachine _gameStateMachine;
    private IGameFactory _gameFactory;
    private IScoreSystem _scoreSystem;
    private BuildSpatialTree _spatialTree;
    private Shape _currentShape;
    
    public LoadNewLevelState(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public void Exit()
    {
        
    }

    public void Enter()
    {
        _gameFactory = GameFactory.instance;
        _scoreSystem = ScoreSystem.instance;
        _spatialTree = BuildSpatialTree.instance;
        _gameFactory.GetShape();
        CreateWorld();
    }
    
    private void CreateWorld()
    {
        float numberWalls = 4;
        _spatialTree.GenerateTreeBlueprint();
        _scoreSystem.ClearScore();
        _scoreSystem.SetMultiplier(1);
        _currentShape = _gameFactory.GetShape();
        _currentShape.SetStartPosition(true);
        
        for (int i = (int)numberWalls - 1; i >= 0; i--)
        {
            var wall = _gameFactory.GetWall();
            wall.WallIndex = i;
            wall.transform.position = new Vector3(0, 0, 75 * i);
        }
        
        _gameStateMachine.Enter<GameLoopState>();
    }
}