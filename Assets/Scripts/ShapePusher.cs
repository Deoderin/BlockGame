public class ShapePusher : IFixedUpdateable
{
    private Shape _currentShape;
    
    public void FixedUpdate()
    {
        _currentShape.MoveForward();
    }
}

public interface IFixedUpdateable
{
    public void FixedUpdate();
}