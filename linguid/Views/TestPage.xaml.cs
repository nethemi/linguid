using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateUser();
            // привязка данных
            dictionaryView.ItemsSource = await App.Database.GetUserAsync();

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var meanings = item.CommandParameter as User;
            var result = await DisplayAlert("Delete", $"Delete {meanings.UserName} from the db", "yes", "no");
            if (result)
            {
                await App.Database.DeleteUserAsync(meanings);
                dictionaryView.ItemsSource = await App.Database.GetUserAsync();
            }
        }
    }
}