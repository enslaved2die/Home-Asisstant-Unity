using UnityEditor;
using Whitepoint.HomeAssistant;

[CustomEditor(typeof(ClimateEntity))]
[CanEditMultipleObjects]
public class ClimateEntityEditor : Editor
{
    private SerializedProperty _service, _entity_id,  _temperature, _hvac_mode, _preset_mode;

    private void OnEnable()
    {
        _service = serializedObject.FindProperty("service");
        _entity_id = serializedObject.FindProperty("entity_id");
        _temperature = serializedObject.FindProperty("temperature");
        _hvac_mode = serializedObject.FindProperty("hvac_mode");
        _preset_mode = serializedObject.FindProperty("preset_mode");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_entity_id);
        EditorGUILayout.PropertyField(_service);
        if (_service.intValue == (int)Entity.climate_service.set_hvac_mode)
        {
            EditorGUILayout.PropertyField(_hvac_mode);
            if(_hvac_mode.intValue == (int)Entity.hvac_mode.heat)
                EditorGUILayout.PropertyField(_temperature);
        }

        if (_service.intValue == (int)Entity.climate_service.set_temperature)
        {
            EditorGUILayout.PropertyField(_temperature);
        }
        if (_service.intValue == (int)Entity.climate_service.set_preset_mode)
            EditorGUILayout.PropertyField(_preset_mode);
        
        serializedObject.ApplyModifiedProperties();
    }
}
