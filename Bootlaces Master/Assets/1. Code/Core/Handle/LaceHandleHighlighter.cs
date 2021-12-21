using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class LaceHandleHighlighter : MonoBehaviour
    {
        [SerializeField] private RopeLace _ropeLace = null;
        [SerializeField] private LaceHandle _laceHandle = null;
        [SerializeField] private MeshRenderer _meshRenderer = null;
        
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private float _colorChangeTime = 0.5f;
        
        private WinConditionChecker _winConditionChecker;

        public void Init(WinConditionChecker winConditionChecker)
        {
            _winConditionChecker = winConditionChecker;
            
            _laceHandle.StartMoving += OnStartMoving;
            _laceHandle.EndMoving += OnEndMoving;

            OnEndMoving();
        }

        private void OnDestroy()
        {
            _laceHandle.StartMoving -= OnStartMoving;
            _laceHandle.EndMoving -= OnEndMoving;
        }

        private void OnEndMoving()
        {
            if (_winConditionChecker.IsHandleOnItsPlace(_ropeLace, _laceHandle))
                ChangeColor(_ropeLace.Color);
            else
                ChangeColor(_defaultColor);
        }

        private void OnStartMoving()
        {
            ChangeColor(_defaultColor);
        }

        private void ChangeColor(Color color)
        {
            _meshRenderer.material.DOKill();
            _meshRenderer.material.DOColor(color, _colorChangeTime).SetEase(Ease.InOutQuad);
        }
    }
}