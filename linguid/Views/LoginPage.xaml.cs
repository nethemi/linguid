using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void SkipBtnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            //Remove last page
            Navigation.RemovePage(this);
        }

        private async void RegBtnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            userLogin.HasError = false;
            userPass.HasError = false;

            bool validUser = false;
            bool validPass = false;

            foreach (var user in await App.Database.GetUserAsync())
            {
                if (user.UserLogin == userLogin.Text)
                {
                    validUser= true;
                }

                if (user.UserPassword == userPass.Text) validPass= true;
            }


            if (string.IsNullOrWhiteSpace(userLogin.Text))
            {
                userLogin.HasError = true;
                userLogin.ErrorText = "Введите логин";
                userLogin.IsErrorIconVisible = true;
            }

            if (string.IsNullOrWhiteSpace(userPass.Text))
            {
                userPass.HasError = true;
                userPass.ErrorText = "Введите пароль";
                userPass.IsErrorIconVisible = true;
            }

            if (!string.IsNullOrWhiteSpace(userLogin.Text) && !string.IsNullOrWhiteSpace(userPass.Text))
            {
                if (validUser && validPass)
                {
                    userLogin.HasError = false;
                    userPass.HasError = false;
                    Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(userLogin.Text), null);

                    await Navigation.PushAsync(new MainPage());
                    Navigation.RemovePage(this);
                }

                if (!validUser) {
                    userLogin.HasError = true;
                    userLogin.ErrorText = "Такого пользователя не существует";
                    userLogin.IsErrorIconVisible = true;
                }

                if (validUser && !validPass)
                {
                    userPass.HasError = true;
                    userPass.ErrorText = "Неверный пароль";
                    userPass.IsErrorIconVisible = true;
                }
              
            }
        }

    }    
}