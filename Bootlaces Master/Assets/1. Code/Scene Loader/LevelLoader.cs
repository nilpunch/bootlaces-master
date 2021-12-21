using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }
    
    private const string LevelIndexPref = "level_index";
    
    [SerializeField] private LevelsContainer _levelsContainer = null;
    [SerializeField] private bool _loadSavedLevel = true;
    [SerializeField] private bool _useLoadingCallbacks = true;
    
    private string _levelLoaderAttachedSceneName;
    private Coroutine _asyncCoroutine;
    private SceneReference _lastLoadedLevel;
    private int _currentLevelIndex;
    private List<AsyncOperation> _asyncOperations = new List<AsyncOperation>();
    
    public event Action<Action> LevelLoading;
    public event Action LevelLoaded;
    public event Action<float> LoadingProgressUpdated;

    public int CurrentLevelIndex => _currentLevelIndex;

    private void Awake()
    {
        _asyncOperations = new List<AsyncOperation>();
        _levelLoaderAttachedSceneName = gameObject.scene.name;

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        transform.parent = null;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (var scene in _levelsContainer.ScenesForPreload)
            _asyncOperations.Add(SceneManager.LoadSceneAsync(scene , LoadSceneMode.Additive));

        _asyncCoroutine = StartCoroutine(ProcessAsyncOperations(() =>
        {
            _asyncOperations.Clear();
            FirstLevelLoadings();
        }));
    }

    public void RestartLevel()
    {
        Load(_lastLoadedLevel);
    }

    public void StartNextLevel()
    {
        int currentLevelIndex = CurrentLevelIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % _levelsContainer.Levels.Count;
        Load(_levelsContainer.GetLevel(nextLevelIndex));
    }

    public void StartLevel(int index)
    {
        Load(_levelsContainer.GetLevel(index));
    }

    public void LoadScene(SceneReference scene)
    {
        Load(scene);
    }

    private void FirstLevelLoadings()
    {
        SceneReference currentLoadedLevel = _levelsContainer.Levels.FirstOrDefault(lvl =>
            string.CompareOrdinal(_levelLoaderAttachedSceneName, lvl.GetSceneName()) == 0);
        
        if (currentLoadedLevel != null)
        {
            _lastLoadedLevel = currentLoadedLevel;
            _currentLevelIndex = _levelsContainer.GetLevelIndex(currentLoadedLevel);
            LevelLoaded?.Invoke();
        }
        else
        {
            int savedLevel = 0;
            
            if (_loadSavedLevel)
                savedLevel = PlayerPrefs.GetInt(LevelIndexPref, 0);
            
            Load(_levelsContainer.GetLevel(savedLevel), true);
        }
    }

    private void Load(SceneReference newLevel, bool ignoreLoadingCallback = false)
    {
        if (_useLoadingCallbacks && ignoreLoadingCallback == false)
        {
            LevelLoading?.Invoke(() => LoadHandler(newLevel));
        }
        else
        {
            LevelLoading?.Invoke(delegate { });
            LoadHandler(newLevel);
        }
    }

    private void LoadHandler(SceneReference newLevel)
    {
        if (_lastLoadedLevel != null)
            _asyncOperations.Add(SceneManager.UnloadSceneAsync(_lastLoadedLevel));
        
        _asyncOperations.Add(SceneManager.LoadSceneAsync(newLevel, LoadSceneMode.Additive));

        if (_asyncCoroutine != null)
            StopCoroutine(_asyncCoroutine);
        
        if (_levelsContainer.Storing(newLevel))
            _currentLevelIndex = _levelsContainer.GetLevelIndex(newLevel);

        _asyncCoroutine = StartCoroutine(ProcessAsyncOperations(() =>
        {
            _asyncOperations.Clear();
            _lastLoadedLevel = newLevel;
            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(newLevel));
            LevelLoaded?.Invoke();
            
            PlayerPrefs.SetInt(LevelIndexPref, _currentLevelIndex);
        }));
    }

    private IEnumerator ProcessAsyncOperations(Action onComplete = null)
    {
        float totalProgress = 0;
        
        while(_asyncOperations.Any(asyncOp => asyncOp.isDone == false))
        {
            foreach(AsyncOperation operation in _asyncOperations)
            {
                totalProgress += operation.progress / _asyncOperations.Count;
            }
                
            LoadingProgressUpdated?.Invoke(totalProgress);
            yield return null;
        }

        _asyncCoroutine = null;
        onComplete?.Invoke();
    }
}