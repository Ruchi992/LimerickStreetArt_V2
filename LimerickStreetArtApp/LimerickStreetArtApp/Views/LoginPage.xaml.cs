using Acr.UserDialogs;
using LimerickStreetArt;
using LimerickStreetArtApp.Models;
using LimerickStreetArtApp.Views.Users.Menu;
using Newtonsoft.Json;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static LimerickStreetArtApp.Models.RegistrationModel;

namespace LimerickStreetArtApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}
        async void Init()
        {
            // BackgroundColor = Constants.BackgroundColor;
            //Lbl_Username.TextColor = Constants.MainTextColor;
            //Lbl_Password.TextColor = Constants.MainTextColor;



            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(Btn_SignIn, e);
            this.IsEnabled = true;


        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            try
            {
                (sender as Button).IsEnabled = false;
                UserDialogs.Instance.ShowLoading("Please Wait...");
                this.IsEnabled = false;

                UserAccount user = new UserAccount(Entry_Username.Text, Entry_Password.Text);


                if (user.CheckInformation())
                {
                    var loginResponse = await App.RestService.PostResponse_HTTPRESPONSE(Constants.APIURL+"useraccount/login", JsonConvert.SerializeObject(user));
                    if (loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonResult = await loginResponse.Content.ReadAsStringAsync();

                        var userAccount= JsonConvert.DeserializeObject<UserAccount>(jsonResult);
                        App.UserDatabase.SaveUser(userAccount);
                        Application.Current.MainPage = new NavigationPage(new CustomerTabbedPage());
                    }
                    else
                    {
                        await DisplayAlert("Login Failed", "Login credentials not correct or something went wrong", "Ok");
                        UserDialogs.Instance.HideLoading();

                        (sender as Button).IsEnabled = true;
                        this.IsEnabled = true;
                    }



                }
                else
                {
                    await DisplayAlert("Login Failed", "Entered value not valid", "Ok");
                    try
                    {
                        UserDialogs.Instance.HideLoading();

                    }
                    catch (Exception ex)
                    {

                    }
                    (sender as Button).IsEnabled = true;
                    this.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed", "Something went wrong check your connection", "Ok");

                try
                {
                    UserDialogs.Instance.HideLoading();

                }
                catch (Exception eex)
                {

                }

            }
            (sender as Button).IsEnabled = true;
            this.IsEnabled = true;
        }

        public async void Btn_Register_Clicked(object sender, EventArgs e)
        {



            clm_Email.IsVisible = true;
            clm_DOB.IsVisible = true;
            clm_Pass1.IsVisible = true;
            Btn_SignIn.IsVisible = false;
            Btn_Register.IsVisible = false;
            Btn_CreateAccount.IsVisible = true;

        }





        private async void Btn_CreateAccount_Clicked(object sender, EventArgs e)
        {
            try
            {
                (sender as Button).IsEnabled = false;
                UserDialogs.Instance.ShowLoading("Please Wait...");
                this.IsEnabled = false;

                RegistrationModel user = new RegistrationModel(Entry_Password.Text, Entry_Password1.Text, Entry_Email.Text, Entry_Username.Text, Entry_DOB.Date);


                if (user.CheckInformation() && user.CheckPass())
                {
                    var loginResponse = await App.RestService.PostResponse_HTTPRESPONSE(Constants.APIURL + "useraccount/Register", JsonConvert.SerializeObject(user));
                    if (loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        UserAccount newUser = new UserAccount(user.Username, user.Password);
                        App.UserDatabase.SaveUser(newUser);
                        Application.Current.MainPage = new NavigationPage(new CustomerTabbedPage());
                    }
                    else
                    {
                        await DisplayAlert("Login Failed", "Login credentials not correct or something went wrong", "Ok");
                        UserDialogs.Instance.HideLoading();

                        (sender as Button).IsEnabled = true;
                        this.IsEnabled = true;
                    }



                }
                else
                {
                    await DisplayAlert("Login Failed", "Entered value not valid", "Ok");
                    try
                    {
                        UserDialogs.Instance.HideLoading();

                    }
                    catch (Exception ex)
                    {

                    }
                    (sender as Button).IsEnabled = true;
                    this.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed", "Something went wrong check your connection", "Ok");

                try
                {
                    UserDialogs.Instance.HideLoading();

                }
                catch (Exception eex)
                {

                }

            }
            (sender as Button).IsEnabled = true;
            this.IsEnabled = true;

            clm_Email.IsVisible = false;
            clm_DOB.IsVisible = false;
            clm_Pass1.IsVisible = false;
            Btn_SignIn.IsVisible = true;
            Btn_Register.IsVisible = true;
            Btn_CreateAccount.IsVisible = false;

        }
    }
}