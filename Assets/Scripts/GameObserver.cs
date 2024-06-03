using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameObserver : MonoBehaviour
{
    private List<IFixedUpdateable> _updatableEntity = new();
    private ShapePusher _shapePusher;
    
    private void Start()
    {
        InitialGameSystem();
        
        RegisterUpdatableSystem(_shapePusher);
    }

    private void InitialGameSystem()
    {
        _shapePusher = new ShapePusher();
    }

    private void RegisterUpdatableSystem(IFixedUpdateable updateableSystems)
    {
        if(_updatableEntity.Contains(updateableSystems) is false)
        {
            _updatableEntity.Add(updateableSystems);
        }
    }

    private void FixedUpdate()
    {
        foreach (IFixedUpdateable entity in _updatableEntity)
        {
            entity.FixedUpdate();
        }
    }
}