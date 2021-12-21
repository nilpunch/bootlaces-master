using UnityEngine;

namespace BootlacesMaster
{
    public readonly struct LineSegment
    {
        public readonly Vector3 Begin;
        public readonly Vector3 End;

        public LineSegment(Vector3 begin, Vector3 end)
        {
            Begin = begin;
            End = end;
        }
    }
}