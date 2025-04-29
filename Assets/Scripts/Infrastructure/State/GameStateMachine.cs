using System.Collections.Generic;
using Infrastructure.State.Enum;
using Infrastructure.State.States;
using Services;

namespace Infrastructure.State
{
    public interface IGameStateMachine : IService
    {
        void SetState(GameStateEnum newState);
    }

    public class GameStateMachine : IGameStateMachine
    {
        private IGameState _currentGameState;

        private readonly Dictionary<GameStateEnum, IGameState> _states;

        public GameStateMachine(SceneLoader sceneLoader, LocalServices services)
        {
            _states = new Dictionary<GameStateEnum, IGameState>();

            _states.Add(GameStateEnum.Init, new GameInitState(this, sceneLoader, services));
            _states.Add(GameStateEnum.Start,
                new StartSceneState(this, sceneLoader));
            _states.Add(GameStateEnum.InitLevel,
                new InitGameLevelState(this));
            _states.Add(GameStateEnum.Gameplay, new GameplayState());
        }

        public void SetState(GameStateEnum newState)
        {
            IGameState state = ChangeState(newState);
            state.Enter();
        }

        private IGameState ChangeState(GameStateEnum newState)
        {
            _currentGameState?.Exit();

            IGameState state = _states[newState];
            _currentGameState = state;

            return state;
        }
    }
}