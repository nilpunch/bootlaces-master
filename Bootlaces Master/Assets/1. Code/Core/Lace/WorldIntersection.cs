using UnityEngine;

namespace BootlacesMaster
{
    public readonly struct WorldIntersection
    {
        public readonly Vector3 FirstLacePoint;
        public readonly Vector3 SecondLacePoint;

        public WorldIntersection(Vector3 firstLacePoint, Vector3 secondLacePoint)
        {
            FirstLacePoint = firstLacePoint;
            SecondLacePoint = secondLacePoint;
        }
    }
}