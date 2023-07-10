using UnityEditor;
using Whitepoint.HomeAssistant;

[CustomEditor(typeof(LightEntity))]
[CanEditMultipleObjects]
public class LightEntityEditor : Editor
{
    private SerializedProperty _service, _entity_id, _color_mode, _color, _kelvin, _brightness;

    private void OnEnable()
    {
        _service = serializedObject.FindProperty("service");
        _entity_id = serializedObject.FindProperty("entity_id");
        _color_mode = serializedObject.FindProperty("colorMode");
        _color = serializedObject.FindProperty("color");
        _kelvin = serializedObject.FindProperty("kelvin");
        _brightness = serializedObject.FindProperty("brightness");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_entity_id);
        EditorGUILayout.PropertyField(_service);
        EditorGUILayout.PropertyField(_color_mode);
        if (_color_mode.intValue != (int)Entity.color_mode.none)
        {
            if(_color_mode.intValue == (int)Entity.color_mode.rgb) 
                EditorGUILayout.PropertyField(_color);
            else if(_color_mode.intValue == (int)Entity.color_mode.temperature) 
                EditorGUILayout.PropertyField(_kelvin);
        }
        EditorGUILayout.PropertyField(_brightness); 
        serializedObject.ApplyModifiedProperties();
    }
}
