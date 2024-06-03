using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private Transform _target;

    private float _baseFOV = 60;
    private float _speedFOV = 80;
    private Camera _camera;
    
    [SerializeField] 
    private Vector3 _distanceFromObject;
    [SerializeField]
    private GameGUI _gameGUI;

    //[Inject] //ToDo fixThis
    public void SetTargets(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        
        InputManager.pressInput += FOVSpeedUp;
        InputManager.unPressInput += FOVSpeedDown;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void SetSpeedFOV(bool speedFOV) => _camera.DOFieldOfView(speedFOV ? _speedFOV : _baseFOV, 0.3f);

    private void FollowTarget()
    {
        if(_target != null)
        {
            transform.position =  _target.position + _distanceFromObject;
        }
    }

    private void FOVSpeedUp() => SetSpeedFOV(true);
    private void FOVSpeedDown() => SetSpeedFOV(false);

    private void OnDestroy()
    {
        InputManager.pressInput -= FOVSpeedUp;
        InputManager.unPressInput -= FOVSpeedDown;
    }
}