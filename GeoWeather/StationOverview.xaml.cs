using System;
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
using GeoWeather;

namespace GeoWeather
{
    /// <summary>
    /// Interaktionslogik für StationOverview.xaml
    /// </summary>
    public partial class StationOverview : Window
    {
        public StationOverview(Station s)
        {
            InitializeComponent();
            StationLBL.Content = s.ToString();
        }


    }
}
