using System;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Services.World
{
    public interface IWorldStateService : IService
    {
        event Action<int> OnBalanceChanged;
        void Load();
        LevelStaticData GetLevelData();
        void BalanceUpdated(int points);
    }

    public class WorldStateService : IWorldStateService
    {
        private const string StaticDataPath = "Static Data/LevelData";

        public event Action<int> OnBalanceChanged;

        private int _balance;
        
        private LevelStaticData _levelStaticData;

        public void Load()
        {
            _levelStaticData = Resources
                .Load<LevelStaticData>(StaticDataPath);

            _balance = LocalServices.Container.Single<IPersistentProgressService>().LoadProgress();
        }

        public LevelStaticData GetLevelData()
        {
            return _levelStaticData;
        }
        
        public void BalanceUpdated(int points)
        {
            _balance += points;
            OnBalanceChanged?.Invoke(_balance);
        }
    }
}