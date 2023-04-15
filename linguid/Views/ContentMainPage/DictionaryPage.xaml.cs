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
	public partial class DictionaryPage : ContentPage
	{
		public DictionaryPage ()
		{
			InitializeComponent ();
		}

        private async void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
			recentlyViewed.IsVisible = !recentlyViewed.IsVisible;
			dictionatyView.ItemsSource = null;
			if (searchBar.Text != "")
				dictionatyView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == 1 && (z.dictionary.Item.StartsWith(e.NewTextValue) || z.transcription.TranscriptionItem.StartsWith(e.NewTextValue) || z.dictionaryRu.ItemRu.StartsWith(e.NewTextValue)));
		}
    }
}