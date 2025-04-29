using Infrastructure.Factory;
using Infrastructure.State.Enum;
using Services;
using Services.World;

namespace Infrastructure.State.States
{
    public class InitGameLevelState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public InitGameLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InitLevelData();
            CreateLevel();
            EnterLoadLevel();
        }

        public void Exit()
        {
        }
        
        private void InitLevelData()
        {
            LocalServices.Container.Single<IWorldStateService>().Load();
        }

        private void CreateLevel()
        {
            LocalServices.Container.Single<IGameFactory>().CreateSpinWheel();
            LocalServices.Container.Single<IGameFactory>().CreateHud();
        }

        private void EnterLoadLevel()
        {
            LocalServices.Container.Single<IGameFactory>().CreateBackground();
            _gameStateMachine.SetState(GameStateEnum.Gameplay);
        }
    }
}