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
            await App.Database.CreateMeaning();
            // привязка данных
            dictionaryView.ItemsSource = await App.Database.GetMeaningAsync();

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var meanings = item.CommandParameter as Meaning;
            var result = await DisplayAlert("Delete", $"Delete {meanings.MeaningID} from the db", "yes", "no");
            if (result)
            {
                await App.Database.DeleteMeaningAsync(meanings);
                dictionaryView.ItemsSource = await App.Database.GetMeaningAsync();
            }
        }
    }
}