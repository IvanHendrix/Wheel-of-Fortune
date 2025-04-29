using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartUI : MonoBehaviour
    {
        public event Action OnStartClick;
        
        [SerializeField] private Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            OnStartClick?.Invoke();
        }
    }
}