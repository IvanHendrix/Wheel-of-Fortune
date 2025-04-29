using UnityEngine;

namespace UI
{
    public class BackgroundManager : MonoBehaviour
    {
        private static BackgroundManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}