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
	public partial class DictionaryPage : ContentPage
	{
		public DictionaryPage ()
		{
			InitializeComponent ();
		}

        private async void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
			if (userLogin != "")
			{
                foreach (var user in await App.Database.GetUserAsync())
                {
                    if (userLogin == user.UserLogin)
                    {
                        recentlyViewed.IsVisible = !recentlyViewed.IsVisible;
                        dictionatyView.ItemsSource = null;
                        if (searchBar.Text != "")
                            dictionatyView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage && (z.dictionary.Item.StartsWith(e.NewTextValue) || z.transcription.TranscriptionItem.StartsWith(e.NewTextValue) || z.dictionaryRu.ItemRu.StartsWith(e.NewTextValue)));
                    }
                }
            }
            else
            {
                recentlyViewed.IsVisible = !recentlyViewed.IsVisible;
                dictionatyView.ItemsSource = null;
                if (searchBar.Text != "")
                    dictionatyView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == 1 && (z.dictionary.Item.StartsWith(e.NewTextValue) || z.transcription.TranscriptionItem.StartsWith(e.NewTextValue) || z.dictionaryRu.ItemRu.StartsWith(e.NewTextValue)));
            }
            
		}

        private async void dictionatyViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			Meaning selected = (Meaning)e.SelectedItem;
			await Navigation.PushAsync(new WordPage(selected));
        }
    }
}