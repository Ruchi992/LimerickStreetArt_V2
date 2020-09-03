using Acr.UserDialogs;
using LimerickStreetArtApp.Data;
using LimerickStreetArtApp.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InsertPicPage : ContentPage
	{
		private MediaFile pic;
		DateTime picDate;
		public InsertPicPage()
		{
			InitializeComponent();
			location = null;

		}

		private async void btnSelectPic_Clicked(object sender, EventArgs e)
		{
			try
			{
				pic = null;
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					await App.Current.MainPage.DisplayAlert("PickPhotoError", "pick photo not available", "OK");
					return;
				}

				pic = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { SaveMetaData = true });
				if (pic == null)
					return;
				picDate = DateTime.Now; ;

				await Navigation.PushAsync(new StreetLocationSelectionMap());
				img_Art.Source = ImageSource.FromFile(pic.Path);

			}
			catch (Exception ex)
			{


			}

		}


		private async void btnCapturePic_Clicked(object sender, EventArgs e)
		{
			try
			{
				pic = null;
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					await App.Current.MainPage.DisplayAlert("PickPhotoError", "pick photo not available", "OK");
					return;
				}

				pic = (await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
				{
					Directory = "StreetArt",
					Name = System.IO.Path.GetRandomFileName() + ".png",
					SaveMetaData = true
				}));
				if (pic == null)
					return;
				await GetCurrentLocation(); //here the location is being taken

				picDate = DateTime.Now;
				img_Art.Source = ImageSource.FromFile(pic.Path);

			}
			catch (Exception ex)
			{


			}

		}

		private async void btn_Upload_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (pic == null || string.IsNullOrWhiteSpace(Entry_Title.Text) || string.IsNullOrWhiteSpace(Entry_Street.Text))
				{
					await DisplayAlert("Error", "title or street values empty", "OK");
				}
				else if (location == null)
				{
					await DisplayAlert("Error", "Location is empty", "OK");
				}
				else
				{
					var user = App.UserDatabase.GetUser();
					(sender as Button).IsEnabled = false;
					UserDialogs.Instance.ShowLoading("Please Wait...");
					var Imagepath = $"A_{user.Id}";


					var streetart = new StreetArtDTO();
					streetart.GpsLatitude = (float)location.Latitude;
					streetart.GpsLongitude = (float)location.Longitude;
					streetart.Title = Entry_Title.Text;
					streetart.Street = Entry_Street.Text;
					streetart.Image = Imagepath;
					streetart.UserAccountId = user.Id;
					streetart.Timestamp = picDate;

					var content = new MultipartFormDataContent();

					content.Add(new StreamContent(pic.GetStream()),
							"\"file_art\"",
							Imagepath);

					content.Add(new StringContent(JsonConvert.SerializeObject(streetart), Encoding.UTF8, "application/json"), "data");

					var httpClient = new HttpClient();

					var uploadServiceBaseAddress = Constants.APIURL + "streetart/upload";

					var response = await httpClient.PostAsync(uploadServiceBaseAddress, content);
					if (response.StatusCode == System.Net.HttpStatusCode.Created)
					{
						try
						{
							string jsonResult = await response.Content.ReadAsStringAsync();

						}
						catch (Exception eex)
						{

						}
						UserDialogs.Instance.HideLoading();
						await DisplayAlert("Success", "Picture added", "OK");
						var test = this.Parent;
						var tab = this.Parent as TabbedPage;
						tab.CurrentPage = tab.Children[0];


					}
					else
					{
						string jsonResult = await response.Content.ReadAsStringAsync();

						var aaa = JsonConvert.DeserializeObject<Exception>(jsonResult);
						UserDialogs.Instance.HideLoading();

						await DisplayAlert("Error", "upload failed", "OK");



					}
				}




			}
			catch (Exception ex)
			{
				UserDialogs.Instance.HideLoading();
				if (ex.GetType() == typeof(FeatureNotEnabledException))
				{
					await App.Current.MainPage.DisplayAlert("Location", "Kindly Turn On Location In Setting Of Your Phone and try again", "OK");

				}
				else
				if (ex.GetType() == typeof(FeatureNotSupportedException))
				{
					await App.Current.MainPage.DisplayAlert("Error", "FeatureNotSupported", "OK");

				}
				else
				if (ex.GetType() == typeof(PermissionException))
				{
					await App.Current.MainPage.DisplayAlert("Location", "Kindly Give The App ThePermision Of Locaiton and try again", "OK");
					await Xamarin.Essentials.Permissions.RequestAsync<Permissions.LocationWhenInUse>();

				}
				else
				{
					await DisplayAlert("Error", "upload failed", "OK");

				}


			}
			location = null;
			(sender as Button).IsEnabled = true;

		}



		static Xamarin.Essentials.Location location = null;

		public static async Task GetCurrentLocation()
		{
			location = new Xamarin.Essentials.Location();
			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Medium, new TimeSpan(0, 1, 0));
				location = await Geolocation.GetLocationAsync(request); // here it is requesting the location from the mobile 


			}
			catch (FeatureNotSupportedException fnsEx)
			{
				throw new FeatureNotSupportedException();
			}

			catch (FeatureNotEnabledException fneEx)
			{
				throw new FeatureNotEnabledException();

			}
			catch (PermissionException pEx)
			{
				throw new PermissionException("");

			}
			catch (Exception e)
			{

			}

		}

		public static void setlocation(Xamarin.Essentials.Location location1)
		{
			location = location1;
		}

		public void btn_Map_Clicked(object sender, EventArgs e)
		{
			if (location != null)
			{
				Navigation.PushAsync(new StreetLocationSelectionMap(location));

			}
			else
				Navigation.PushAsync(new StreetLocationSelectionMap());
		}

		private void btn_Map_Clicked_1(object sender, EventArgs e)
		{
			if (location != null)
			{
				Navigation.PushAsync(new StreetLocationSelectionMap(location));

			}
			else
				Navigation.PushAsync(new StreetLocationSelectionMap());
		}
	}
}