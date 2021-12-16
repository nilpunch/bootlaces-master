using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster.UI
{
    public class NextLevel : MonoBehaviour
    {
        [SerializeField] private EasingButton _easingButton = null;
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        
        [Space, SerializeField] private float _winDelay = 1f;
        
        private void Awake()
        {
            _easingButton.Clicked += OnNextLevelButtonClicked;
            _winConditionChecker.Winned += OnWinned;
        }

        private void OnDestroy()
        {
            _easingButton.Clicked -= OnNextLevelButtonClicked;
            _winConditionChecker.Winned -= OnWinned;
        }

        private void OnWinned()
        {
            DOTween.Sequence()
                .AppendInterval(_winDelay)
                .AppendCallback(() => _easingButton.Show());
        }

        private void OnNextLevelButtonClicked()
        {
            LevelLoader.Instance.StartNextLevel();
        }
    }
}