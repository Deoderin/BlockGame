using DG.Tweening;
using UnityEngine;
using Zenject;

public class Shape : MonoBehaviour
{
    [SerializeField]
    private GameObject _block;
    
    private BuildSpatialTree _buildSpatialTree;
    
    private float _normalSpeed;
    private float _boostSpeed;
    private float _currentSpeed;

    private Vector3 _rotationPosition;
    
    [Inject]
    public void Construct(BuildSpatialTree buildSpatialTree)
    {
        _buildSpatialTree = buildSpatialTree;
        
        _normalSpeed = 0.2f;
        _boostSpeed = 0.3f;
    }
    
    private void Start()
    {
        Construct(BuildSpatialTree.instance); //todo fix enjection
            
        RebuildShape();

        _currentSpeed = _normalSpeed;
        _rotationPosition = transform.rotation.eulerAngles;
        
        InputManager.swipeInput += RotateShape;
        InputManager.pressInput += SpeedUp;
        InputManager.unPressInput += SpeedDown;
    }

    private void RotateShape(InputManager.DraggedDirection direction)
    {
        int rotationDegree = 90;
        float animationDuration = 1;
        
        switch(direction)
        {
            case InputManager.DraggedDirection.Right:
                _rotationPosition = new Vector3(_rotationPosition.x, _rotationPosition.y + rotationDegree, _rotationPosition.z);
                transform.DORotate(_rotationPosition, animationDuration);
                break;
            case InputManager.DraggedDirection.Left:
                _rotationPosition = new Vector3(_rotationPosition.x, _rotationPosition.y - rotationDegree, _rotationPosition.z);
                transform.DORotate(_rotationPosition, animationDuration);
                break;
        }
    }
    
    public void MoveForward()
    {
        transform.Translate(Vector3.back * _currentSpeed, Space.World);
    }

    public void RebuildShape()
    {
        _buildSpatialTree.GenerateTreeBlueprint();
        var treeElementsPositions = _buildSpatialTree.GetVectorWithTree();

        foreach (var position in treeElementsPositions)
        {
            var block = Instantiate(_block, gameObject.transform);
            block.transform.localPosition = position;
        }
    }

    public void SpeedUp() => _currentSpeed = _boostSpeed;
    public void SpeedDown() => _currentSpeed = _normalSpeed;

    public void SetStartPosition()
    {
        transform.position = GameObject.FindWithTag("Respawn").transform.position;
    }
}