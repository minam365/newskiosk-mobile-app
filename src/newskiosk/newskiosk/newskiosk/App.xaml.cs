using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using newskiosk.Services;
using newskiosk.Views;

namespace newskiosk
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<NewsSourceDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
