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

                Browser.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                string mapPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
                Browser.CoreWebView2.NavigationCompleted += async (sender, args) =>
                {
                    string stationsJson = GetStations();

                    await Browser.CoreWebView2.ExecuteScriptAsync($"window.stations = {stationsJson};");
                    await Browser.CoreWebView2.ExecuteScriptAsync("addMarkers(window.stations);");
                };

                Browser.CoreWebView2.Navigate(new Uri(mapPath).AbsoluteUri);
            };
        }

        private void CoreWebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
            {
            Station s = new Station();
                string stationName = e.TryGetWebMessageAsString();



            string query = "SELECT station_id, xCoordinate, yCoordinate FROM stations where name = @stationName";
            string connectionString = @"data source=PC-Janik\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@stationName", stationName);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            s.Id = reader.GetInt32(0);
                            s.Name = stationName;
                            s.XCoordinate = reader.GetDouble(1);
                            s.YCoordinate = reader.GetDouble(2);
                        }
                    }
                }
            }



            StationOverview stationOverview = new StationOverview(s);
                stationOverview.Show();
                this.Close();
            }

    }
}
