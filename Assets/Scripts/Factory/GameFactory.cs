using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

public class GameFactory : IGameFactory 
{
    private BaseLevelConfig _levelConfig;
    private Shape _currentShape;

    public static GameFactory instance;
    
    public GameFactory(BaseLevelConfig config)
    {
        _levelConfig = config;
        instance = this;
    }
    
    public Shape GetShape()
    {
        if(_currentShape == null)
        {
            _currentShape = Object.Instantiate(_levelConfig.MainShape);
        }

        return _currentShape;
    }

    public List<Wall> GetWalls(int levelNumber) => _levelConfig.LevelWallContainer[levelNumber].LevelWall.Select(Object.Instantiate).ToList();
}

public interface IGameFactory : IService
{
    Shape GetShape();    
    List<Wall> GetWalls(int levelNumber);
}