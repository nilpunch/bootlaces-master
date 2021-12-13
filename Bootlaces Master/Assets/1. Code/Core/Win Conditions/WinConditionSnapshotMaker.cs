#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public class WinConditionSnapshotMaker : MonoBehaviour
    {
        [ContextMenu("Make Win Snapshot")]
        public void MakeConditionSnapshot()
        {
#if UNITY_EDITOR
            var winCondition = ScriptableObject.CreateInstance<WinConditionPreset>();

            var winConditions = WinConditions.Generate(FindObjectsOfType<Lace>());
            
            winCondition.LoadConditions(winConditions);
            
            string path = EditorUtility.SaveFilePanel("Save WinCondition Snapshot", "Assets", "Win Condition", "asset");

            
            if (path.Length != 0)
            {
                path = path.Remove(0, path.IndexOf(@"Assets"));
                AssetDatabase.CreateAsset(winCondition, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
#endif
        }
    }
}