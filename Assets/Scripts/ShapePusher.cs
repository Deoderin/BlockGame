using UnityEngine;
using Zenject;

public class ShapePusher : MonoBehaviour
{
    private IGameFactory _gameFactory;
        
    [Inject]
    private void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }
    
    private void FixedUpdate()
    {
        
    }
}

public class WorldCreator
{
    private void CreateWall()
    {
        
    }

    private void CreateShape()
    {
        
    }
}

public class GameObserver : MonoBehaviour
{
    private void FixedUpdate()
    {
        
    }
}

