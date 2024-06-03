using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Shape : MonoBehaviour
{
    public static Action levelCompleted;
    
    [SerializeField]
    private GameObject _block;
    [SerializeField]
    private LayerMask _layerMask;    
    [SerializeField]
    private LayerMask _finishLayerMask;    
    [SerializeField]
    private LayerMask _wallPassLayerMask;

    private List<GameObject> _blockList = new();
    private BuildSpatialTree _buildSpatialTree;
    private IScoreSystem _scoreSystem;
    private float _normalSpeed;
    private float _boostSpeed;
    private float _currentSpeed;
    private bool _isCollision;
    
    private Vector3 _rotationPosition;
    
    [Inject] //ToDO fix inject
    public void Construct(BuildSpatialTree buildSpatialTree)
    {
        _buildSpatialTree = buildSpatialTree;
        
        _normalSpeed = 0.4f;
        _boostSpeed = 0.8f;
    }
    
    public void SpeedUp() => _currentSpeed = _boostSpeed;

    public void SpeedDown() => _currentSpeed = _normalSpeed;
    
    public void SetStartPosition(bool force)
    {
        string initialPoint = "Respawn";
        var spawnPosition = GameObject.FindWithTag(initialPoint).transform.position;
        transform.DOMove(spawnPosition, force ? 0 : 1);
    }
    
    private void Start()
    {
        Construct(BuildSpatialTree.instance); //todo fix enjection
        
        _currentSpeed = _normalSpeed;
        _rotationPosition = transform.rotation.eulerAngles;
        _scoreSystem = ScoreSystem.instance;
            
        InputManager.swipeInput += RotateShape;
        InputManager.pressInput += SpeedUp;
        InputManager.unPressInput += SpeedDown;
    }
    
    public void MoveForward()
    {
        if(_isCollision)
        {
            _currentSpeed = -_normalSpeed;
            DOTween.To(x => _currentSpeed = x, _currentSpeed, _normalSpeed, 3f);
            
            _isCollision = false;
        } 
        else
        {
            transform.Translate(Vector3.back * _currentSpeed, Space.World);
        }
        
        CheckSpeed();
    }

    private void CheckSpeed()
    {
        int approximately = 1; 
        if(_currentSpeed + approximately >= _boostSpeed)
        {
            _scoreSystem.SetScore(ScoreType.speedScore);
        }
    }

    public void RebuildShape() => ShapeBuilder().Forget();

    private async UniTaskVoid ShapeBuilder()
    {
        (float, float) scaleAnimationTime = (0.3f, 0.8f);        
        (float, float) moveAnimationTime = (0.1f, 1f);
        float delay = 0.1f;
        
        Vector3 startPosition = gameObject.transform.localPosition;
        
        var treeElementsPositions = _buildSpatialTree.GetVectorWithTree();

        foreach (var position in treeElementsPositions)
        {
            await UniTask.WaitForSeconds(delay);
            
            var block = Instantiate(_block, gameObject.transform);
            
            block.transform.localScale = Vector3.zero;
            block.transform.localPosition = startPosition;
            block.transform.DOLocalMove(position, Random.Range(moveAnimationTime.Item1, moveAnimationTime.Item2));
            block.transform.DOScale(Vector3.one, Random.Range(scaleAnimationTime.Item1, scaleAnimationTime.Item2));

            _blockList.Add(block);
        }
    }
    
    private void RotateShape(InputManager.DraggedDirection direction)
    {
        int rotationDegree = 90;
        float animationDuration = 0.5f;
        
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

    private void OnTriggerEnter(Collider other)
    {
        FinishCheck(other);
        WallThePass(other);
        PunchWallBlock(other);
    }

    private void FinishCheck(Collider other)
    {
        if(((1 << other.gameObject.layer) & _finishLayerMask.value) > 0)
        {
            levelCompleted?.Invoke();
    
            transform.DOLocalMoveZ(transform.position.z + 2, 0.2f);
        }
    }

    private void WallThePass(Collider other)
    {
        if(((1 << other.gameObject.layer) & _wallPassLayerMask.value) > 0)
        {
            int multiplierNum = _scoreSystem.GetMultiplier() + 1;
            
            _scoreSystem.SetScore(ScoreType.gateScore);
            _scoreSystem.SetMultiplier(multiplierNum);

            Destroy(other);
        }
    }
    
    private void PunchWallBlock(Collider other)
    {
        float animationDuration = 5;
        float punchForce = 300;
        
        if(((1 << other.gameObject.layer) & _layerMask.value) > 0)
        {
            other.attachedRigidbody.isKinematic = false;
            other.attachedRigidbody.AddForce(Vector3.back * punchForce);

            other.transform
                 .DOScale(Vector3.zero, animationDuration)
                 .SetEase(Ease.InExpo)
                 .OnComplete(() => other.gameObject.SetActive(false));

            SwitchBlockLayer(other).Forget();
            
            _isCollision = true;
            _scoreSystem.SetMultiplier(1);
        }
    }

    private async UniTaskVoid SwitchBlockLayer(Collider other)
    {
        float delay = 0.5f;

        await UniTask.WaitForSeconds(delay);

        if(other != null)
        {
            other.gameObject.layer = 0;
        }
    }

    public void CollectBlock()
    {
        foreach (var block in _blockList)
        {
            block.transform.DOLocalMove(Vector3.up * Random.Range(0.1f, 12f), Random.Range(0.5f, 1f));
            block.transform.DOScale(Vector3.zero, Random.Range(1f, 2.1f)).OnComplete(() => Destroy(block));
        }
        
        _blockList.Clear();
    }
}