namespace  Whitepoint.Tools
{
    using System; 
    using UnityEngine;
    public static class Conversions
    {
        public static Color colorTempToRGB(int kelvin)
        {
            // From http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/

            var temp = kelvin / 100;

            double red, green, blue;

            if( temp <= 66 ){ 

                red = 255; 
        
                green = temp;
                green = 99.4708025861 * Math.Log(green) - 161.1195681661;

        
                if( temp <= 19){

                    blue = 0;

                } else {

                    blue = temp-10;
                    blue = 138.5177312231 * Math.Log(blue) - 305.0447927307;

                }

            } else {

                red = temp - 60;
                red = 329.698727446 * Math.Pow(red, -0.1332047592);
        
                green = temp - 60;
                green = 288.1221695283 * Math.Pow(green, -0.0755148492 );

                blue = 255;

            }

            //"r: " + red/255 + "\n" + "g: " + green/255 + "\n" + "b: " + blue/255 + "\n";

            return new Color(
                (float)red / 255,
                (float)green / 255,
                (float)blue / 255,
                1);

        }
    }
}

