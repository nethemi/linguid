using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDataPage : ContentPage
    {
        public AddDataPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateLanguage();
            // привязка данных
            choiceLang.ItemsSource = await App.Database.GetLanguageAsync();

            base.OnAppearing();
        }

        private void ButtonClicked(object sender, System.EventArgs e)
        {

        }
    }
}