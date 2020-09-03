using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StreetArtLocation : Rg.Plugins.Popup.Pages.PopupPage
    {
        private float gpsLongitude;
        private float gpsLatitude;

        public StreetArtLocation()
        {
            InitializeComponent();
           
        }

        public StreetArtLocation(float gpsLongitude, float gpsLatitude)
        {
            InitializeComponent();

            MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(gpsLatitude, gpsLongitude), Distance.FromKilometers(0.444));
            Pin p;


            p = new Pin
            {
                Position = new Position(gpsLatitude, gpsLongitude),
                Label = "Pic Location",
                Type = PinType.Generic
            };
            map.Pins.Add(p);




           
            map.MoveToRegion(mapSpan);
            this.gpsLongitude = gpsLongitude;
            this.gpsLatitude = gpsLatitude;
        }

        private async void ClosePopup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}