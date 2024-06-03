using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        Shape.levelCompleted += DestroyWall; //ToDo create wall observer System
        GameLoopState.startLevel += BuildWall().Forget;
    }

    private async UniTaskVoid BuildWall()
    {
        var wallCells = _spatialTree.GetVectorForWall((BuildSpatialTree.Direction)WallIndex);
        float delay = 0.2f;
        for (int i = 0; i < wallCells.GetLength(1); i++)
        {
            for (int j = 0; j < wallCells.GetLength(0); j++)
            {
                if(wallCells[j, i] > 0)
                {
                    await UniTask.WaitForSeconds(delay);
                    Destroy(_wallBlocks[i].WallsLine[j].gameObject);
                }
            }
        }
    }

    private void DestroyWall()
    {
        Destroy(gameObject); //ToDo move to pool
    }

    private void OnDestroy()
    {
        Shape.levelCompleted -= DestroyWall;
        GameLoopState.startLevel -= BuildWall().Forget;
    }
}

[Serializable]
public struct WallBlocks
{
    [field:SerializeField]
    public List<GameObject> WallsLine {get;private set;}
}