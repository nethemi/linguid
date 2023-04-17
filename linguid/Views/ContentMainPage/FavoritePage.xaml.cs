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
	public partial class FavoritePage : ContentPage
	{
		public FavoritePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            await App.Database.CreateFavorite();

            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    var favorite = (await App.Database.GetFavoriteWithChildren()).Where(z => z.fkUser == user.UserID);
                    var meaning = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage);
                    List<Meaning> listMeans = new List<Meaning>();
                    foreach (var item in favorite)
                    {
                        foreach (var item2 in meaning)
                        {
                            if (item.fkMeaning == item2.MeaningID)
                            {
                                listMeans.Add(item2);
                            }
                        }
                    }
                    dictionaryView.ItemsSource = listMeans;

                }
            }

            base.OnAppearing();
        }

        private async void dictionatyViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Meaning selected = (Meaning)e.SelectedItem;
            await Navigation.PushAsync(new WordPage(selected));
        }
    }
}