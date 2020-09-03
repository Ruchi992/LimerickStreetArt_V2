using LimerickStreetArtApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerTabbedPage : Xamarin.Forms.TabbedPage
    {
        public CustomerTabbedPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            CreateTabs();
        }


        private void CreateTabs()
        {
            try
            {
                
               

                //Pages : About , Contact Us , Log off , 

                BarBackgroundColor = AppSettings.Customer_Barcolor;
                SelectedTabColor = Color.White;
                BarTextColor = Color.White;
                UnselectedTabColor = Color.FromHex("#B7C3BC");

                var HomePage = new HomePage
                {
                    IconImageSource = ImageSource.FromResource("LimerickStreetArtApp.Image.Picture.png"),
                    Title = "Home",
                    
                };
                var UploadArt = new InsertPicPage
                {
                    IconImageSource = ImageSource.FromResource("LimerickStreetArtApp.Image.Upload-02-WF.png"),
                    Title = "Upload",
                };




                this.Children.Add(HomePage);
                this.Children.Add(UploadArt);
                this.CurrentPage = HomePage;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}