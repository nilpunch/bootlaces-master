using System;
using UnityEngine;

namespace BootlacesMaster
{
    public static class LineMath
    {
        private const double Epsilon = 1e-10;

        private static bool IsZero(double d)
        {
            return Math.Abs(d) < Epsilon;
        }

        private static float Cross(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.y - lhs.y * rhs.x;
        }

        public struct Intersection
        {
            public readonly Vector2 Point;
            public readonly float FirstLineInterpolation;
            public readonly float SecondLineInterpolation;

            public Intersection(Vector2 point, float firstLineInterpolation, float secondLineInterpolation)
            {
                Point = point;
                FirstLineInterpolation = firstLineInterpolation;
                SecondLineInterpolation = secondLineInterpolation;
            }
        }

        /// <summary>
        /// Test whether two line segments intersect. If so, calculate the intersection point.
        /// </summary>
        /// <param name="firstBegin">Vector to the start point of p.</param>
        /// <param name="firstEnd">Vector to the end point of p.</param>
        /// <param name="secondBegin">Vector to the start point of q.</param>
        /// <param name="secondEnd">Vector to the end point of q.</param>
        /// <param name="intersection">The point of intersection, if any.</param>
        /// <param name="considerOverlapAsIntersect">Do we consider overlapping lines as intersecting?</param>
        /// <returns>True if an intersection point was found.</returns>
        public static bool TryIntersect(Vector2 firstBegin, Vector2 firstEnd, Vector2 secondBegin, Vector2 secondEnd,
            out Intersection intersection, bool considerCollinearOverlapAsIntersect = false)
        {
            intersection = new Intersection();

            Vector2 firstRadius = firstEnd - firstBegin;
            Vector2 secondRadius = secondEnd - secondBegin;

            float radiusCross = Cross(firstRadius, secondRadius);
            float semiRadiusCross = Cross(secondBegin - firstBegin, firstRadius);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (IsZero(radiusCross) && IsZero(semiRadiusCross))
            {
                // If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping.
                if (considerCollinearOverlapAsIntersect)
                    if (0f <= Vector2.Dot(secondBegin - firstBegin, firstRadius) &&
                        Vector2.Dot(secondBegin - firstBegin, firstRadius) <= Vector2.Dot(firstRadius, firstRadius)
                        || 0 <= Vector2.Dot(firstBegin - secondBegin, secondRadius) &&
                        Vector2.Dot(firstBegin - secondBegin, secondRadius) <= Vector2.Dot(secondRadius, secondRadius))
                        return true;

                // If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            // If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (IsZero(radiusCross) && IsZero(semiRadiusCross) == false)
                return false;

            // t = (q - p) x s / (r x s)
            float firstLineInterpolation = Cross(secondBegin - firstBegin, secondRadius) / radiusCross;

            // u = (q - p) x r / (r x s)
            float secondLineInterpolation = Cross(secondBegin - firstBegin, firstRadius) / radiusCross;

            // If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (IsZero(radiusCross) == false
                && 0f <= firstLineInterpolation && firstLineInterpolation <= 1f
                && 0f <= secondLineInterpolation && secondLineInterpolation <= 1f)
            {
                // We can calculate the intersection point using either t or u.
                Vector2 intersectionPoint = firstBegin + firstLineInterpolation * firstRadius;
                intersection = new Intersection(intersectionPoint, firstLineInterpolation, secondLineInterpolation);

                // An intersection was found.
                return true;
            }

            // Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }
    }
}