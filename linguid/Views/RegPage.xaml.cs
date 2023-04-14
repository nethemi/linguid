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
    public partial class RegPage : ContentPage
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private async void NextBtnClick(object sender, EventArgs e)
        {
            userName.HasError = false;
            userLogin.HasError = false;
            userPass.HasError = false;
            checkPass.HasError = false;

            bool flagUser = false;

            foreach (var user in await App.Database.GetUserAsync())
            {
                if (user.UserLogin == userLogin.Text)
                {
                   flagUser= true;
                }
            }

            if (string.IsNullOrWhiteSpace(userName.Text))
            {
                userName.HasError = true;
                userName.ErrorText = "Введите имя";
                userName.IsErrorIconVisible = true;
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

            if (string.IsNullOrWhiteSpace(checkPass.Text))
            {
                checkPass.HasError = true;
                checkPass.ErrorText = "Введите пароль";
                checkPass.IsErrorIconVisible = true;
            }


            if (!string.IsNullOrWhiteSpace(userName.Text) && !string.IsNullOrWhiteSpace(userLogin.Text) && !string.IsNullOrWhiteSpace(userPass.Text) && !string.IsNullOrWhiteSpace(checkPass.Text))
            {
                if (flagUser)
                {
                    userLogin.HasError = true;
                    userLogin.ErrorText = "Пользователь с таким логином уже существует!";
                    userLogin.IsErrorIconVisible = true;
                }

                if (checkPass.Text != userPass.Text)
                {
                    checkPass.HasError = true;
                    checkPass.ErrorText = "Пароли не совпадают";
                    checkPass.IsErrorIconVisible = true;
                }
                
                if(!flagUser && checkPass.Text == userPass.Text)
                {
                    await App.Database.SaveUserAsync(new User
                    {
                        UserName = userName.Text,
                        UserLogin = userLogin.Text,
                        UserPassword = userPass.Text,
                        fkLanguage = 1
                    });
                    Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(userLogin.Text), null);

                    userName.Text = userLogin.Text = userPass.Text = checkPass.Text = string.Empty;

                    await Navigation.PushAsync(new ChoiceLanguagePage());
                }  
            }
        }
    }
}