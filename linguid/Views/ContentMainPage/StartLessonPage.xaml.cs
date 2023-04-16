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
                    var mbc = (await App.Database.GetMbCWithChildren()).Where(z => z.fkCategory == _category.CategoryID).Select(x => x.fkMeaning);
                    foreach( var child in mbc)
                    {

                        dictionaryView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage && z.MeaningID == child).ToList();
                    }
                }
            }

            


            base.OnAppearing();
        }
    }
}