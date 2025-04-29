using Infrastructure.Factory;
using Infrastructure.State.Enum;
using Services;
using UI;
using UnityEngine;

namespace Infrastructure.State.States
{
    public class StartSceneState : IGameState
    {
        private const string SceneName = "MainScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public StartSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            CreateBackground();
            CreateStartUI();
        }

        public void Exit()
        {
            _sceneLoader.Load(SceneName, EnterLoadLevel);
        }
        
        private void EnterLoadLevel()
        {
            _gameStateMachine.SetState(GameStateEnum.InitLevel);
        }

        private void CreateBackground()
        {
            LocalServices.Container.Single<IGameFactory>().CreateBackground();
        }

        private void CreateStartUI()
        {
            GameObject ui = LocalServices.Container.Single<IGameFactory>().CreateStartUI();

            ui.GetComponent<StartUI>().OnStartClick += OnNextSceneClick;
        }

        private void OnNextSceneClick()
        {
            _gameStateMachine.SetState(GameStateEnum.InitLevel);
        }
    }
}