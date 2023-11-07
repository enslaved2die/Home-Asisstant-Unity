using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Whitepoint.HomeAssistant
{
    public class Entity : MonoBehaviour
    {
        public static int connectionID;

        #region Light
        
        public enum light_service {turn_on, turn_off}
        public enum color_mode {rgb, temperature, none}
        
        #endregion

        #region Climate
        
        public enum climate_service
        {
            // set_aux_heat, 
            set_preset_mode, 
            set_temperature, 
            // set_humidity,
            // set_fan_mode, 
            set_hvac_mode, 
            // set_swing_mode, 
            // turn_on, 
            // turn_off
        }
        public enum hvac_mode
        {
            off, 
            heat, 
            auto, 
            // cool,
            // heat_cool, 
            // dry, 
            // fan_only
        }

        public enum preset_mode
        {
            // eco,
            away,
            // boost,
            // comfort,
            home,
            // sleep,
            // activity
        }

        #endregion

        public class Light
        {
            public light_service service;
            public string entity_id;
            public color_mode colorMode;
            public Color32 color = Color.white;
            public int kelvin;
            [Range(0, 255)] public int brightness;
            
            public string CreateEntity()
            {
                connectionID = Connection.CurrentID++;

                if (service == light_service.turn_on)
                {
                    if (colorMode == color_mode.rgb)
                    {
                        string rgb = "["+ color.r + "," + color.g + ","+ color.b +"]";
                        
                        return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": " 
                               + "{\"rgb_color\": " + rgb + "," 
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }

                    if (colorMode == color_mode.temperature)
                    {
                        return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": " 
                               + "{\"color_temp_kelvin\": " + kelvin + "," 
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }

                    if (colorMode == color_mode.none)
                    {
                        return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                               + service.ToString() 
                               +"\",\"service_data\": {"
                               + "\"brightness\": \"" + brightness +"\"}," 
                               + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                    }
                }
                
                //OFF
                return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                       + service.ToString() + "\",\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
            }

            public string On_Off()
            {
                connectionID = Connection.CurrentID++;
                return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                       + service.ToString() + "\",\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
            }
        }
        public class Climate
        {
            public climate_service service;
            public string entity_id;
            public float temperature;
            public hvac_mode hvac_mode;
            public preset_mode preset_mode;
            
            public string CreateEntity()
            {
                connectionID = Connection.CurrentID++;

                if (service == climate_service.set_temperature)
                {
                    return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"climate\",\"service\": \""
                           + service.ToString() 
                           +"\",\"service_data\": " 
                           + "{\"temperature\": " + temperature + "," 
                           + "\"hvac_mode\": \"heat\"}," 
                           + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                }

                if (service == climate_service.set_hvac_mode)
                {
                    return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"climate\",\"service\": \""
                           + service.ToString() 
                           +"\",\"service_data\": " 
                           + "{\"hvac_mode\": \"" + hvac_mode +"\"}," 
                           + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                }
                
                if (service == climate_service.set_preset_mode)
                {
                    return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"climate\",\"service\": \""
                           + service.ToString() 
                           +"\",\"service_data\": " 
                           + "{\"preset_mode\": \"" + preset_mode +"\"}," 
                           + "\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
                }
                
                //OFF
                return "{\"id\":"+ connectionID + ",\"type\": \"call_service\",\"domain\": \"light\",\"service\": \""
                       + service.ToString() + "\",\"target\": {\"entity_id\": \"" + entity_id +"\"}}";
            }
        }
    }
}

