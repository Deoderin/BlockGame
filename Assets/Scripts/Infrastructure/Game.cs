public class Game
{
    public readonly GameStateMachine stateMachine;

    public Game()
    {
        stateMachine = new GameStateMachine(new SceneLoader());
    }
}