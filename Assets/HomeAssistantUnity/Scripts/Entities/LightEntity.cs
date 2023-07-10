using UnityEngine;
using Whitepoint.HomeAssistant;

public class LightEntity : MonoBehaviour
{
    Entity.LightEntity _lightEntity = new Entity.LightEntity();
    public int id;
    public Entity.light_service service;
    public string entity_id;
    public Entity.color_mode colorMode;
    public Color32 color = Color.white;
    public int kelvin = 3500;
    [Range(0, 255)] public int brightness = 255;

    void InjectData()
    {
        _lightEntity.service = service;
        _lightEntity.entity_id = entity_id;
        _lightEntity.colorMode = colorMode;
        _lightEntity.color = color;
        _lightEntity.kelvin = kelvin;
        _lightEntity.brightness = brightness;
    }
    public void SendData()
    {
        InjectData();
        Connection.CommandPhase(_lightEntity.CreateEntity());
    }

    public void On_Off()
    {
        //_lightEntity.service = service;
        _lightEntity.entity_id = entity_id;

        if (_lightEntity.service == Entity.light_service.turn_on)
        {
            _lightEntity.service = Entity.light_service.turn_off;
        }
        else
        {
            _lightEntity.service = Entity.light_service.turn_on;
        }
        
        Connection.CommandPhase(_lightEntity.On_Off());
    }

    public void Brightness(float value)
    {
        InjectData();
        _lightEntity.brightness = (int)value; 
        //Connection.CommandPhase(_lightEntity.CreateEntity());
        Connection.messageQueue = _lightEntity.CreateEntity();
    }
}
