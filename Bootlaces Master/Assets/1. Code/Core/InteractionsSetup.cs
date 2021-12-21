using System;
using System.Collections;
using BootlacesMaster.UI;
using UnityEngine;

namespace BootlacesMaster
{
    public class InteractionsSetup : MonoBehaviour
    {
        [SerializeField] private CameraSwitcher _cameraSwitcher = null;
        [SerializeField] private FadeButton _startButton = null;
        [SerializeField] private EasingButton _moreHoles = null;
        [SerializeField] private InputQuery _pointerInputSplitter = null;
        [SerializeField] private RotationHintActivator _rotationHintActivator = null;
        [SerializeField] private RotationHint _rotationHint = null;

        [Space, SerializeField] private float _cameraSwitchTime = 1f;
        [SerializeField] private bool _showMoreHolesInstantly = true;

        public event Action LevelStarted;
        public event Action FocusedOnBoot;
        
        private void Start()
        {
            _pointerInputSplitter.enabled = false;
            _rotationHintActivator.enabled = false;
            
            _startButton.Clicked += OnStartClicked;
            _cameraSwitcher.SwitchCamera(CameraSwitcher.CameraType.Start);
        }

        private void OnDestroy()
        {
            _startButton.Clicked -= OnStartClicked;
        }

        private void OnStartClicked()
        {
            StartCoroutine(StartClicked());
        }

        private IEnumerator StartClicked()
        {
            _startButton.Hide();
            
            _cameraSwitcher.SwitchCamera(CameraSwitcher.CameraType.Main);
            _pointerInputSplitter.enabled = true;
            _rotationHintActivator.enabled = true;
            
            LevelStarted?.Invoke();

            yield return new WaitForSeconds(_cameraSwitchTime);
            
            FocusedOnBoot?.Invoke();
            
            _rotationHint.Show();
            
            if (_showMoreHolesInstantly)
                _moreHoles.Show();
        }
    }
}