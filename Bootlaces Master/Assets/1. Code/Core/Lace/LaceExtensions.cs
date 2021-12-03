using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public static class LaceExtensions
    {
        public static IEnumerable<LineSegment> ToSegments(this IEnumerable<Vector3> points)
        {
            return points.Pairwise((begin, end) => new LineSegment(begin, end));
        }
        
        public static bool Intersects(this ILace first, ILace second, out IEnumerable<WorldIntersection> intersectionPoints)
        {
            intersectionPoints = Enumerable.Empty<WorldIntersection>();

            foreach (var firstLaceSegment in first.Points.ToSegments())
            {
                foreach (var secondLaceSegment in second.Points.ToSegments())
                {
                    if (LineMath.TryIntersect(firstLaceSegment.Begin.ToXZ(), firstLaceSegment.End.ToXZ(),
                        secondLaceSegment.Begin.ToXZ(), secondLaceSegment.End.ToXZ(), out var intersection))
                    {
                        Vector3 firstIntersectionPoint = Vector3.Lerp(firstLaceSegment.Begin, firstLaceSegment.End, intersection.FirstSegmentInterpolation);
                        Vector3 secondIntersectionPoint = Vector3.Lerp(secondLaceSegment.Begin, secondLaceSegment.End, intersection.SecondSegmentInterpolation);
                        
                        WorldIntersection worldIntersection = new WorldIntersection(firstIntersectionPoint, secondIntersectionPoint);
                        
                        intersectionPoints = intersectionPoints.Concat(worldIntersection.Yield());
                    }
                }
            }

            return ReferenceEquals(intersectionPoints, Enumerable.Empty<WorldIntersection>()) == false;
        }
    }
}