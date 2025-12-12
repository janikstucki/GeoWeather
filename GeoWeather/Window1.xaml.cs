using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace GeoWeather
{
    public partial class Window1 : Window
    {
        public string GetStations()
        {
            List<Station> stations = new List<Station>();

            string query = "SELECT name, xCoordinate, yCoordinate FROM stations";
            string connectionString = @"data source=PC-Janik\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stations.Add(new Station
                            {
                                Name = reader.GetString(0),
                                XCoordinate = reader.GetDouble(2),
                                YCoordinate = reader.GetDouble(1)
                            });
                        }
                    }
                }
            }
            return JsonSerializer.Serialize(stations);
        }
        public Window1()
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
                await Browser.EnsureCoreWebView2Async(); 
                string mapPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
                Browser.CoreWebView2.Navigate(new Uri(mapPath).AbsoluteUri);

                string stationsJson = GetStations();
                string js = $"window.stations = {stationsJson};"; 
                await Browser.CoreWebView2.ExecuteScriptAsync(js);
            };
        }
    }
}
