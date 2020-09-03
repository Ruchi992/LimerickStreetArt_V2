using LimerickStreetArtApp.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StreetArtDetailInfo : Rg.Plugins.Popup.Pages.PopupPage
    {
        public StreetArtDetailInfo(StreetArtDTO streetartdto)
        {
            InitializeComponent();
            Entry_Title.Text = streetartdto.Title;
            Entry_Title.Text = streetartdto.Title;
            Entry_Username.Text = streetartdto.UserName;
            Entry_Time.Text = streetartdto.Timestamp.ToLongDateString();
            Entry_Street.Text = streetartdto.Street;
            Entry_Latitude.Text = streetartdto.GpsLatitude.ToString();
            Entry_Longitude.Text = streetartdto.GpsLongitude.ToString();
        }

        private void btn_Close_Clicked(object sender, EventArgs e)
        {
             Navigation.PopPopupAsync();
        }
    }
}