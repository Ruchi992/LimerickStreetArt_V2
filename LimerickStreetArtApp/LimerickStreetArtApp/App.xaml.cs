using LimerickStreetArtApp.Data;
using LimerickStreetArtApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp
{
	public partial class App : Application
	{

		private static UserDatabaseController userDatabase;
		private static RestService restService;

		public static UserDatabaseController UserDatabase
		{
			get { return userDatabase ?? (userDatabase = new UserDatabaseController()); }
		}

		

		public static RestService RestService
		{
			get { return restService ?? (restService = new RestService()); }
		}
		public App()
		{
			System.Console.WriteLine("test \n /n test \n /ntest \n /ntest \n /ntest \n /n");
			InitializeComponent();

			MainPage = (new LoginPage());
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
