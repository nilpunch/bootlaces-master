using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BootlacesMaster.UI
{
    public class EasingButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _movingPivot = null;
        [SerializeField] private RectTransform _hidingPivot = null;
        [SerializeField] private Button _button = null;
        [SerializeField] private bool _hideOnAwake = true;

        [Space, SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        private Vector2 _originalPosition;
        private Vector2 _hidingPosition;
        private bool _hided = true;

        public event Action Clicked;
        
        private void Awake()
        {
            _originalPosition = _movingPivot.anchoredPosition;
            _hidingPosition = _hidingPivot.anchoredPosition;

            if (_hideOnAwake)
            {
                _hided = true;
                _movingPivot.anchoredPosition = _hidingPosition;
            }

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke();
        }

        public void Show()
        {
            if (_hided == false)
                return;

            _hided = false;

            _button.interactable = true;

            _movingPivot.DOKill();
            _movingPivot.DOAnchorPos(_originalPosition, _showTime)
                .SetEase(Ease.OutCubic);
        }
        
        public void Hide()
        {
            if (_hided)
                return;
            
            _hided = true;

            _button.interactable = false;
            
            _movingPivot.DOKill();
            _movingPivot.DOAnchorPos(_hidingPosition, _hideTime)
                .SetEase(Ease.InQuad);
        }
    }
}