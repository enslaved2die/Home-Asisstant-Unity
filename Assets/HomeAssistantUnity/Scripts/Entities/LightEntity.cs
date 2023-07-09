using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whitepoint.HomeAssistant;

public class LightEntity : MonoBehaviour
{
    public Entity.LightEntity _lightEntity = new Entity.LightEntity();

    public void SendData()
    {
        Connection.CommandPhase(_lightEntity.CreateEntity());
    }

    public void On_Off()
    {
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
}
