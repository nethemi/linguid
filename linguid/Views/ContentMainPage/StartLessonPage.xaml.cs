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
	public partial class StartLessonPage : ContentPage
	{
        Category _category = new Category();
		public StartLessonPage()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            await App.Database.CreateMbC();

            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            var user = await App.Database.GetUserAsync(userLogin);
            dictionaryView.ItemsSource = await App.Database.GetMbCAsync();


            base.OnAppearing();
        }
    }
}