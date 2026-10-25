
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MenuVolumeSlider))]
[CanEditMultipleObjects]
public class MenuVolumeSliderEditor : SliderEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "Handle Sprites",
            EditorStyles.boldLabel
        );

        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("handleImage")
        );

        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("focusedSprite")
        );

        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("editingSprite")
        );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "Keyboard / Gamepad",
            EditorStyles.boldLabel
        );

        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("volumeStep")
        );

        serializedObject.ApplyModifiedProperties();
    }
}
