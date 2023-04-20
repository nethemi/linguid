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
	public partial class ChangePasswordPage : ContentPage
	{
		public ChangePasswordPage ()
		{
			InitializeComponent ();
		}

        private async void ChangeClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            foreach (var user in await App.Database.GetUserAsync())
            {
                userPass.HasError = false;
                checkPass.HasError = false;
                if (user.UserLogin == userLogin && userPass.Text == user.UserPassword && checkPass.Text == newPass.Text)
                {
                    user.UserPassword= newPass.Text;
                    await App.Database.SaveUserAsync(user);
                    await DisplayAlert("Изменение пароля", $"Пароль успешно изменен!", "ОК");
                    await Navigation.PushAsync(new ProfilePage());
                }
                else
                {
                    if (userPass.Text != user.UserPassword)
                    {
                        userPass.HasError = true;
                        userPass.ErrorText = "Непраивльный пароль!";
                    }
                    if (checkPass.Text != newPass.Text)
                    {
                        checkPass.HasError= true;
                        checkPass.ErrorText = "Пароли не совпадают!";
                    }
                }
            }
        }
    }
}