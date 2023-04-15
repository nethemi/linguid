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
	public partial class LessonPage : ContentPage
	{
		public LessonPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateCategory();
            // привязка данных
            categoryView.ItemsSource = await App.Database.GetCategoryAsync();

            base.OnAppearing();
        }

        private async void categoryViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new StartLessonPage());
        }
    }
}