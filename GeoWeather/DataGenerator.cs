using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.Data.SqlClient;
using GeoWeather;
using Microsoft.Identity.Client;
using System.Windows;
using System.Windows.Controls;


namespace GeoWeather
{
    internal class DataGenerator(int stationId)
    {
        public int countIds = 0;
        private Random random = new Random();


        public void GenerateData()
        {
            List<string> WindDirections = new List<string> { "N", "NE", "NW", "E", "W", "SE", "S", "SW" };
            int selectWindDirection = random.Next(WindDirections.Count);

            DateTime dateTime = DateTime.Now;

            string connectionString = @"data source=PC-Janik\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "insert into stationData (station_id, timestamp, temperatur, humidity, windSpeed, windDirection) values (@station_id, @timestamp, @temperatur, @humidity, @windSpeed, @windDirection)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@station_id", stationId);
                    command.Parameters.AddWithValue("@timestamp", dateTime);
                    command.Parameters.AddWithValue("@temperatur", random.Next(-20, 40));
                    command.Parameters.AddWithValue("@humidity", random.Next(0, 100));
                    command.Parameters.AddWithValue("@windSpeed", random.Next(0, 150));
                    command.Parameters.AddWithValue("@windDirection", WindDirections[selectWindDirection]);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
