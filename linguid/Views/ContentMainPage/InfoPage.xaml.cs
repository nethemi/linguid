﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();

            db.Text = App.DATABASE_NAME;
        }

        protected override async void OnAppearing()
        {
            tableView.ItemsSource = await App.database.GetAllTablesAsync();
            base .OnAppearing();    
        }
    }
}