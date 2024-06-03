using System.Collections.Generic;

public class UpdatableServices : IUpdateableServices
{
    public static UpdatableServices instance;
    private List<IFixedUpdateable> _updatableEntity = new();

    public UpdatableServices()
    {
        instance = this;
    }
    
    public void UnRegisterUpdatableSystem(IFixedUpdateable updateableSystems)
    {
        if(_updatableEntity.Contains(updateableSystems))
        {
            _updatableEntity.Remove(updateableSystems);
        }
    }

    public void CleanUp() => _updatableEntity.Clear();

    public void FixedUpdate()
    {
        foreach (IFixedUpdateable entity in _updatableEntity)
        {
            entity.FixedUpdate();
        }
    }

    void IUpdateableServices.RegisterUpdatableSystem(IFixedUpdateable updateableSystems)
    {
        if(_updatableEntity.Contains(updateableSystems) is false)
        {
            _updatableEntity.Add(updateableSystems);
        }
    }
}