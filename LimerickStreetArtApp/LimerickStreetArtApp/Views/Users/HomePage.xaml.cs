using Acr.UserDialogs;
using LimerickStreetArtApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LimerickStreetArtApp.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            try
            {
                InitializeComponent();
                 Init();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async Task Init()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please Wait...");
                DDTO = await App.RestService.GetResponse<List<StreetArtDTO>>(Constants.APIURL + "streetart/GetStreetArtDTO");

                if (DDTO != null)
                {
                    DDTO.ForEach(art =>
                           {
                               try
                               {
                                   art.ImageSource = new UriImageSource { CachingEnabled = true, Uri = new Uri(Constants.PicsFolder + "thumbs/" + art.Image) };
                               }
                               catch (Exception)
                               {

                                   throw;
                               }
                           });
                }
                UserDialogs.Instance.HideLoading();
                if (App.UserDatabase.GetUser().AccessLevel == 1)
                {
                    listStreetArt.ItemTemplate = (DataTemplate)Resources["AdminTemplate"];

                }
                else
                {
                    listStreetArt.ItemTemplate = (DataTemplate)Resources["UserTemplate"];

                }
                listStreetArt.ItemsSource = DDTO;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new InsertPicPage(), true);
        }

        private void listStreetArt_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushModalAsync(new StreetArtDetailPage(e.SelectedItem as StreetArtDTO), true);
                listStreetArt.SelectedItem = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private async void listStreetArt_Refreshing(object sender, EventArgs e)
        {
            await Init();
            listStreetArt.EndRefresh();
        }

        private async void btn_delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please Wait...");
                var button = sender as Button;
                var art = button.BindingContext as StreetArtDTO;
                var response=await App.RestService.GetResponse_HTTPRESPONSE(Constants.APIURL + "streetart/Delete?id="+art.Id);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Street Art deleted", "OK");

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");

                }
                UserDialogs.Instance.HideLoading();
                await Init();


            }
            catch(Exception ex)
            {

            }
        }

        public List<StreetArtDTO> TempDDTO;
        public List<StreetArtDTO> DDTO;
        private void searchlist_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (searchlist.Text != "")
                {
                    TempDDTO = DDTO.Where(x =>
                    {
                        var a = x.Title.ToLower() ?? "";
                        var b = x.UserName.ToLower() ?? "";
                        var c = x.Street.ToLower() ?? "";
                       

                        if (a.ToString().Contains(searchlist.Text.ToLower()) ||
                        b.ToString().Contains(searchlist.Text.ToLower()) ||                       
                        c.ToString().Contains(searchlist.Text.ToLower())       )
                            return true;
                        else
                            return false;
                    }).ToList();
                    listStreetArt.ItemsSource = TempDDTO;
                }
                else
                    listStreetArt.ItemsSource = DDTO;
            }
            catch (Exception ex)
            {


            }
        }
    }
}