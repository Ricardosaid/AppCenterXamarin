using System;
using AppCenterXamarin.Views;
using Microsoft.AppCenter;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace AppCenterXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=3aa0c8d9-c0bb-4cae-ad31-194d6f516038;",
                  typeof(Microsoft.AppCenter.Analytics.Analytics),
                  typeof(Microsoft.AppCenter.Crashes.Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}