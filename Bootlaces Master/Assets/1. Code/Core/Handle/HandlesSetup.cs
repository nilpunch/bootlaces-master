using UnityEngine;

namespace BootlacesMaster
{
    public class HandlesSetup : MonoBehaviour
    {
        [SerializeField] private LaceHandleHighlighter _firstHandle = null;
        [SerializeField] private LaceHandleHighlighter _secondHandle = null;

        public void Init(WinConditionChecker winConditionChecker)
        {
            _firstHandle.Init(winConditionChecker);
            _secondHandle.Init(winConditionChecker);
        }
    }
}