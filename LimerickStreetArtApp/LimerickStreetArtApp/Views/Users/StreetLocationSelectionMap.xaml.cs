using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StreetLocationSelectionMap : ContentPage
	{
		public StreetLocationSelectionMap()
		{
			InitializeComponent();
			MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(double.Parse("52.65970230"), double.Parse("-8.54024124")), Distance.FromKilometers(0.444));
			map.MoveToRegion(mapSpan);

		}

		public StreetLocationSelectionMap(Location location)
		{
			InitializeComponent();
			MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(0.444));
			map.MoveToRegion(mapSpan);
			map.Pins.Clear();
			StreetLoc = new Pin
			{
				Position = new Position(location.Latitude, location.Longitude),
				Label = "Picture Location",
				Address = "",
				Type = PinType.SearchResult
			};
			map.Pins.Add(StreetLoc);
		}

		private void SaveLocation_Clicked(object sender, EventArgs e)
		{
			if (StreetLoc != null)
			{
				InsertPicPage.setlocation(new Xamarin.Essentials.Location(StreetLoc.Position.Latitude, StreetLoc.Position.Longitude));
				Navigation.PopAsync();
			}
			else
			{
				DisplayAlert("Error", "Location not selected..Select location first on map", "OK");
			}
		}
		private Pin StreetLoc;

		private void map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
		{
			map.Pins.Clear();
			StreetLoc = new Pin
			{
				Position = e.Position,
				Label = "Picture Location",
				Address = "",
				Type = PinType.SearchResult
			};
			map.Pins.Add(StreetLoc);
		}

		protected override bool OnBackButtonPressed()
		{
			Navigation.PopToRootAsync();
			return base.OnBackButtonPressed();
		}
	}
}