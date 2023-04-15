using SQLitePCL;
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
    public partial class WordPage : ContentPage
    {
        Meaning meaning = new Meaning();
        public WordPage(Meaning selected)
        {
            InitializeComponent();
            meaning = selected;

            item.Text = selected.dictionary.Item;
            itemRu.Text = selected.dictionaryRu.ItemRu;
            transcription.Text = selected.transcription.TranscriptionItem;

            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin == "")
            {
                addFav.IsVisible = false;
                delFav.IsEnabled = false;
            }
            else
            {
                addFav.IsVisible = true;
            }
        }

        protected override async void OnAppearing()
        {
            await App.Database.CreateMbC();
            await App.Database.CreateMeaning();

            categoryView.ItemsSource = (await App.Database.GetMbCWithChildren()).Where(z => z.fkMeaning == meaning.MeaningID);

            base.OnAppearing();
        }

        private async void categoryViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin == "")
            {
                categoryView.IsEnabled = false;
            }
            else
            {
                MeaningByCategory selected = (MeaningByCategory)e.SelectedItem;
                Category category = selected.category;
                await Navigation.PushAsync(new StartLessonPage(category));
            }      
        }
    }
}