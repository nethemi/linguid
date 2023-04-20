using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeNamePage : ContentPage
	{
		public ChangeNamePage ()
		{
			InitializeComponent ();
		}

        private async void ChangeClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
			foreach (var user in await App.Database.GetUserAsync())
			{
                userPass.HasError = false;
                if (user.UserLogin == userLogin && userPass.Text == user.UserPassword)
                {
                    user.UserName = newName.Text;
                    await App.Database.SaveUserAsync(user);
                    await DisplayAlert("Изменение имени", $"Имя успешно изменено!", "ОК");
                    await Navigation.PushAsync(new ProfilePage());
                }
                else if (userPass.Text != user.UserPassword)
                {
                    userPass.HasError = true;
                    userPass.ErrorText = "Непраивльный пароль!";

                }
            }
        }
    }
}