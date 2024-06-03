using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField]
    private SceneContext _serviceLocator;    
    private Game _game;
    
    private IInstantiator _instantiator;

    [Inject]
    public void Initialize(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    private void Awake()
    {
        _game = new Game(_serviceLocator);
        _game.stateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}