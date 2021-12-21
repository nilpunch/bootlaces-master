using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopesLoader : MonoBehaviour
    {
        [SerializeField] private ObiSolver _obiSolver = null;
        [SerializeField] private Holes _holes = null;
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        [SerializeField] private RopeSetup _ropePrefab = null;
        [SerializeField] private LevelSnapshotPreset _levelSnapshotPreset = null;

        private List<RopeLace> _ropeLaces = new List<RopeLace>();

        public event Action RopesLoaded;

        public IEnumerable<RopeLace> Ropes => _ropeLaces;
        
        private IEnumerator Start()
        {
            foreach (var ropeSnapshot in _levelSnapshotPreset.RopeSnapshot)
            {
                var rope = Instantiate(_ropePrefab);
                rope.Init(_obiSolver, _holes, ropeSnapshot);
                rope.GetComponent<HandlesSetup>()?.Init(_winConditionChecker);
                _ropeLaces.Add(rope.GetComponent<RopeLace>());
            }
            
            RopesLoaded?.Invoke();
            
            yield break;
        }
    }
}