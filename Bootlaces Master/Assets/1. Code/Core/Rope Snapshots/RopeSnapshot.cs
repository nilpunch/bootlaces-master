using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeSnapshot
    {
        public List<float> LengthChanges;
        public List<Vector3> ParticlePositions;
        public List<Vector3> ParticleVelocities;
        public Color Color;
        public int FirstHole;
        public int SecondHole;
    }
}