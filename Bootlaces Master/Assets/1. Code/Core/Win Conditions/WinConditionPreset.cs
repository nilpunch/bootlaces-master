using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    [CreateAssetMenu]
    public class WinConditionPreset : ScriptableObject
    {
        [SerializeField] private WinConditions _winConditions = null;

        public WinConditions Conditions => _winConditions;

        public void LoadConditions(WinConditions conditions)
        {
            _winConditions = conditions;
        }
    }
}