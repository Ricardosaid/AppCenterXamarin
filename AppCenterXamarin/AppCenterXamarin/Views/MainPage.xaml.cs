using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCenterXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCenterXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var items = new List<TripLogEntry>
            {
                new TripLogEntry
                {
                    Title = "CDMX",
                    Notes = "Mexico",
                    Rating = 3,
                    Date = new DateTime(2023, 12, 19),
                    Latitude = 38.8977,
                    Longitude = -77.0365
                },
                new TripLogEntry
                {
                    Title = "LOS CABOS",
                    Notes = "Mexico",
                    Rating = 4,
                    Date = new DateTime(2023, 12, 19),
                    Latitude = 38.8977,
                    Longitude = -77.0365
                },
                new TripLogEntry
                {
                    Title = "Munich",
                    Notes = "Alemania",
                    Rating = 2,
                    Date = new DateTime(2023, 12, 19),
                    Latitude = 38.8977,
                    Longitude = -77.0365
                }
            };
            trips.ItemsSource = items;
        }
    }
}