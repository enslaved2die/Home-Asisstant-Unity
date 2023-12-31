using UnityEngine;
using Whitepoint.HomeAssistant;

public class LightEntity : MonoBehaviour
{
    Entity.Light _light = new Entity.Light();
    public int id;
    public Entity.light_service service;
    public string entity_id;
    public Entity.color_mode colorMode;
    public Color32 color = Color.white;
    public int kelvin = 3500;
    [Range(0, 255)] public int brightness = 255;

    void InjectData()
    {
        _light.service = service;
        _light.entity_id = entity_id;
        _light.colorMode = colorMode;
        _light.color = color;
        _light.kelvin = kelvin;
        _light.brightness = brightness;
    }
    public void SendData()
    {
        InjectData();
        //Connection.CommandPhase(_lightEntity.CreateEntity());
        Connection.messageQueue = _light.CreateEntity();
    }

    public void On_Off()
    {
        //_lightEntity.service = service;
        _light.entity_id = entity_id;

        if (_light.service == Entity.light_service.turn_on)
        {
            _light.service = Entity.light_service.turn_off;
        }
        else
        {
            _light.service = Entity.light_service.turn_on;
        }
        
        //Connection.CommandPhase(_lightEntity.On_Off());
        Connection.messageQueue = _light.On_Off();
    }

    public void Brightness(float value)
    {
        InjectData();
        _light.brightness = (int)value; 
        //Connection.CommandPhase(_lightEntity.CreateEntity());
        Connection.messageQueue = _light.CreateEntity();
    }
}
