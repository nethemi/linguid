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
        private List<Meaning> forLastIndexListMeans = new List<Meaning>();
        private int countFalseWords, countTrueWords = 0;
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

            foreach (var item in listMeans)
            {
                forLastIndexListMeans.Add(item);
            }

            base.OnAppearing();
        }

        private async void dictionaryViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Meaning meaning = (Meaning)e.SelectedItem;
            await Navigation.PushAsync(new WordPage(meaning));
        }

        private void RandomWords(ref List<Meaning> listMeans)
        {
            indexListMeans = new List<int>();
            for (int i = 0; i < listMeans.Count; i++)
            {
                indexListMeans.Add(i);
            }

            Random rnd = new Random();
            index = rnd.Next(indexListMeans.First(), indexListMeans.Last());

            if (listMeans.Count == 1)
            {
                index = 0;
                word.Text = listMeans.ElementAt(index).dictionary.Item;
                transcription.Text = listMeans.ElementAt(index).transcription.TranscriptionItem;


                int randIndex = 0;
                int randomBtn = rnd.Next(1, 4);
                switch (randomBtn)
                {
                    case 1:
                        translate_one.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_two.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_three.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        break;
                    case 2:
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_one.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        translate_two.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_three.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        break;
                    case 3:
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_one.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                        translate_two.Text = forLastIndexListMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                        translate_three.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                        break;
                }
            }

            else
            {

                if (listMeans.ElementAt(index) != null)
                {

                    word.Text = listMeans.ElementAt(index).dictionary.Item;
                    transcription.Text = listMeans.ElementAt(index).transcription.TranscriptionItem;

                    int randIndex;
                    int randomBtn = rnd.Next(1, 4);
                    switch (randomBtn)
                    {
                        case 1:
                            translate_one.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_two.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_three.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            break;
                        case 2:
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_one.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            translate_two.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_three.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            break;
                        case 3:
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_one.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            randIndex = rnd.Next(indexListMeans.First(), indexListMeans.Last());
                            translate_two.Text = listMeans.ElementAt(randIndex).dictionaryRu.ItemRu;
                            translate_three.Text = listMeans.ElementAt(index).dictionaryRu.ItemRu;
                            break;
                    }
                }
                else RandomWords(ref listMeans);
            }


        }

        private  async void ReturnToLessonsClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LessonPage());
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


            RandomWords(ref listMeans);

        }
        private async void TranslateOneClicked(object sender, EventArgs e)
        {
            translate_two.IsEnabled = false;
            translate_three.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_one.Text)
            {
                translate_one.Background = Color.FromHex("98D8AA");
                countTrueWords++;
            }
            else
            {
                translate_one.Background = Color.FromHex("FF6D60");
                countFalseWords++;
            }


            Random rnd = new Random();

            listMeans.RemoveAt(index);

            await Task.Delay(1000);
            translate_two.IsEnabled = true;
            translate_three.IsEnabled = true;
            translate_one.Background = Color.FromRgb(241, 243, 244);
            if (listMeans.Count == 0)
            {
                lessonContent.IsVisible = false;
                translate_one.IsEnabled = false;
                translate_two.IsEnabled = false;
                translate_three.IsEnabled = false;
                if(countFalseWords <= 1)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Отлично";
                    resultScoreFrame.BackgroundColor = Color.FromHex("98D8AA");
                }
                if(countFalseWords >=2 && countFalseWords <= 4)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Хорошо";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FFFBAC");
                }   
                if(countFalseWords >=5)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Повторите слова и попробуйте снова";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FF6D60");  
                }
                HistoryLesson lesson = new HistoryLesson();
                var userLogin = Thread.CurrentPrincipal.Identity.Name;
                foreach (var user in await App.Database.GetUserAsync())
                {
                    if (userLogin == user.UserLogin)
                    {
                        foreach (var MbC in await App.Database.GetMbCAsync())
                        {
                            if (MbC.fkCategory == _category.CategoryID)
                            {
                                lesson.fkUser = user.UserID;
                                lesson.fkMbC = MbC.MbCID;
                                lesson.Date = DateTime.Now;
                                await App.Database.SaveLessonAsync(lesson);
                            }
                        }
                    }
                }
            }
            else
                RandomWords(ref listMeans);
        }

        private async void TranslateTwoClicked(object sender, EventArgs e)
        {
            translate_one.IsEnabled = false;
            translate_three.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_two.Text)
            {
                translate_two.Background = Color.FromHex("98D8AA");
                countTrueWords++;
            }
            else
            {
                translate_two.Background = Color.FromHex("FF6D60");
                countFalseWords++;
            }
            Random rnd = new Random();

            listMeans.RemoveAt(index);

            await Task.Delay(1000);
            translate_one.IsEnabled = true;
            translate_three.IsEnabled = true;
            translate_two.Background = Color.FromRgb(241, 243, 244);
            if (listMeans.Count == 0)
            {
                lessonContent.IsVisible = false;
                translate_one.IsEnabled = false;
                translate_two.IsEnabled = false;
                translate_three.IsEnabled = false;
                if (countFalseWords <= 1)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Отлично";
                    resultScoreFrame.BackgroundColor = Color.FromHex("98D8AA");
                }
                if (countFalseWords >= 2 && countFalseWords <= 4)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Хорошо";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FFFBAC");
                }
                if (countFalseWords >= 5)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Повторите слова и попробуйте снова";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FF6D60");
                }

                HistoryLesson lesson = new HistoryLesson();
                var userLogin = Thread.CurrentPrincipal.Identity.Name;
                foreach (var user in await App.Database.GetUserAsync())
                {
                    if (userLogin == user.UserLogin)
                    {
                        foreach( var MbC in await App.Database.GetMbCAsync())
                        {
                            if (MbC.fkCategory == _category.CategoryID)
                            {
                                lesson.fkUser = user.UserID;
                                lesson.fkMbC = MbC.MbCID;
                                lesson.Date = DateTime.Now;
                                await App.Database.SaveLessonAsync(lesson);
                            }
                        }
                    }
                }
            }
            else
                RandomWords(ref listMeans);
        }

        private async void TranslateThreeClicked(object sender, EventArgs e)
        {
            translate_one.IsEnabled = false;
            translate_two.IsEnabled = false;
            if (listMeans.ElementAt(index).dictionaryRu.ItemRu == translate_three.Text)
            {
                translate_three.Background = Color.FromHex("98D8AA");
                countTrueWords++;
            }
            else
            {
                translate_three.Background = Color.FromHex("FF6D60");
                countFalseWords++;
            }

            Random rnd = new Random();

            listMeans.RemoveAt(index);

            await Task.Delay(2000);
            translate_two.IsEnabled = true;
            translate_one.IsEnabled = true;
            translate_three.Background = Color.FromRgb(241, 243, 244);
            if (listMeans.Count == 0)
            {
                lessonContent.IsVisible = false;
                translate_one.IsEnabled = false;
                translate_two.IsEnabled = false;
                translate_three.IsEnabled = false;
                if (countFalseWords <= 1)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Отлично";
                    resultScoreFrame.BackgroundColor = Color.FromHex("98D8AA");
                }
                if (countFalseWords >= 2 && countFalseWords <= 4)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Хорошо";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FFFBAC");
                }
                if (countFalseWords >= 5)
                {
                    endLesson.IsVisible = true;
                    resultScore.Text = countTrueWords + "/" + forLastIndexListMeans.Count;
                    resultMessage.Text = "Повторите слова и попробуйте снова";
                    resultScoreFrame.BackgroundColor = Color.FromHex("FF6D60");
                }

                HistoryLesson lesson = new HistoryLesson();
                var userLogin = Thread.CurrentPrincipal.Identity.Name;
                foreach (var user in await App.Database.GetUserAsync())
                {
                    if (userLogin == user.UserLogin)
                    {
                        foreach (var MbC in await App.Database.GetMbCAsync())
                        {
                            if (MbC.fkCategory == _category.CategoryID)
                            {
                                lesson.fkUser = user.UserID;
                                lesson.fkMbC = MbC.MbCID;
                                lesson.Date = DateTime.Now;
                                await App.Database.SaveLessonAsync(lesson);
                            }
                        }
                    }
                }
            }
            else
                RandomWords(ref listMeans);
        }
    }
}
