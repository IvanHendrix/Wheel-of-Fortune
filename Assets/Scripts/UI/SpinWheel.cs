using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Services.PersistentProgress;
using Services.World;

namespace UI
{
    public class SpinWheel : MonoBehaviour
    {
        [SerializeField] public RectTransform _wheel;
        [SerializeField] public Button _spinButton;
        [SerializeField] public GameObject _segmentPrefab;

        private readonly float _wheelSize = 400f;

        private int _numberOfSegments;

        private List<int> _segmentValues = new List<int>();
        private List<Segment> _segments = new List<Segment>();

        private IWorldStateService _worldStateService;
        private IPersistentProgressService _persistentProgressService;

        public void Construct(IWorldStateService worldStateService,
            IPersistentProgressService persistentProgressService)
        {
            _worldStateService = worldStateService;
            _persistentProgressService = persistentProgressService;

            _numberOfSegments = _worldStateService.GetLevelData().NumberOfSegments;
        }

        private void Start()
        {
            _spinButton.onClick.AddListener(OnStartSpinButtonClick);

            _wheel.sizeDelta = new Vector2(_wheelSize, _wheelSize); // Adjust wheel size

            GenerateSegmentValues();
            CreateWheelSegments();
            UpdateBalanceScore(_persistentProgressService.LoadProgress());
        }

        private void GenerateSegmentValues()
        {
            _segmentValues = GenerateNumberExtension.GenerateUniqueValues(_numberOfSegments, 1000, 100000, 1000)
                .ToList();
        }

        private void CreateWheelSegments()
        {
            float angleStep = 360f / _numberOfSegments;

            for (int i = 0; i < _numberOfSegments; i++)
            {
                GameObject segmentObj = Instantiate(_segmentPrefab, _wheel);
                Segment segment = new Segment(segmentObj, _segmentValues[i], angleStep, _wheelSize, i,
                    _numberOfSegments);
                _segments.Add(segment);
            }
        }

        private void OnStartSpinButtonClick()
        {
            _spinButton.interactable = false;

            float angleStep = 360f / _numberOfSegments;
            int targetIndex = Random.Range(0, _numberOfSegments);
            float targetRotation = 360f - (targetIndex * angleStep);

            targetRotation += Random.Range(1080f, 1800f);

            _wheel.DORotate(new Vector3(0, 0, targetRotation), 2.5f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    DetermineWinningSegment();
                    _spinButton.interactable = true;
                });
        }

        private void DetermineWinningSegment()
        {
            int winningIndex = Mathf.RoundToInt((360f - _wheel.eulerAngles.z) / (360f / _numberOfSegments)) %
                               _numberOfSegments;

            if (winningIndex < 0)
            {
                winningIndex += _numberOfSegments;
            }

            Debug.Log($"Winning segment: {winningIndex}, Value: {_segments[winningIndex].GetValue()}");
            
            UpdateBalanceScore(_segments[winningIndex].GetValue());
        }

        private void UpdateBalanceScore(int playerBalance)
        {
            _worldStateService.BalanceUpdated(playerBalance);
        }
    }
}