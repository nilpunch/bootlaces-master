using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private Slider _progressBar = null;

    private void Awake()
    {
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        LevelLoader.Instance.LevelLoading += OnLevelLoading;
        LevelLoader.Instance.LevelLoaded += OnLevelLoaded;
        LevelLoader.Instance.LoadingProgressUpdated += OnLoadingProgressUpdated;
    }

    private void OnDisable()
    {
        LevelLoader.Instance.LevelLoading -= OnLevelLoading;
        LevelLoader.Instance.LevelLoaded -= OnLevelLoaded;
        LevelLoader.Instance.LoadingProgressUpdated -= OnLoadingProgressUpdated;
    }

    private void OnLoadingProgressUpdated(float progress)
    {
        _progressBar.value = progress;
    }

    private void OnLevelLoading()
    {
        _canvas.enabled = true;
    }

    private void OnLevelLoaded()
    {
        _canvas.enabled = false;
    }
}