using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildSpatialTree
{
    private int _height = 6;
    private int _width = 5;    
    private int _depth = 5;
    
    private int[,,] _array = { };    
    private int[,] _wallArray = { };
    private int _centerTree = 2;

    public static BuildSpatialTree instance;
    
    public BuildSpatialTree()
    {
        instance = this;
    }
    
    public void GenerateTreeBlueprint()
    {
        _array = new int[_width, _height, _depth];

        CreateMainPilar(ref _array);
        CreateBranch(ref _array);
    }

    public int[,,] GetTreeBlueprint()
    {
        return _array;
    }

    public int GetCountTree() => _array.Cast<int>().Sum(element => element == 1 ? 1 : 0);

    public List<Vector3> GetVectorWithTree()
    {
        List<Vector3> positionsForTreeElements = new List<Vector3>();
        
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                for (int k = 0; k < _depth; k++)
                {
                    if(_array[j, i, k] == 1)
                    {
                        positionsForTreeElements.Add(new Vector3(j - _centerTree, i, k - _centerTree));
                    }
                }
            }
        }

        return positionsForTreeElements;
    }

    public int[,] GetVectorForWall(Direction direction)
    {
        int[,] positionsForWallBlocks = new int[_width, _height];
        
        switch(direction)
        {
            case Direction.forward:
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < _depth; k++)
                        {
                            sum += _array[i, j, k];
                        }

                        positionsForWallBlocks[i, j] = sum;
                    }
                }

                break;
            case Direction.back:
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < _depth; k++)
                        {
                            sum += _array[k, j, i];
                        }

                        positionsForWallBlocks[i, j] = sum;
                    }
                }

                break;
            case Direction.left:
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < _depth; k++)
                        {
                            sum += _array[i, j, k];
                        }

                        positionsForWallBlocks[(_width -1) - i, j] = sum;
                    }
                }

                break;
            case Direction.right:
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < _depth; k++)
                        {
                            sum += _array[k, j, i];
                        }

                        positionsForWallBlocks[(_width -1) - i, j] = sum;
                    }
                }

                break;
        }

        return positionsForWallBlocks;
    }

    private void CreateMainPilar(ref int[,,] array)
    {
        int deviationStart = Random.Range(2, _height);
        int deviation = Random.Range(-1, 2);

        for (int i = 0; i < _height; i++)
        {
            int deviationApplied = 0;

            if(i >= deviationStart)
            {
                deviationApplied = deviation;
            }

            if(i == deviationStart)
            {
                array[_centerTree, i, _centerTree] = 1;
            }

            array[_centerTree + deviationApplied, i, _centerTree] = 1;
        }
    }

    private void CreateBranch(ref int[,,] array)
    {
        int branchesCount = 4;
        var branches = new List<Branch>();
        
        for (int i = 0; i < branchesCount; i++)
        {
            branches.Add(new Branch()
            {
                length = GetBranchCount(),
                spawnHeight = GetBranchHeight(),
                direction = (Direction)i
            });
        }

        for (int i = 0; i < branchesCount; i++)
        {
            int width = _centerTree;            
            int depth = _centerTree;
            
            array[width, branches[i].spawnHeight, depth] = 1;
            
            for (int j = 1; j < branches[i].length; j++)
            {
                width += ParseDirection(branches[i].direction).Item1;
                depth += ParseDirection(branches[i].direction).Item2;
                
                array[width, branches[i].spawnHeight, depth] = 1;
            }
        }
    }

    public int[,] CreateWallArrayMap()
    {
        _wallArray = new int[_width, _height];
            
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                int sum = 0;
                for (int k = 0; k < _depth; k++)
                {
                    sum += _array[i, j, k];
                }
                _wallArray[i, j] = sum;
            }
        }

        return _wallArray;
    }
    
    private struct Branch
    {
        public int length;        
        public int spawnHeight;        
        public Direction direction;
    }

    public enum Direction
    {
        forward = 0,
        back,
        left,
        right
    }
    
    private (int, int) ParseDirection(Direction direction)
    {
        return direction switch
        {
            Direction.forward => (1, 0),
            Direction.back => (-1, 0),
            Direction.left => (0, 1),
            Direction.right => (0, -1),
            _ => (0,0)
        };
    }

    private int GetBranchCount() => Random.Range(2, 4);
    private int GetBranchHeight() => Random.Range(1, _height);
}