using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CreateWorldState : IState
{
    private Shape _currentShape;
    private GameStateMachine _stateMachine;
    private List<Wall> _walls;
    
    private IGameFactory _gameFactory;
    
    public CreateWorldState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void CreateWall()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Enter()
    {
        _gameFactory = GameFactory.instance; //ToDo Inject

        _currentShape = _gameFactory.GetShape();
        _walls = _gameFactory.GetWalls(0);
        
        _stateMachine.Enter<PendingUIState>();
    }
}