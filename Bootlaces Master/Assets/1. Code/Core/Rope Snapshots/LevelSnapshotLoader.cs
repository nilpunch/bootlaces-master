using System.Collections;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class LevelSnapshotLoader : MonoBehaviour
    {
        [SerializeField] private ObiSolver _obiSolver = null;
        [SerializeField] private Holes _holes = null;
        [SerializeField] private RopeSetup _ropePrefab = null;
        [SerializeField] private LevelSnapshotPreset _levelSnapshotPreset = null;

        private void Start()
        {
            foreach (var ropeSnapshot in _levelSnapshotPreset.RopeSnapshot)
            {
                var rope = Instantiate(_ropePrefab);
                rope.Init(_obiSolver, _holes, ropeSnapshot);
            }
        }
    }
}