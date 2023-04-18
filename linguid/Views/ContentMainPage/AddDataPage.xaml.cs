using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDataPage : ContentPage
    {
        Meaning meaning = new Meaning();
        public AddDataPage(Meaning selected)
        {
            InitializeComponent();
            if (selected != null)
            {
                meaning = selected;
                itemWord.Text = selected.dictionary.Item;
                transcriptionItem.Text = selected.transcription.TranscriptionItem;
                itemRu.Text = selected.dictionaryRu.ItemRu;

                choiceLang.SelectedIndex = selected.dictionary.fkLanguage - 1;
            }
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateLanguage();
            // привязка данных
            choiceLang.ItemsSource = await App.Database.GetLanguageAsync();

            base.OnAppearing();
        }

        private async void ButtonClicked(object sender, System.EventArgs e)
        {
            Dictionary dictionary = new Dictionary();
            dictionary.fkLanguage = choiceLang.SelectedIndex+1;
            dictionary.Item = itemWord.Text;
            await App.Database.SaveItemAsync(dictionary);

            DictionaryRU dictionaryRU = new DictionaryRU();
            dictionaryRU.ItemRu = itemRu.Text;
            await App.Database.SaveItemRuAsync(dictionaryRU);

            Transcription transcription = new Transcription();
            transcription.TranscriptionItem = transcriptionItem.Text;
            await App.Database.SaveTranscriptionAsync(transcription);

            meaning.fkItem = dictionary.ItemID;
            meaning.fkTransctription = transcription.TranscriptionID;
            meaning.fkItemRu = dictionaryRU.ItemRuID;
            await App.Database.SaveMeaningAsync(meaning);

            await DisplayAlert("Добавление в базу данных", $"Значение слова {dictionary.Item} успешно доавлено в базу данных!", "ОК");

            await Navigation.PushAsync(new MainPage());
        }
    }
}