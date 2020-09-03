using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimerickStreetArt.Web.Models
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
		public int UserID { get; set; }
	}
}
