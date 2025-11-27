
using Microsoft.Data.SqlClient;
using System.Windows;

namespace GeoWeather
{
    public partial class StationOverview : Window
    {
        public StationOverview(Station s)
        {
            InitializeComponent();

            StationLBL.Content = s.ToString();
            LoadStationData(s.Id);


            DataGenerator generator = new DataGenerator(s.Id);

            generator.GenerateData();
        }

        private void LoadStationData(int stationId)
        {
            List<StationData> data = new List<StationData>();

            string query = "SELECT data_id, station_id, temperatur, humidity, windSpeed, windDirection " +
                           "FROM StationData WHERE station_id = @id";

            string connectionString = @"data source=PC-Janik\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", stationId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new StationData
                            {
                                DataId = reader.GetInt32(0),
                                StationId = reader.GetInt32(1),
                                Temperatur = reader.GetDouble(2),
                                Humidity = reader.GetDouble(3),
                                WindSpeed = reader.GetDouble(4),
                                WindDirection = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            stationsListBox.ItemsSource = data;
        }
    }
}