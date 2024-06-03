using System;
using System.Collections.Generic;
using Zenject;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _state;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, SceneContext serviceLocator)
    {
        _state = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
            [typeof(CreateWorldState)] = new CreateWorldState(this),
            [typeof(PendingUIState)] = new PendingUIState(this),
            [typeof(GameLoopState)] = new GameLoopState(this),
            [typeof(FinishLevelState)] = new FinishLevelState(this),
            [typeof(LoadNewLevelState)] = new LoadNewLevelState(this)
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _state[typeof(TState)] as TState;
    }
}