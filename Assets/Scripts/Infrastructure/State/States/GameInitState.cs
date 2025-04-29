using Infrastructure.Factory;
using Infrastructure.State.Enum;
using Services;
using Services.PersistentProgress;
using Services.World;

namespace Infrastructure.State.States
{
    public class GameInitState : IGameState
    {
        private const string SceneName = "StartScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LocalServices _services;

        public GameInitState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LocalServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName, EnterLoadLevel);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel()
        {
            _gameStateMachine.SetState(GameStateEnum.Start);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterSingle<IWorldStateService>(new WorldStateService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IWorldStateService>(),_services.Single<IPersistentProgressService>()));
        }
    }
}