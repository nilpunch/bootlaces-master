using Cinemachine;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Vector2Int _targetAspectRatio = new Vector2Int(9, 16);
        [SerializeField, Range(0, 1)] private float _widthOrHeight = 0f;

        private CinemachineVirtualCamera _camera;
    
        private float _originalOrthographicSize;
        private float _targetAspect;

        private float _verticalFov;
        private float _horizontalFov;

        private Vector2Int _deviceScreenResolution;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();

            InitializeScales();
        }
    
        private void InitializeScales()
        {
            _deviceScreenResolution = new Vector2Int(0, 0);
        
            _originalOrthographicSize = _camera.m_Lens.OrthographicSize;

            _targetAspect = (float)_targetAspectRatio.x / _targetAspectRatio.y;

            _verticalFov = _camera.m_Lens.FieldOfView;
            _horizontalFov = CalculateFov(_verticalFov, 1 / _targetAspect);
        }

        private void Update()
        {
            if (_deviceScreenResolution.x == Screen.width && _deviceScreenResolution.y == Screen.height)
                return;
            
            _deviceScreenResolution.x = Screen.width;
            _deviceScreenResolution.y = Screen.height;
        
            if (_camera.m_Lens.Orthographic)
            {
                float currentWidthSize = _originalOrthographicSize * (_targetAspect / _camera.m_Lens.Aspect);
                _camera.m_Lens.OrthographicSize = Mathf.Lerp(currentWidthSize, _originalOrthographicSize, _widthOrHeight);
            }
            else
            {
                float currentVerticalFov = CalculateFov(_horizontalFov, _camera.m_Lens.Aspect);
                _camera.m_Lens.FieldOfView = Mathf.Lerp(currentVerticalFov, _verticalFov, _widthOrHeight);
            }
        }

        private float CalculateFov(float fovInDeg, float aspectRatio)
        {
            float fovInRads = fovInDeg * Mathf.Deg2Rad;

            float convertedFovInRads = 2f * Mathf.Atan(Mathf.Tan(fovInRads / 2f) / aspectRatio);

            return convertedFovInRads * Mathf.Rad2Deg;
        }
    }
}