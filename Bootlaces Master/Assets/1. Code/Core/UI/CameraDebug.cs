using System;
using Cinemachine;
using UnityEngine;

namespace BootlacesMaster.UI
{
    public class CameraDebug : MonoBehaviour
    {
        [SerializeField] private CheatSlider _cameraPositionZ = null;
        [SerializeField] private CheatSlider _cameraPositionY = null;
        [SerializeField] private CheatSlider _cameraRotation = null;

        private CinemachineVirtualCamera _camera;
        private CinemachineTransposer _transposer;
        private CinemachineComposer _composer;
        
        private void Awake()
        {
            _camera = FindObjectOfType<CinemachineVirtualCamera>();

            _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
            _composer = _camera.GetCinemachineComponent<CinemachineComposer>();
        }

        private void OnEnable()
        {
            _cameraPositionZ.SetValue(_transposer.m_FollowOffset.z);
            _cameraPositionY.SetValue(_transposer.m_FollowOffset.y);
            _cameraRotation.SetValue(_composer.m_TrackedObjectOffset.y);
            
            _cameraPositionZ.ValueChanged += OnCameraZValueChanged;
            _cameraPositionY.ValueChanged += OnCameraYValueChanged;
            _cameraRotation.ValueChanged += OnCameraRotationValueChanged;
        }

        private void OnDisable()
        {
            _cameraPositionZ.ValueChanged -= OnCameraZValueChanged;
            _cameraPositionY.ValueChanged -= OnCameraYValueChanged;
            _cameraRotation.ValueChanged -= OnCameraRotationValueChanged;
        }

        private void OnCameraRotationValueChanged()
        {
            _composer.m_TrackedObjectOffset.y = _cameraRotation.Value;
        }

        private void OnCameraYValueChanged()
        {
            _transposer.m_FollowOffset.y  = _cameraPositionY.Value;
        }

        private void OnCameraZValueChanged()
        {
            _transposer.m_FollowOffset.z = _cameraPositionZ.Value;
        }
    }
}