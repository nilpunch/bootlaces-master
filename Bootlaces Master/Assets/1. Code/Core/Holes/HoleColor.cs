using UnityEngine;

namespace BootlacesMaster
{
    public class HoleColor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private float _colorValue = 0.7f;
        
        public void SetColor(Color color)
        {
            Color.RGBToHSV(color, out var h, out var s, out var _);
            Color.RGBToHSV(_meshRenderer.material.color, out _, out var _, out _);
            
            _meshRenderer.material.color = Color.HSVToRGB(h, s, _colorValue);
        }
    }
}