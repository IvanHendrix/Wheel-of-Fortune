using System.IO;
using UnityEngine;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        void SaveProgress(int playerBalance);
        int LoadProgress();
    }

    public class PersistentProgressService : IPersistentProgressService
    {
        private const string PlayerProgressPath = "/PlayerProgress.json";
        
        public void SaveProgress(int playerBalance)
        {
            PlayerData data = new PlayerData(playerBalance);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + PlayerProgressPath, json);
        }

        public int LoadProgress()
        {
            int playerBalance = 0;
            
            string path = Application.persistentDataPath + PlayerProgressPath;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                playerBalance = data.Balance;
            }

            return playerBalance;
        }
    }
}