using Infrastructure;
using Infrastructure.State;
using Infrastructure.State.Enum;
using Services;

public class Game
{
    private GameStateMachine _stateMachine;
        
    public Game(ICoroutineRunner coroutineRunner)
    {
        _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),LocalServices.Container);
        
        _stateMachine.SetState(GameStateEnum.Init);
    }
}