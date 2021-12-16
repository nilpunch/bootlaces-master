using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels Pack", fileName = "Levels")]
public class LevelsContainer : ScriptableObject
{
    [SerializeField] private List<SceneReference> _scenesForPreload = null;
    [SerializeField] private List<SceneReference> _levels = null;

    public IReadOnlyList<SceneReference> Levels => _levels;

    public IReadOnlyList<SceneReference> ScenesForPreload => _scenesForPreload;

    public string GetSceneName(int levelId)
    {
        return _levels[levelId].GetSceneName();
    }
    
    public SceneReference GetLevel(int levelId)
    {
        return _levels[levelId];
    }

    public int GetLevelIndex(SceneReference level)
    {
        return _levels.IndexOf(level);
    }

    public bool Storing(SceneReference level)
    {
        return _levels.Contains(level);
    }
}