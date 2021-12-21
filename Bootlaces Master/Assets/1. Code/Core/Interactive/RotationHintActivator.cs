using System;
using BootlacesMaster.UI;
using UnityEngine;

namespace BootlacesMaster
{
    public class RotationHintActivator : PointerInputHandler
    {
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        [SerializeField] private RotationHint _rotationHint = null;
        [SerializeField] private float _inactivityDelay = 3f;
        
        private float _elapsedTime;
        private bool _pressed;
        private bool _winned;

        private void Awake()
        {
            _winConditionChecker.Winned += OnWinned;
        }
        
        private void OnDestroy()
        {
            _winConditionChecker.Winned -= OnWinned;
        }


        private void OnWinned()
        {
            _winned = true;
            
            if (_rotationHint.Showed)
                _rotationHint.Hide();
        }

        private void Update()
        {
            if (_pressed || _rotationHint.Showed || _winned)
                return;
            
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _inactivityDelay)
                if (_rotationHint.Showed == false)
                    _rotationHint.Show();
        }

        public override bool OnPressed(Vector2 screenPosition)
        {
            _pressed = true;
            _elapsedTime = 0f;
            
            if (_rotationHint.Showed)
                _rotationHint.Hide();
            
            return false;
        }

        public override void OnMoved(Vector2 screenPosition)
        {
        }

        public override void OnReleased(Vector2 screenPosition)
        {
            _pressed = false;
            _elapsedTime = 0f;
        }
    }
}