public class ShapePusher : IFixedUpdateable
{
    private Shape _currentShape;

    //ToDo Di
    public void SetShape()
    {
        _currentShape = GameFactory.instance.GetShape();
    }
    
    public void FixedUpdate()
    {
        _currentShape.MoveForward();
    }
}

public interface IFixedUpdateable
{
    public void FixedUpdate();
}