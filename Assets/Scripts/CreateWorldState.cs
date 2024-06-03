using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CreateWorldState : IState
{
    private Shape _currentShape;
    private GameStateMachine _stateMachine;
    private List<Wall> _walls = new();

    private int _numberWalls = 4;
    
    private IGameFactory _gameFactory;
    
    public CreateWorldState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        
    }

    public void Enter()
    {
        CreateWorld();
        _stateMachine.Enter<PendingUIState>();
    }
    
    private void CreateWorld()
    {
        _gameFactory = GameFactory.instance; //ToDo Inject
        _currentShape = _gameFactory.GetShape();

        _currentShape.SetStartPosition();
        
        for (int i = 0; i < _numberWalls; i++)
        {
            var wall = _gameFactory.GetWall();
            wall.WallIndex = i;
            wall.transform.position = new Vector3(0, 0, 75 * i);
            _walls.Add(wall);
        }
    }
}