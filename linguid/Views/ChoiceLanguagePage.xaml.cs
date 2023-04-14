using linguid.Views.ContentMainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceLanguagePage : ContentPage
    {
        public ChoiceLanguagePage()
        {
            InitializeComponent();
        }

        private async void EngClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            var user = await App.Database.GetUserAsync();
            foreach (var i in user)
            {
                if (i.UserLogin == userLogin)
                {
                    i.fkLanguage = 1;
                    await App.Database.SaveUserAsync(i);
                }
            }

            var _navigation = Application.Current.MainPage.Navigation;
            await _navigation.PushAsync(new MainPage());
        }

        private async void JpClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            var user = await App.Database.GetUserAsync();
            foreach (var i in user)
            {
                if (i.UserLogin == userLogin)
                {
                    i.fkLanguage = 3;
                    await App.Database.SaveUserAsync(i);
                }
            }

            var _navigation = Application.Current.MainPage.Navigation;
            await _navigation.PushAsync(new MainPage());
        }

        private async void ChinaClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            var user = await App.Database.GetUserAsync();
            foreach (var i in user)
            {
                if (i.UserLogin == userLogin)
                {
                    i.fkLanguage = 2;
                    await App.Database.SaveUserAsync(i);
                }
            }

            var _navigation = Application.Current.MainPage.Navigation;
            await _navigation.PushAsync(new MainPage());
        }
    }
}