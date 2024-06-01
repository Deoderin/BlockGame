using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Configs/BaseLevelConfig", fileName = "BaseLevelConfig", order = 0)]
public class BaseLevelConfig : ScriptableObjectInstaller
{
    [field:SerializeField]
    public List<Shape> LevelShape {get;private set;}
    
    [field:SerializeField]
    public List<WallContainer> LevelWallContainer {get;private set;}
}

[Serializable]
public class WallContainer
{
    [field: SerializeField]
    public int Level {get;private set;}

    [field: SerializeField]
    public List<Wall> LevelWall {get; private set;}
}