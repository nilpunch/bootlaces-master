using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BootlacesMaster
{
    public class LevelSnapshotMaker : MonoBehaviour
    {
        [ContextMenu("Save Level")]
        private void SaveSnapshot()
        {
#if UNITY_EDITOR
            var ropeSnapshotPreset = ScriptableObject.CreateInstance<LevelSnapshotPreset>();
            
            var ropeSnapshots = FindObjectsOfType<RopeSnapshotRecorder>()
                .Select(recorder => recorder.MakeSnapshot()).ToList();
            
            ropeSnapshotPreset.RopeSnapshot = ropeSnapshots;
            
            string path = EditorUtility.SaveFilePanel("Save Level Snapshot", "Assets", "Level", "asset");
            
            if (path.Length != 0)
            {
                path = path.Remove(0, path.IndexOf(@"Assets"));
                AssetDatabase.CreateAsset(ropeSnapshotPreset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
#endif
        }
    }
}