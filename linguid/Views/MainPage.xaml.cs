using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin == "")
            {
                favoritePage.IsEnabled = false;
                lessonsPage.IsEnabled = false;
            }
           
            GetLanguage();
        }

        public async void GetLanguage()
        {
            var userLogin = Thread.CurrentPrincipal.Identity.Name;
            if (userLogin == "")
            {
                var language = await App.Database.GetLanguageAsync(1);
                lang.Source = ImageSource.FromResource("linguid.Images.england.png");
                languageName.Text = language.LanguageName;
            }
            else
            {
                var language = await App.Database.GetLanguageAsync();
                var user = await App.Database.GetUserAsync();
                foreach (var u in user)
                {
                    if (userLogin == u.UserLogin)
                    {
                        foreach (var item in language)
                        {
                            if (item.LanguageID == u.fkLanguage)
                            {
                                languageName.Text = item.LanguageName;
                                if (item.LanguageID == 1) lang.Source = ImageSource.FromResource("linguid.Images.england.png");
                                if (item.LanguageID == 2) lang.Source = ImageSource.FromResource("linguid.Images.china.png"); ;
                                if (item.LanguageID == 3) lang.Source = ImageSource.FromResource("linguid.Images.japan.png"); ;
                            }
                        }
                    }
                }
            }
        }
    }
}