using Infrastructure.Assets;
using Services;
using Services.PersistentProgress;
using Services.World;
using UI;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        void CreateSpinWheel();
        void CreateBackground();
        GameObject CreateStartUI();
    }

    public class GameFactory : IGameFactory
    {
        private IWorldStateService _worldStateService;
        private IPersistentProgressService _persistentProgressService;

        public GameFactory(IWorldStateService worldStateService, IPersistentProgressService persistentProgressService)
        {
            _worldStateService = worldStateService;
            _persistentProgressService = persistentProgressService;
        }

        public void CreateBackground()
        {
            GameObject background = InstantiatePrefab(AssetsAddress.Background, Vector3.zero);
        }

        public GameObject CreateStartUI()
        {
            GameObject startUI = InstantiatePrefab(AssetsAddress.StartUI, Vector3.zero);
            return startUI;
        }

        public void CreateHud()
        {
            GameObject hud = InstantiatePrefab(AssetsAddress.Hud, Vector3.zero);
            hud.GetComponent<MainUI>().Construct(_worldStateService, _persistentProgressService);
        }

        public void CreateSpinWheel()
        {
            GameObject spinWheelHud = InstantiatePrefab(AssetsAddress.SpinWheel, Vector3.zero);
            spinWheelHud.GetComponentInChildren<SpinWheel>().Construct(_worldStateService, _persistentProgressService);
        }

        private GameObject InstantiatePrefab(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found at path: {path}");
            }

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}