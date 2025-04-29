using Extensions;
using Services.PersistentProgress;
using Services.World;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _balanceText;

        private IWorldStateService _worldStateService;
        private IPersistentProgressService _persistentProgressService;
        
        public void Construct(IWorldStateService worldStateService, IPersistentProgressService persistentProgressService)
        {
            _worldStateService = worldStateService;
            _persistentProgressService = persistentProgressService;
            
            _balanceText.text = GenerateNumberExtension.FormatNumber(persistentProgressService.LoadProgress());
        }

        private void Start()
        {
            _worldStateService.OnBalanceChanged += OnScoreBalanceChanged;
        }

        private void OnScoreBalanceChanged(int balance)
        {
            _balanceText.text = GenerateNumberExtension.FormatNumber(balance);
            _persistentProgressService.SaveProgress(balance);
        }
    }
}