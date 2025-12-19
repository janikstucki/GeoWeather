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
        public overview()
        {
            InitializeComponent();

            List<Station> stations = new List<Station>();



            string query = "SELECT station_id, name, xCoordinate, yCoordinate FROM stations";
            string connectionString = @"data source=NOTEBOOK-JANIK\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";
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

            stationsListBox.ItemsSource = stations;
        }
        private void stationsListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Station s = (Station)stationsListBox.SelectedItem;
            StationOverview stationOverview = new StationOverview(s);
            stationOverview.Show();
            this.Close();
        }

        private void StationOverviewBTN_click(object sender, RoutedEventArgs e)
        { 


            
        }

        private void testBTN_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
            this.Close();
        }
    }
}