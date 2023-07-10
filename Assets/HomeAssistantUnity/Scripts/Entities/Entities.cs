using System;
using UnityEngine;

namespace Whitepoint.HomeAssistant
{
    public class Entity : MonoBehaviour
    {
        public enum light_service {turn_on, turn_off}
        public enum color_mode {rgb, temperature, none}
        
        // [Serializable]
        // public class ServiceData
        // {
        //     public string color_name;
        //     public string brightness;
        // }
        //
        // [Serializable]
        // public class Target
        // {
        //     public string entity_id;
        // }
        //
        // [Serializable]
        // public class Service
        // {
        //     public int id;
        //     public string type;
        //     public string domain;
        //     public string service;
        //     public ServiceData service_data;
        //     public Target target;
        // }
        
        public class LightEntity
        {
            public int id;
            public light_service service;
            public string entity_id;
            public color_mode colorMode;
            public Color32 color = Color.white;
            public int kelvin;
            [Range(0, 255)] public int brightness;
            
            public string CreateEntity()
            {
                id = Connection.CurrentID++;

                if (service == light_service.turn_on)
                {
                    if (colorMode == color_mode.rgb)
                    {
                        string rgb = "["+ color.r + "," + color.g + ","+ color.b +"]";
                        
                        return "{\"id\":"+ id + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": " 
                               + "{\"rgb_color\": " + rgb + "," 
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }

                    if (colorMode == color_mode.temperature)
                    {
                        return "{\"id\":"+ id + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": " 
                               + "{\"color_temp_kelvin\": " + kelvin + "," 
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }

                    if (colorMode == color_mode.none)
                    {
                        return "{\"id\":"+ id + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": {"
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }
                }
                
                //OFF
                return "{\"id\":"+ id + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                       + service.ToString() + "\",\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
            }

            public string On_Off()
            {
                id = Connection.CurrentID++;
                return "{\"id\":"+ id + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                       + service.ToString() + "\",\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
            }
        }
    }
}

