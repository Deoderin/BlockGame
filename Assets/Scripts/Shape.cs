using UnityEngine;

public class Shape : MonoBehaviour
{
    private float _normalSpeed;
    private float _boostSpeed;
    private float _currentSpeed;
    
    public void SetParam(float speed, float boostSpeed)
    {
        _normalSpeed = speed;
        _boostSpeed = boostSpeed;

        _currentSpeed = _normalSpeed;
    }
    
    public void MoveForward()
    {
        transform.Translate(Vector3.forward * _currentSpeed, Space.World);
    }

    public void RebuildShape()
    {
        
    }

    public void RotateShape()
    {
        
    }

    public void SpeedUp() => _currentSpeed = _boostSpeed;
    public void SpeedDown() => _currentSpeed = _normalSpeed;
}