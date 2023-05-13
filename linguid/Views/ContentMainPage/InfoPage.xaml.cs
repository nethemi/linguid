using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        List<int> count = new List<int>() { 3, 3, 3, 4, 4, 2, 2, 5, 3, 2, 2, 4};
        public InfoPage()
        {
            InitializeComponent();

            db.Text = App.DATABASE_NAME;
            countView.ItemsSource = count;
            
        }

        protected override async void OnAppearing()
        {
            tableView.ItemsSource = await App.database.GetAllTablesAsync();
            base .OnAppearing();    
        }
    }
}