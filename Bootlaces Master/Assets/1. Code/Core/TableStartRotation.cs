using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class TableStartRotation : MonoBehaviour
    {
        [SerializeField] private RotationTable _rotationTable = null;
        [SerializeField] private InteractionsSetup _interactionsSetup = null;
        [SerializeField] private Vector3 _rotation = Vector3.zero;
        [SerializeField] private float _rotationTime = 1.5f;
        
        private void Awake()
        {
            _interactionsSetup.LevelStarted += OnLevelStarted;
        }
        
        private void OnDestroy()
        {
            _interactionsSetup.LevelStarted -= OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            _rotationTable.Pivot
                .DORotateQuaternion(_rotationTable.Pivot.rotation * Quaternion.Euler(_rotation), _rotationTime)
                .SetEase(Ease.InOutQuad);
        }
    }
}