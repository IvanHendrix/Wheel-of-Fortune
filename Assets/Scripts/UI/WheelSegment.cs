using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Segment
    {
        private int _valueData;

        private GameObject _segmentContainer;
        private Image _segmentImage;
        private TextMeshProUGUI _segmentText;

        public Segment(GameObject segmentContainer, int valueData, float angleStep, float wheelSize, int index,
            int totalSegments)
        {
            _segmentContainer = segmentContainer;
            _valueData = valueData;
            _segmentImage = _segmentContainer.GetComponentInChildren<Image>();
            _segmentText = _segmentContainer.GetComponentInChildren<TextMeshProUGUI>();
            SetupSegment(angleStep, wheelSize, index, totalSegments);

            _segmentText.text = _valueData.ToString();
        }

        public int GetValue()
        {
            return _valueData;
        }

        private void SetupSegment(float angleStep, float wheelSize, int index, int totalSegments)
        {
            RectTransform segmentTransform = _segmentContainer.GetComponent<RectTransform>();
            segmentTransform.sizeDelta = new Vector2(wheelSize / 2, wheelSize / 2);

            if (_segmentImage != null)
            {
                _segmentImage.fillAmount = 1f / totalSegments;
                _segmentImage.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            }

            if (_segmentText != null)
            {
                Vector3 center = _segmentImage.rectTransform.localPosition;

                float angle = 270f - _segmentImage.fillAmount * 180f;
                float angleRad = angle * Mathf.Deg2Rad;

                float adjustedRadius = wheelSize;

                float x = Mathf.Cos(angleRad) * adjustedRadius;
                float y = Mathf.Sin(angleRad) * adjustedRadius;

                _segmentText.rectTransform.localPosition = center + new Vector3(x, y, 0);

                Vector3 direction = center - _segmentText.rectTransform.localPosition;
                float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _segmentText.rectTransform.localRotation = Quaternion.Euler(0, 0, rotationZ + 90);
            }

            float rotation = -(1 - _segmentImage.fillAmount) * 180f;
            segmentTransform.rotation = Quaternion.Euler(0, 0, rotation + angleStep * index);
        }
    }
}