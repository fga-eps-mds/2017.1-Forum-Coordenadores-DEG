using ForumDEG.DAO;
using ForumDEG.Models;
using ForumDEG.Utils;
using ForumDEG.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {

        public App() {
            InitializeComponent();

            if (ForumDEG.Helpers.Settings.IsUserLogged) {
                if (Helpers.Settings.IsUserAdmin) {
                    MainPage = new NavigationPage(new AppMasterPage()) {
                        BarBackgroundColor = Color.FromHex("#ff8924")
                    };
                }
                else if (Helpers.Settings.IsUserCoord) {
                    MainPage = new NavigationPage(new CoordinatorTabbedPage()) {
                        BarBackgroundColor = Color.FromHex("#ff8924")
                    };
                }
            } else {
                MainPage = new NavigationPage(new LoginPage()) {
                    BarBackgroundColor = Color.FromHex("#ff8924")
                };
            }
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
