using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class WinConditionChecker : MonoBehaviour
    {
        [SerializeField] private WinConditionPreset _winConditionPreset = null;
        [SerializeField] private RopesLoader _ropesLoader = null;
        [SerializeField] private Grabber _grabber = null;
        [SerializeField] private Holes _holes = null;

        private bool _checkingWin;

        public event Action Winned;
        
        private void Awake()
        {
            _ropesLoader.RopesLoaded += OnRopesLoaded;
        }
        
        private void OnDestroy()
        {
            _ropesLoader.RopesLoaded -= OnRopesLoaded;
            
        }

        private void Update()
        {
            if (_checkingWin == false || _grabber.Grabbing)
                return;

            var currentCondition = WinConditions.Generate(_ropesLoader.Ropes);

            if (currentCondition.Equals(_winConditionPreset.Conditions))
            {
                _checkingWin = false;
                _grabber.DisableInput();
                Winned?.Invoke();
            }
        }

        private void OnRopesLoaded()
        {
            foreach (var placementCondition in _winConditionPreset.Conditions.PlacementConditions)
            {
                _holes.GetHole(placementCondition.FirstHole)
                    .GetComponent<HoleColor>()
                    .SetColor(placementCondition.Color);
                
                _holes.GetHole(placementCondition.SecondHole)
                    .GetComponent<HoleColor>()
                    .SetColor(placementCondition.Color);
            }

            _checkingWin = true;
        }
    }
}