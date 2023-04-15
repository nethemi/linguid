using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		public StartLessonPage(Category selected)
		{
			InitializeComponent ();
            _category = selected;
            lesson.Text = _category.CategoryName;
        }

        protected override async void OnAppearing()
        {
            await App.Database.CreateMbC();
            await App.Database.CreateMeaning();

            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    dictionaryView.ItemsSource = (await App.Database.GetMbCWithChildren()).Where(z=>z.fkCategory==_category.CategoryID);
                }
            }

            


            base.OnAppearing();
        }
    }
}