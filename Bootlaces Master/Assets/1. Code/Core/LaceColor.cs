using UnityEngine;

namespace BootlacesMaster
{
    [CreateAssetMenu]
    public class LaceColor : ScriptableObject
    {
        [SerializeField] private Color _color = Color.red;

        public Color Color => _color;
    }
}