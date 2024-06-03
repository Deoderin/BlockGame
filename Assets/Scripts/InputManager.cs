using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager :  MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static UnityAction pressInput;
    public static UnityAction unPressInput;    
    public static UnityAction<DraggedDirection> swipeInput;
    
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

            if(_touch.phase == TouchPhase.Stationary)
            {
                pressInput?.Invoke();
            }
            else if(_touch.phase == TouchPhase.Stationary)
            {
                unPressInput?.Invoke();
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