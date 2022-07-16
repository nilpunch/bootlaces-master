using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private RectTransform _leftShade = null;
    [SerializeField] private RectTransform _rightShade = null;
    [SerializeField] private TextMeshProUGUI _logo = null;
    [SerializeField] private float _animationTime = 1f;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _firstLoadDelay = 1f;
    
    private Rect _originalLeftRect;
    private Rect _originalRightRect;
    private bool _firstLoad = true;
    
    private void Awake()
    {
        _originalLeftRect = _leftShade.rect;
        _originalRightRect = _rightShade.rect;

        // _leftShade.anchoredPosition = _originalLeftRect.center + Vector2.left * _originalLeftRect.width;
        // _rightShade.anchoredPosition = _originalRightRect.center + Vector2.right * _originalRightRect.width;
        
        _logo.color = new Color(_logo.color.r, _logo.color.g, _logo.color.b, 1f);
    }

    private void OnEnable()
    {
        LevelLoader.Instance.LevelLoading += OnLevelLoading;
        LevelLoader.Instance.LevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        LevelLoader.Instance.LevelLoading -= OnLevelLoading;
        LevelLoader.Instance.LevelLoaded -= OnLevelLoaded;
    }

    private void Show(Action transitionCallback)
    {
        _leftShade.DOKill();
        _rightShade.DOKill();
        _logo.DOKill();
        _leftShade.DOAnchorPos(_originalLeftRect.center, _animationTime).SetEase(Ease.InOutCubic);
        _rightShade.DOAnchorPos(_originalRightRect.center, _animationTime).SetEase(Ease.InOutCubic);
        _logo.DOFade(1f, _animationTime).SetEase(Ease.OutQuad)
            .OnComplete(() => transitionCallback?.Invoke());
    }

    private void Hide()
    {
        _leftShade.DOKill();
        _rightShade.DOKill();
        _logo.DOKill();

        float delay = _delay;

        if (_firstLoad)
            delay = _firstLoadDelay;

        _firstLoad = false;
        
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(_leftShade
                .DOAnchorPos(_originalLeftRect.center + Vector2.left * _originalLeftRect.width, _animationTime)
                .SetEase(Ease.InOutCubic))
            .Join(_rightShade
                .DOAnchorPos(_originalRightRect.center + Vector2.right * _originalRightRect.width, _animationTime)
                .SetEase(Ease.InOutCubic))
            .Join(_logo.DOFade(0f, _animationTime)
                .SetEase(Ease.InQuad));
    }

    private void OnLevelLoading(Action transitionCallback)
    {
        Show(transitionCallback);
    }

    private void OnLevelLoaded()
    {
        Hide();
    }
}