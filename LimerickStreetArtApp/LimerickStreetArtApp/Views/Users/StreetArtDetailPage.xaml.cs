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
    public partial class StreetArtDetailPage : ContentPage
    {
        public StreetArtDetailPage(StreetArtDTO streetartdto)
        {
            InitializeComponent();
            
            StreetArtDTO = streetartdto;

           
            img_Art.Source = (new UriImageSource { CachingEnabled = true, Uri = new Uri(Constants.PicsFolder + streetartdto.Image) });
        }
        StreetArtDTO StreetArtDTO;
        private async void btn_map_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new StreetArtLocation(StreetArtDTO.GpsLongitude,StreetArtDTO.GpsLatitude));
        }

        private void btn_close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        private void btn_details_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new StreetArtDetailInfo(StreetArtDTO));
        }
    }
}