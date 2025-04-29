namespace Infrastructure.State.States
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}