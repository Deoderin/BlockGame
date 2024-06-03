using Zenject;

public class Game
{
    public readonly GameStateMachine stateMachine;

    public Game(SceneContext serviceLocator)
    {
        stateMachine = new GameStateMachine(new SceneLoader(), serviceLocator);
    }
}