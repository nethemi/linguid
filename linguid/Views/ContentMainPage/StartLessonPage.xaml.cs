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
        private int index = 0;
        private List<Meaning> listMeans = new List<Meaning>();
        private List<int> indexListMeans;
        public StartLessonPage(Category selected)
        {
            InitializeComponent();
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
                    var mbc = (await App.Database.GetMbCWithChildren()).Where(z => z.fkCategory == _category.CategoryID);
                    var meaning = (await App.Database.GetMeaningWithChildren()).Where(z => z.dictionary.fkLanguage == user.fkLanguage);

                    foreach (var item in mbc)
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

        private async void dictionaryViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Meaning meaning = (Meaning)e.SelectedItem;
            await Navigation.PushAsync(new WordPage(meaning));
        }

        private void RandomTranscriptionBtn(ref List<int> indexListMeans, ref List<Meaning> listMeans)
        {
            Random rnd = new Random();
            int randIndex = 0;
            int randomBtn = rnd.Next(1, 4);
            if (index < listMeans.Count()-1)
            {
                switch (randomBtn)
                {
                    case 1:
                        translate_one.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_two.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_three.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        break;
                    case 2:
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_one.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        translate_two.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_three.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        break;
                    case 3:
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_one.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(0, indexListMeans.Count);
                        translate_two.Text = listMeans.Where(x => randIndex != index).ElementAt(randIndex).dictionaryRu.ItemRu;
                        translate_three.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        break;
                }
            }
            else index = rnd.Next(0, indexListMeans.Count);

        }
        private void RandomWords(ref List<int> indexListMeans, ref List<Meaning> listMeans)
        {
            indexListMeans = new List<int>();
            for (int i = 0; i < listMeans.Count; i++)
            {
                indexListMeans.Add(i);
            }

            Random rnd = new Random();

            if(listMeans.Count == 0)
            {
                lessonCompleted.IsVisible = true;
            }

            index = rnd.Next(0, indexListMeans.Count);
            if (indexListMeans.Contains(index))
            {
                word.Text = listMeans.ElementAt(index).dictionary.Item;
                transcription.Text = listMeans.ElementAt(index).transcription.TranscriptionItem;


                RandomTranscriptionBtn(ref indexListMeans, ref listMeans);
            }
            else index = rnd.Next(0, indexListMeans.Count);
        }

        private void StartLessonBtnClicked(object sender, EventArgs e)
        {
            dictionaryView.IsVisible = false;
            startLessonBtn.IsVisible = false;
            lesson.IsVisible = false;
            lessonContent.IsVisible = true;

            translate_one.IsEnabled = true;
            translate_two.IsEnabled = true;
            translate_three.IsEnabled = true;


            RandomWords(ref indexListMeans, ref listMeans);
            

            //if (translate_one.IsChecked == true)
            //{
            //    if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_one.Content)
            //    {
            //        translate_one.BackgroundColor = Color.Green;
            //    }
            //    else translate_one.BackgroundColor = Color.Red;
            //}


            //if (translate_two.IsPressed == true)
            //{
            //    if (listMeans.Where(x => x.dictionaryRu.ItemRu == translate_two.Text).First() != null)
            //    {
            //        translate_two.BackgroundColor = Color.Green;
            //    }
            //    else translate_two.BackgroundColor = Color.Red;
            //}
            //if (translate_three.IsPressed == true)
            //{
            //    if (listMeans.Where(x => x.dictionaryRu.ItemRu == translate_three.Text).First() != null)
            //    {
            //        translate_three.BackgroundColor = Color.Green;
            //    }
            //    else translate_three.BackgroundColor = Color.Red;
            //}


        }
        private async void TranslateOneClicked(object sender, EventArgs e)
        {
            translate_two.IsEnabled = false;
            translate_three.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_one.Text)
            {
                translate_one.Background = Color.LightGreen;
            }
            else translate_one.Background = Color.Red;


            Random rnd = new Random();

            listMeans.RemoveAt(index);
            indexListMeans.RemoveAt(index);
            index = rnd.Next(0, indexListMeans.Count);
            await Task.Delay(1000);
            translate_two.IsEnabled = true;
            translate_three.IsEnabled = true;
            translate_one.Background = Color.FromRgb(241, 243, 244);
            RandomWords(ref indexListMeans, ref listMeans);
        }

        private async void TranslateTwoClicked(object sender, EventArgs e)
        {
            translate_one.IsEnabled = false;
            translate_three.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_two.Text)
            {
                translate_two.Background = Color.LightGreen;
            }
            else translate_two.Background = Color.Red;
            Random rnd = new Random();

            listMeans.RemoveAt(index);
            indexListMeans.RemoveAt(index);
            index = rnd.Next(0, indexListMeans.Count);
            await Task.Delay(1000);
            translate_one.IsEnabled = true;
            translate_three.IsEnabled = true;
            translate_two.Background = Color.FromRgb(241, 243, 244);
            RandomWords(ref indexListMeans, ref listMeans);
        }

        private async void TranslateThreeClicked(object sender, EventArgs e)
        {
            translate_one.IsEnabled = false;
            translate_two.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_three.Text)
            {
                translate_three.Background = Color.LightGreen;
            }
            else translate_three.Background = Color.Red;

            Random rnd = new Random();

            listMeans.RemoveAt(index);
            indexListMeans.RemoveAt(index);
            index = rnd.Next(0, indexListMeans.Count);
            await Task.Delay(2000);
            translate_two.IsEnabled = true;
            translate_one.IsEnabled = true;
            translate_three.Background = Color.FromRgb(241, 243, 244);
            RandomWords(ref indexListMeans, ref listMeans);
        }
    }
}
