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
        public static bool Intersects(this ILace first, ILace second)
        {
            List<Vector2> firstLace = first.Points
                .Select(point => point.ToXZ())
                .ToList();
            List<Vector2> secondLace = second.Points
                .Select(point => point.ToXZ())
                .ToList();

            for (int i = 1; i < firstLace.Count; ++i)
            {
                Vector2 beginOfLine = firstLace[i - 1];
                Vector2 endOfLine = firstLace[i];

                for (int j = 1; j < secondLace.Count; ++j)
                {
                    Vector2 otherBeginOfLine = secondLace[i - 1];
                    Vector2 otherEndOfLine = secondLace[i];

                    if (LineMath.TryIntersect(beginOfLine, endOfLine, otherBeginOfLine, otherEndOfLine, out _))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}