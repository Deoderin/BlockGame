using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BaseLevelConfig", fileName = "BaseLevelConfig", order = 0)]
public class BaseLevelConfig : ScriptableObject
{
    [field:SerializeField]
    public Shape MainShape {get;private set;}
    
    [field:SerializeField]
    public Wall BaseWall {get;private set;}
}

[Serializable]
public class WallContainer
{
    [field: SerializeField]
    public int Level {get;private set;}

    [field: SerializeField]
    public List<Wall> LevelWall {get; private set;}
}