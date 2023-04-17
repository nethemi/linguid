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
                IsFavorite(userLogin);
            }

        }

        private async void IsFavorite(string userLogin)
        {
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
                                addFav.IsVisible = false;
                                delFav.IsVisible= true;
                            }
                        }
                    }

                }
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

        private async void addFavClicked(object sender, EventArgs e)
        {
            Favorite fav = new Favorite();
            fav.fkMeaning = meaning.MeaningID;
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    fav.fkUser = user.UserID;
                    await App.Database.SaveFavoriteAsync(fav);
                }
            }

            addFav.IsVisible = false;
            delFav.IsVisible = true;
        }

        private async void delFavClicked(object sender, EventArgs e)
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    var result = await DisplayAlert("Удаление", $"Удалить {meaning.dictionary.Item} из избранного", "Да", "Нет");
                    if (result)
                    {
                        foreach (var item in await App.Database.GetFavoriteAsync())
                        {
                            if (item.fkMeaning == meaning.MeaningID)
                            {
                                await App.Database.DeleteFavoriteAsync(item);
                                categoryView.ItemsSource = (await App.Database.GetMbCWithChildren()).Where(z => z.fkMeaning == meaning.MeaningID);
                            }
                        }
                    }
                }
            }
          
        }
    }
}