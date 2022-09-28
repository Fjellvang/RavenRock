using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ExposedScriptableObjectAttribute), true)]
public class ExposedScriptableObjectAttributeDrawer : PropertyDrawer
{
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        // draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        // Draw folout peroperties
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            // draw object properties

            if (!editor)
            {
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
            }

            editor.OnInspectorGUI();

            //Set level back to what it was
            EditorGUI.indentLevel--;
        }
    }
}
#endif
