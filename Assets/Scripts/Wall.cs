using System;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int WallIndex {get;set;}

    private BuildSpatialTree _spatialTree;

    [SerializeField]
    private List<WallBlocks> _wallBlocks;
    
    private void Start()
    {
        _spatialTree = BuildSpatialTree.instance; //Todo move to Inject
        BuildWall();
    }

    public void BuildWall()
    {
        var wallCells = _spatialTree.GetVectorForWall((BuildSpatialTree.Direction)WallIndex);
        
        for (int i = 0; i < wallCells.GetLength(1); i++)
        {
            for (int j = 0; j < wallCells.GetLength(0); j++)
            {
                if(wallCells[j, i] > 0)
                {
                    _wallBlocks[i].WallsLine[j].gameObject.SetActive(false);
                }
            }
        }
    }
}

[Serializable]
public struct WallBlocks
{
    [field:SerializeField]
    public List<GameObject> WallsLine {get;private set;}
}