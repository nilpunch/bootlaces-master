using System;
using Cinemachine;
using UnityEngine;

namespace BootlacesMaster
{
    public class CameraSwitcher : MonoBehaviour
    {
        private const int HighPriority = 11;
        private const int LowPriority = 9;
        
        [SerializeField] private CinemachineVirtualCamera _startCamera = null;
        [SerializeField] private CinemachineVirtualCamera _mainCamera = null;

        public enum CameraType { Start, Main }

        public void SwitchCamera(CameraType cameraType)
        {
            _startCamera.Priority = LowPriority;
            _mainCamera.Priority = LowPriority;
            
            switch (cameraType)
            {
                case CameraType.Start:
                    _startCamera.Priority = HighPriority;
                    break;
                case CameraType.Main:
                    _mainCamera.Priority = HighPriority;
                    break;
            }
        }
    }
}