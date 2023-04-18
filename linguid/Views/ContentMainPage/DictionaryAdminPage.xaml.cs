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
    public partial class DictionaryAdminPage : ContentPage
    {
        public DictionaryAdminPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            dictionatyView.IsVisible = false;
            recentlyViewed.IsVisible = true;
            recentlyView.IsVisible = true;
            await App.Database.CreateFavorite();
            await App.Database.CreateMeaning();
            var userLogin = Thread.CurrentPrincipal.Identity.Name;

                foreach (var user in await App.Database.GetUserAsync())
                {
                if (userLogin == user.UserLogin)
                {
                    var history = (await App.Database.GetHistoryWithChildren()).Where(z => z.fkUser == user.UserID).OrderByDescending(x => x.Date);
                    var meaning = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage);
                    List<Meaning> listMeans = new List<Meaning>();

                    foreach (var item in history)
                    {
                        foreach (var item2 in meaning)
                        {
                            if (item.fkMeaning == item2.MeaningID)
                            {
                                listMeans.Add(item2);
                            }
                        }
                    }
                    recentlyView.ItemsSource = listMeans;
                }
            }
            base.OnAppearing();
        }
        private async void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            recentlyViewed.IsVisible = false;
            recentlyView.IsVisible = false;
            dictionatyView.IsVisible = true;
            dictionatyView.ItemsSource = null;
            var userLogin = Thread.CurrentPrincipal.Identity.Name;

            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    if (searchBar.Text != "")
                    {
                        dictionatyView.ItemsSource = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage && (z.dictionary.Item.StartsWith(e.NewTextValue) || z.transcription.TranscriptionItem.StartsWith(e.NewTextValue) || z.dictionaryRu.ItemRu.StartsWith(e.NewTextValue)));
                    }

                }
            }


            if (searchBar.Text == "")
            {
                dictionatyView.IsVisible = false;
                recentlyViewed.IsVisible = true;
                recentlyView.IsVisible = true;
            }

        }

        private async void dictionatyViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            dictionatyView.IsVisible = true;
            recentlyView.IsVisible = false;
            Meaning selected = (Meaning)e.SelectedItem;
            HistoryMeaning history = new HistoryMeaning();

            var userLogin = Thread.CurrentPrincipal.Identity.Name;

            foreach (var user in await App.Database.GetUserAsync())
            {
                if (userLogin == user.UserLogin)
                {
                    history.fkMeaning = selected.MeaningID;
                    history.fkUser = user.UserID;
                    history.Date = DateTime.Now;
                    await App.Database.SaveHistoryAsync(history);
                }
            }
            await Navigation.PushAsync(new WordPage(selected));
        }

        private async void editBtnClicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var meaning = item.CommandParameter as Meaning;
            await Navigation.PushAsync(new AddDataPage(meaning));
        }

        private async void delBtnClicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var meaning = item.CommandParameter as Meaning;
            meaning = await App.Database.GetMeaningWith(meaning.MeaningID);
            var result = await DisplayAlert("Удаление", $"Удалить {meaning.dictionary.Item} из базы данных", "Да", "Нет");
            if (result)
            {
                await App.Database.DeleteMeaning(meaning);
                await DisplayAlert("Удаление", $"Значение успешно удалено из базы данных", "ОК");
                OnAppearing();
            }
        }

        private async void recentlyViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            dictionatyView.IsVisible = false;
            recentlyView.IsVisible = true;
            recentlyViewed.IsVisible = true;
            Meaning selected = (Meaning)e.SelectedItem;
            await Navigation.PushAsync(new WordPage(selected));
        }
    }
}