using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLogicLibrary.Stuff
{
    public class JsonEditor
    {
        /// <summary>
        /// Ange fordonstyp och vilket fält du vill läsa av
        /// </summary>
        /// <param name="field"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string ReadProperty(string property, string field)
        {
            string json = File.ReadAllText(@"GarageSettings.json");

            dynamic jsonObject = JsonConvert.DeserializeObject(json);

            if (property.ToLower() == "car" || property.ToLower() == "motorcycle" || property.ToLower() == "bus" || property.ToLower() == "bike")
            {
                if (field.ToLower() == "size" || field.ToLower() == "cost")
                {
                    return jsonObject[char.ToUpper(property[0]) + property.Substring(1)][char.ToUpper(field[0]) + field.Substring(1)];
                }
                else
                {
                    return String.Empty;
                }
            }
            else if (property.ToLower() == "parkingspot" || property.ToLower() == "parkinggarage")
            {
                if (field.ToLower() == "size")
                {
                    return jsonObject[char.ToUpper(property[0]) + property.Substring(1)][char.ToUpper(field[0]) + field.Substring(1)];
                }
                else
                {
                    return String.Empty;
                }
            }
            return String.Empty;
        }
        /// <summary>
        /// Ange vilket fordon du vill ändra, vilket fält och vad det nya värdet ska bli, VAR NOGRANN! Teckenkänsliga fält och värdet måste vara av rätt typ!, ska det vara en int, då ska den skickas in som en int!
        /// </summary>
        /// <param name="property"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void EditProperty(string property, string field, int value)
        {
            string path = @"GarageSettings.json";
            string json = File.ReadAllText(path);

            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            if (value >= 0 && value < 2000000)
            {
                if (property.ToLower() == "car" || property.ToLower() == "motorcycle" || property.ToLower() == "bus" || property.ToLower() == "bike")
                {
                    if (/*field.ToLower() == "size" || */field.ToLower() == "cost")
                    {
                        jsonObject[char.ToUpper(property[0]) + property.Substring(1)][char.ToUpper(field[0]) + field.Substring(1)] = value;
                    }
                    else return;
                }
                else if (/*property.ToLower() == "parkingspot" ||*/ property.ToLower() == "parkinggarage")
                {
                    if (field.ToLower() == "size")
                    {
                        jsonObject[char.ToUpper(property[0]) + property.Substring(1)][char.ToUpper(field[0]) + field.Substring(1)] = value;

                    }
                    else return;

                }
                else return;
            }
            else return;

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(jsonObject);
            }
        }

        public string ReturnAllTextClean()
        {
            Regex regex = new Regex("[,.{}\"]");
            string path = @"GarageSettings.json";
            string json = File.ReadAllText(path);
            string output = regex.Replace(json, " ");
            return output;
        }

        public string JsonSerializer(object vehicle)
        {
            string output = JsonSerializer(vehicle);
            return output;
        }
        public object JsonDeserializer(string vehicle)
        {
            object output = JsonDeserializer(vehicle);
            return output;
        }
    }
}
