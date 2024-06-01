using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class GameFactory : IGameFactory
{
    private BaseLevelConfig _levelConfig;
    private Shape _currentShape;
    
    public GameFactory(BaseLevelConfig config)
    {
        _levelConfig = config;
    }
    
    public Shape GetShape(int levelNumber) => Object.Instantiate(_levelConfig?.LevelShape[levelNumber]);
    public Wall GetWall(int levelNumber, int wallNumber) => Object.Instantiate(_levelConfig?.LevelWallContainer[levelNumber]?.LevelWall[wallNumber]);
}

public interface IGameFactory
{
    Shape GetShape(int levelNumber);    
    Wall GetWall(int levelNumber, int wallNumber);
}