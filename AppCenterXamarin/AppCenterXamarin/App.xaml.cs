using System;
using System.Threading.Tasks;
using AppCenterXamarin.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Distribute;
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
            Analytics.TrackEvent("App started");
            Distribute.DisableAutomaticCheckForUpdate();
            Analytics.TrackEvent("Disabling automatic check for updates");
            Distribute.CheckForUpdate();
            Analytics.TrackEvent("Checking for updates");
            Distribute.NoReleaseAvailable = OnNoReleaseAvailable;
            Analytics.TrackEvent("Setting no release available callback");
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            Analytics.TrackEvent("Setting release available callback");
            
            AppCenter.Start("android=3aa0c8d9-c0bb-4cae-ad31-194d6f516038;",
                  typeof(Microsoft.AppCenter.Analytics.Analytics),
                  typeof(Microsoft.AppCenter.Crashes.Crashes));
            Analytics.TrackEvent("AppCenter started");
        }
        void OnNoReleaseAvailable()
        {
            AppCenterLog.Info("AppCenterXamarin", "No release available callback triggered.");
        }
        bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            Analytics.TrackEvent("Release available for version " + versionName);
            string versionCodeOrBuildNumber = releaseDetails.Version;
            Analytics.TrackEvent("Release available for version code " + versionCodeOrBuildNumber);
            string releaseNotes = releaseDetails.ReleaseNotes;
            Analytics.TrackEvent("Release notes: " + releaseNotes);
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;
            Analytics.TrackEvent("Release notes URL: " + releaseNotesUrl);

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user can't postpone
            if (releaseDetails.MandatoryUpdate)
            {
                Analytics.TrackEvent("Mandatory update, user can't postpone");
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                Analytics.TrackEvent("Not mandatory update, user can postpone");
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // This method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });
            Analytics.TrackEvent("Returning true");
            // Return true if you're using your own dialog, false otherwise
            return true;
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