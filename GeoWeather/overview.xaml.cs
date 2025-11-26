using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeoWeather
{
    /// <summary>
    /// Interaktionslogik für overview.xaml
    /// </summary>
    public partial class overview : Window
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


        public overview()
        {
            InitializeComponent();

            List<Station> stations = new List<Station>();



            string query = "SELECT station_id, name, xCoordinate, yCoordinate FROM stations";
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
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                XCoordinate = reader.GetDouble(2),
                                YCoordinate = reader.GetDouble(3)
                            });
                         }
                    }
                }
            }
            //foreach (Station station in stations)
            //{

            //    stationsListBox.Items.Add($"{station.Id}: {station.Name} ({station.XCoordinate}, {station.YCoordinate})");
            //}

            stationsListBox.ItemsSource = stations;
        }
        private void stationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
                Station s = (Station)stationsListBox.SelectedItem;
                MessageBox.Show($"{s.Name} ausgewählt");
        }

    }
}