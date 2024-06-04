using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager :  MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static UnityAction pressInput;
    public static UnityAction unPressInput;    
    public static UnityAction<DraggedDirection> swipeInput;

    private float _timeTouchEnded;

    private float _displayTime = 0.25f;
    public enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    
    private Touch _touch;
    
    private void Update()
    {
        if(Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);
            
            switch(_touch.phase)
            {
                case TouchPhase.Stationary:
                    if(Time.time - _timeTouchEnded > _displayTime)
                    {
                        pressInput?.Invoke();
                    }
                    break;
                case TouchPhase.Ended:
                    _timeTouchEnded = Time.time;
                    unPressInput?.Invoke();
                    break;
                case TouchPhase.Began:
                    _timeTouchEnded = Time.time;
                    break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int minimumSwipePosition = Screen.width / 4;
        if(Vector2.Distance(eventData.pressPosition, eventData.position) < minimumSwipePosition)
        {
            return;
        }
        
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;

        swipeInput?.Invoke(GetDragDirection(dragVectorDirection));
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }

        return draggedDir;
    }
}