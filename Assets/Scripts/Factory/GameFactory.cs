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

    public Wall GetWall() => Object.Instantiate(_levelConfig.BaseWall);
}

public interface IGameFactory : IService
{
    Shape GetShape();    
    Wall GetWall();
}