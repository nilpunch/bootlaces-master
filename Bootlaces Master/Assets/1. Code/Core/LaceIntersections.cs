using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BootlacesMaster
{
    public class LaceIntersections : MonoBehaviour
    {
        [SerializeField] private Color _intersectionUpperPointColor = Color.green;
        [SerializeField] private Color _intersectionLowerPointColor = Color.red;
        [SerializeField] private float _intersectionPointRadius = 0.5f;

        private Lace[] _laces = null;

        private Dictionary<ILace, Color> _colors;

        private void Awake()
        {
            _laces = FindObjectsOfType<Lace>().ToArray();

            _colors = new Dictionary<ILace, Color>();

            foreach (var lace in _laces)
            {
                _colors.Add(lace, Random.ColorHSV(0f, 1f, 0.9f, 0.9f, 0.9f, 0.9f));
            }
        }

        private void OnDrawGizmos()
        {
            if (_laces == null)
                return;
            
            foreach (var (firstLace, secondLace) in _laces.DistinctPairs((first, second) => (first, second)))
            {
                Gizmos.color = _colors[firstLace];
                foreach (var segment in firstLace.Points.ToSegments())
                    Gizmos.DrawLine(segment.Begin, segment.End);
                
                if (firstLace.Intersects(secondLace, out var intersections) == false)
                    continue;
                
                foreach (var intersection in intersections)
                {
                    if (intersection.FirstLacePoint.y > intersection.SecondLacePoint.y)
                    {
                        Gizmos.color = _intersectionUpperPointColor;
                        Gizmos.DrawSphere(intersection.FirstLacePoint, _intersectionPointRadius);
                        Gizmos.color = _intersectionLowerPointColor;
                        Gizmos.DrawSphere(intersection.SecondLacePoint, _intersectionPointRadius);
                    }
                    else
                    {
                        Gizmos.color = _intersectionUpperPointColor;
                        Gizmos.DrawSphere(intersection.SecondLacePoint, _intersectionPointRadius);
                        Gizmos.color = _intersectionLowerPointColor;
                        Gizmos.DrawSphere(intersection.FirstLacePoint, _intersectionPointRadius);
                    }
                }
            }
        }
    }
}