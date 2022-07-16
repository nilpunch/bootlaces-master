using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster.UI
{
    public class WinConditionViewer : MonoBehaviour
    {
        [Space, SerializeField] private InteractionsSetup _interactionsSetup = null;
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        
        [SerializeField] private RectTransform _movingPivot = null;
        [SerializeField] private RectTransform _hidingPivot = null;
        [SerializeField] private bool _hideOnAwake = true;

        [Space, SerializeField] private float _showTime = 1;
        [SerializeField] private float _hideTime = 0.5f;

        private Vector2 _originalPosition;
        private Vector2 _hidingPosition;
        private bool _hided = false;

        private void Awake()
        {
            if (_interactionsSetup == null || _winConditionChecker == null)
            {
                Destroy(gameObject);
                return;
            }
            
            _originalPosition = _movingPivot.anchoredPosition;
            _hidingPosition = _hidingPivot.anchoredPosition;

            if (_hideOnAwake)
            {
                _hided = true;
                _movingPivot.anchoredPosition = _hidingPosition;
            }

            _interactionsSetup.LevelStarted += OnLevelStarted;
            _winConditionChecker.Winned += OnWinned;
        }

        private void OnWinned()
        {
            Hide();
        }

        private void OnDestroy()
        {
            if (_interactionsSetup != null)
                _interactionsSetup.LevelStarted -= OnLevelStarted;
            if (_winConditionChecker != null)
                _winConditionChecker.Winned -= OnWinned;
        }

        private void OnLevelStarted()
        {
            Show();
        }

        public void Show()
        {
            if (_hided == false)
                return;

            _hided = false;

            _movingPivot.DOKill();
            _movingPivot.DOAnchorPos(_originalPosition, _showTime)
                .SetEase(Ease.InOutCubic);
        }
        
        public void Hide()
        {
            if (_hided)
                return;
            
            _hided = true;

            _movingPivot.DOKill();
            _movingPivot.DOAnchorPos(_hidingPosition, _hideTime)
                .SetEase(Ease.InOutCubic);
        }
    }
}