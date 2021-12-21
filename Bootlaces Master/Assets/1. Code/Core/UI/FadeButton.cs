using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BootlacesMaster.UI
{
    public class FadeButton : MonoBehaviour
    {
        [SerializeField] private Button _button = null;
        [SerializeField] private Image _image = null;
        [SerializeField] private TextMeshProUGUI _text = null;
        [SerializeField] private bool _hideOnAwake = true;

        [Space, SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        private bool _hided = false;

        public event Action Clicked;
        
        private void Awake()
        {
            if (_hideOnAwake)
            {
                _hided = true;
                _button.interactable = false;
                _image.raycastTarget = false;
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0f);
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
            _image.raycastTarget = true;

            _text.DOKill();
            _text.DOFade(1f, _showTime).SetEase(Ease.OutQuad);
        }
        
        public void Hide()
        {
            if (_hided)
                return;
            
            _hided = true;

            _button.interactable = false;
            _image.raycastTarget = false;

            _text.DOKill();
            _text.DOFade(0f, _hideTime)
                .SetEase(Ease.InQuad);
        }
    }
}