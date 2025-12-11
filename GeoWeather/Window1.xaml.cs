using System;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace GeoWeather
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await Browser.EnsureCoreWebView2Async();
                string mapPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");
                Browser.CoreWebView2.Navigate(new Uri(mapPath).AbsoluteUri);
            };
        }
    }
}
