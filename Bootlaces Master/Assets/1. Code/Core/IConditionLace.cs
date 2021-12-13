using UnityEngine;

namespace BootlacesMaster
{
    public interface IConditionLace : ILace
    {
        public Color Color { get; }
        
        public int FirstHole { get; }
        
        public int SecondHole { get; }
    }
}