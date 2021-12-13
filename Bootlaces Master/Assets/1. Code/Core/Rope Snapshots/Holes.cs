using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public class Holes : MonoBehaviour
    {
        private List<Hole> _holes;

        private void Awake()
        {
            _holes = GetComponentsInChildren<Hole>().ToList();
        }

        public Hole GetHole(int index)
        {
            return _holes.Find(hole => hole.Index == index);
        }
    }
}