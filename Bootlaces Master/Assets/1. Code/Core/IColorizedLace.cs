using UnityEngine;

namespace BootlacesMaster
{
    public interface IColorizedLace : ILace
    {
        public Color Color { get; }
    }
}