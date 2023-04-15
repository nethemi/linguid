using DevExpress.XamarinForms.Core.Themes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

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
            await App.Database.CreateMeaning();

            //dictionaryView.ItemsSource = await App.Database.GetMeaningWithChildren();
            dictionaryView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == 2);


            base.OnAppearing();
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    var item = sender as Button;
        //    var meanings = item.CommandParameter as Meaning;
        //    var result = await DisplayAlert("Delete", $"Delete {meanings.dictionary.Item} from the db", "yes", "no");
        //    if (result)
        //    {
        //        await App.Database.DeleteMeaningAsync(meanings);
        //        dictionaryView.ItemsSource = await App.Database.GetMeaningAsync();
        //    }
        //}
    }
}