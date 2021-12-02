using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public interface ILace
    {
        IEnumerable<Vector3> Points { get; }
    }

    public static class LaceExtensions
    {
        public static bool Intersects(this ILace first, ILace second, out Vector3 intersectionPoint)
        {
            List<Vector3> firstLace = first.Points.ToList();
            List<Vector3> secondLace = second.Points.ToList();

            for (int i = 1; i < firstLace.Count; ++i)
            {
                Vector3 beginOfLine = firstLace[i - 1];
                Vector3 endOfLine = firstLace[i];

                for (int j = 1; j < secondLace.Count; ++j)
                {
                    Vector3 otherBeginOfLine = secondLace[j - 1];
                    Vector3 otherEndOfLine = secondLace[j];

                    if (LineMath.TryIntersect(beginOfLine.ToXZ(), endOfLine.ToXZ(),
                        otherBeginOfLine.ToXZ(), otherEndOfLine.ToXZ(), out var intersection))
                    {
                        intersectionPoint = Vector3.Lerp(beginOfLine, endOfLine, intersection.FirstLineInterpolation);
                        return true;
                    }
                }
            }

            intersectionPoint = new Vector3();
            return false;
        }
    }
}