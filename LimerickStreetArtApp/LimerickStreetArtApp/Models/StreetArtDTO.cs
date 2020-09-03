using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LimerickStreetArtApp.Models
{
    public class StreetArtDTO
    {
		public int Id { get; set; }
		public float GpsLatitude
		{
			get;
			set;
		}
		public float GpsLongitude
		{
			get;
			set;
		}
		public String Title
		{
			get;
			set;
		}
		public String Street
		{
			get;
			set;
		}
		public DateTime Timestamp
		{
			get;
			set;
		}
		public String Image { get; set; }
		public string UserName { get; set; }
		public int UserAccountId { get; set; }

		public ImageSource ImageSource { get; set; }
	}
}
