using UnityEngine;

namespace BootlacesMaster.UI
{
    public class DebugNextLevel : MonoBehaviour
    {
        [SerializeField] private EasingButton _easingButton = null;
        
        private void Awake()
        {
            _easingButton.Clicked += OnNextLevelButtonClicked;
        }

        private void OnDestroy()
        {
            _easingButton.Clicked -= OnNextLevelButtonClicked;
        }

        private void OnNextLevelButtonClicked()
        {
            LevelLoader.Instance.StartNextLevel();
        }
    }
}