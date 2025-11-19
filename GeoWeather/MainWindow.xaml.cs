using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GeoWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StationNameTBX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StationXCoordinateTBX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StationYCoordinateTBX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"data source=PC-Janik\SQLEXPRESS;initial catalog=stations_db;trusted_connection=true;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "insert into stations (name, xCoordinate, yCoordinate) values (@name,@xCoordinate, @yCoordinate)";

                
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", stationNameTBX.Text);
                        command.Parameters.AddWithValue("@xCoordinate", stationXCoordinateTBX.Text);
                        command.Parameters.AddWithValue("@yCoordinate", stationYCoordinateTBX.Text);

                        connection.Open();
                        command.ExecuteNonQuery();
                        {
                            MessageBox.Show("Data inserted successfully.");
                        }
                    }
                }

            }

        private void overviewWindowBTN_Click(object sender, RoutedEventArgs e)
        {
            overview overviewWindow = new overview(); 
            overviewWindow.Show(); 
        }
    }
    
}



//get constring

//select
//    'data source=' + @@servername +
//    ';initial catalog=' + db_name() +
//    case type_desc
//        when 'WINDOWS_LOGIN' 
//            then ';trusted_connection=true'
//        else
//            ';user id=' + suser_name() + ';password=<<YourPassword>>'
//    end
//    as ConnectionString
//from sys.server_principals
//where name = suser_name()