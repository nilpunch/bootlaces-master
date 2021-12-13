using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    [CreateAssetMenu]
    public class LevelSnapshotPreset : ScriptableObject
    {
        [field: SerializeField] public List<RopeSnapshot> RopeSnapshot { get; set; }
    }
}