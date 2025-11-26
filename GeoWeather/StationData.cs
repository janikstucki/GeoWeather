using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWeather
{
    public class StationData
    {
        public int DataId { get; set; }         
        public int StationId { get; set; }      
        public double Temperatur { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; }

        public override string ToString()
        {
            return $"{DataId}: {Temperatur}°C, {Humidity}%, {WindSpeed} km/h, {WindDirection}";
        }
    }
}
