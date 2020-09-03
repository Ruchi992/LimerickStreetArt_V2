using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LimerickStreetArtApp.Models
{
	class Constants
	{
		public static string APIURL = "http://192.168.0.7:51606/api/";

		public static string PicsFolder = "http://192.168.0.7:51606/Pics/";
	}


	public class UserAccount
	{

		// public int Id { get; set; }
		[PrimaryKey]
		public string Username { get; set; }

		public string Password { get; set; }
		public int Id { get; set; }

		public String Email
		{
			get;
			set;
		}

		public bool Active
		{
			get;
			set;
		}
		public DateTime DateOfBirth
		{
			get;
			set;
		}
		public int AccessLevel
		{
			get;
			set;
		}

		public UserAccount()
		{

		}
		public UserAccount(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public UserAccount(string username, string password, int id, string email, bool active, DateTime dateOfBirth, int accessLevel) : this(username, password)
		{
			Id = id;
			Email = email;
			Active = active;
			DateOfBirth = dateOfBirth;
			AccessLevel = accessLevel;
		}

		public bool CheckInformation()
		{
			if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
				return false;

			return true;
		}
	}

	public static class AppSettings
	{
		public const string AppName = "App Name";
		public static Color Customer_Barcolor = Color.FromHex("#021B3A");


	}

	public class RegistrationModel
	{



		public String Password { get; set; }

		public String ReconformPassword { get; set; }
		public String Email { get; set; }



		public String Username { get; set; }

		public DateTime dateOfBirth { get; set; }

		public RegistrationModel(string password, string reconformPassword, string email, string username, DateTime dateOfBirth)
		{
			Password = password;
			ReconformPassword = reconformPassword;
			Email = email;
			Username = username;
			dateOfBirth = dateOfBirth;
		}


		internal bool CheckInformation()
		{
			if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)
				|| string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(ReconformPassword)
				 )
				return false;

			return true;
		}
		internal bool CheckPass()
		{
			if (Password != ReconformPassword)
				return false;

			return true;
		}
	}



}
