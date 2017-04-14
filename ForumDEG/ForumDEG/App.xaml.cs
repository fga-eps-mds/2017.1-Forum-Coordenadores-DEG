using ForumDEG.Utils;
using ForumDEG.Views;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {

        static ForumDatabase _forumDatabase;
         
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.NewForumPage());
        }

        public static ForumDatabase ForumDatabase {
            get {
                if (_forumDatabase == null) {
                    _forumDatabase = new ForumDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Forum.db3"));
                }

                return _forumDatabase;
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
