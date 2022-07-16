using UnityEngine;

namespace BootlacesMaster.UI
{
    public class DebugLevelSwitch : MonoBehaviour
    {
        public void NextLevel()
        {
            LevelLoader.Instance.StartNextLevel();
        }
        
        public void PrevLevel()
        {
            LevelLoader.Instance.StartPrevLevel();
        }
        
        public void RestartLevel()
        {
            LevelLoader.Instance.RestartLevel();
        }
    }
}