using UnityEngine;
using Whitepoint.HomeAssistant;

public class SwitchEntity : MonoBehaviour
{
    /*Entity.Switch _light = new Entity.Switch();
    public int id;
    public Entity.light_service service;
    public string entity_id;

    void InjectData()
    {
        _light.service = service;
        _light.entity_id = entity_id;
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
    }*/
}
