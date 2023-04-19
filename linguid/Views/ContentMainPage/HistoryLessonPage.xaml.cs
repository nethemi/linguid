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
	public partial class HistoryLessonPage : ContentPage
	{
		public HistoryLessonPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin != "")
            {
                foreach (var user in await App.Database.GetUserAsync())
                {
                    if (userLogin == user.UserLogin)
                    {
                        var lesson = (await App.Database.GetLessonWithChildren()).Where(z => z.fkUser == user.UserID).OrderByDescending(x => x.Date);
                        var mbc = await App.Database.GetMbCWithChildren();
                        List<MeaningByCategory> byCategories = new List<MeaningByCategory>();
                        foreach (var l in lesson)
                        {
                            foreach (var m in mbc)
                            {
                                if (l.fkMbC == m.MbCID)
                                {
                                    byCategories.Add(m);
                                }
                            }
                        }

                        lessonView.ItemsSource = byCategories;
                        dateView.ItemsSource = lesson;
                    }
                }
            }

            base.OnAppearing();
        }

    }
}