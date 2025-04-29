using System;

namespace Services.PersistentProgress
{
    [Serializable]
    public class PlayerData
    {
        public int Balance;
        
        public PlayerData(int balance)
        {
            Balance = balance;
        }
    }
}