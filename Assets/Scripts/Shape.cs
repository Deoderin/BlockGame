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

    public void SpeedUp() => _currentSpeed = _boostSpeed;
    public void SpeedDown() => _currentSpeed = _normalSpeed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward, Space.World);
    }
}

public class ProgressManager
{
    
}