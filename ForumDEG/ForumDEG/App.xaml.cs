using ForumDEG.Utils;
using ForumDEG.Views;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {

        static AdministratorDatabase _administratorDatabase;
        static CoordinatorDatabase _coordinatorDatabase;
        static ForumDatabase _forumDatabase;

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new AdministratorsPage());
            //MainPage = new ForumDEG.MainPage();
        }

        public static AdministratorDatabase AdministratorDatabase{
            get {
                if(_administratorDatabase == null){
                    _administratorDatabase = new AdministratorDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Administrator.db3"));
                }
                return _administratorDatabase;
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
