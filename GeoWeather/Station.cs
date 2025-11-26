using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWeather
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name} ({XCoordinate}, {YCoordinate})";
        }
    }
}
