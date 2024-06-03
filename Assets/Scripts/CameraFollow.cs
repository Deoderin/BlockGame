using DG.Tweening;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;

    [SerializeField] private Vector3 _distanceFromObject;

    [SerializeField]
    private GameGUI _gameGUI;

    //[Inject] //ToDo fixThis
    public void SetTargets(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        FollowTarget();
        //LookUI();
    }

    private void FollowTarget()
    {
        if(_target != null)
        {
            Vector3 positionToGo = _target.transform.position + _distanceFromObject;
            Vector3 smoothPosition = Vector3.Slerp(transform.position, positionToGo, 0.125f);
            transform.position = smoothPosition; 
            //transform.LookAt(_target.transform.position);
        }
    }

    public void OpenUI()
    {
        _target = null;
        
        if(_gameGUI != null)
        {
            transform.DOLookAt(_gameGUI.transform.position, 1);
        }
    }
}