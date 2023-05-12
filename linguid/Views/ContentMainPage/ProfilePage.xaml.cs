using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin == "")
            {
                noUser.IsVisible = true;
                profileInfo.IsVisible=false;
            }
            else
            {
                GetUserInfo();
            }
        }

        public async void GetUserInfo()
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            loginUser.Text = userLogin;

            foreach (var user in await App.Database.GetUserAsync())
            {
                int idUser;
                if (user.UserLogin == userLogin)
                {
                    userName.Text = user.UserName;
                    idUser = user.UserID;
                    foreach (var ubr in await App.Database.GetUbRAsync())
                    {
                        if (ubr.fkUser == idUser)
                        {
                            if (ubr.fkRole == 1) addBtn.IsVisible = true;
                        }
                    }
                }         
            }
        }

        private async void ChangePasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }

        private async void ChangeNameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeNamePage());
        }

        private async void HistoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryLessonPage());
        }


        private async void ChoiceClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoiceLanguagePage());
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDataPage(null));
        }

        private async void ExitClicked(object sender, EventArgs e)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(""), null);
            var _navigation = Application.Current.MainPage.Navigation;
            var _lastPage = _navigation.NavigationStack.LastOrDefault();
            await _navigation.PushAsync(new LoginPage());
            _navigation.RemovePage(_lastPage);
        }

        private async void RegClick (object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            await _navigation.PushAsync(new RegPage());
        }

        private async void infoBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

        private async void timerBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimerPage());
        }
    }
}