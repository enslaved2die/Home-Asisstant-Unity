using UnityEngine;
using Whitepoint.HomeAssistant;

public class ClimateEntity : MonoBehaviour
{
    Entity.Climate _climate = new Entity.Climate();
    public int id;
    public Entity.climate_service service;
    public string entity_id;
    public float temperature;
    public Entity.hvac_mode hvac_mode;
    public Entity.preset_mode preset_mode;


    void InjectData()
    {
        _climate.service = service;
        _climate.entity_id = entity_id;
        _climate.temperature = temperature;
        _climate.hvac_mode = hvac_mode;
        _climate.preset_mode = preset_mode;

    }
    public void SendData()
    {
        InjectData();
        //Connection.CommandPhase(_lightEntity.CreateEntity());
        Connection.messageQueue = _climate.CreateEntity();
    }

    /*public void Temperature(float value)
    {
        InjectData();
        _climateEntity.temperature = value; 
        //Connection.CommandPhase(_lightEntity.CreateEntity());
        Connection.messageQueue = _climateEntity.CreateEntity();
    }*/
}
