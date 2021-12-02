using UnityEditor;
using UnityEngine;

/// <summary>
/// Attribute to select a single layer.
/// </summary>
public class LayerAttribute : PropertyAttribute
{ 
#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
            EditorGUI.EndProperty();
        }
    }
#endif
}