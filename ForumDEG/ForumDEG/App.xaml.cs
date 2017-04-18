using ForumDEG.Utils;
using ForumDEG.Views;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {
        static AdministratorDatabase _administratorDatabase;
        static CoordinatorDatabase _coordinatorDatabase;
        //static ForumDatabase _forumDatabase;

        static AdministratorDatabase _administratorDatabase;
        static CoordinatorDatabase _coordinatorDatabase;
        //static ForumDatabase _forumDatabase;

        public App() {
            InitializeComponent();

            if (Helpers.Settings.IsLoggedIn) {
                if (Helpers.Settings.IsAdmin) {
                    MainPage = new NavigationPage(new Views.MainPageAdministrator());
                } else {
                    MainPage = new NavigationPage(new Views.MainPageCoordinator());
                }
            } else {
                MainPage = new NavigationPage(new Views.LoginPage());
            }

        }

        public static AdministratorDatabase AdministratorDatabase{
            get {
                if(_administratorDatabase == null){
                    _administratorDatabase = new AdministratorDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Administrator.db3"));
                }
                return _administratorDatabase;

            }
        }

        public static AdministratorDatabase AdministratorDatabase{
            get {
                if(_administratorDatabase == null){
                    _administratorDatabase = new AdministratorDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Administrator.db3"));
                }
                return _administratorDatabase;
            }
        }

        public static CoordinatorDatabase CoordinatorDatabase{
            get{
                if (_coordinatorDatabase == null){
                    _coordinatorDatabase = new CoordinatorDatabase(DependencyService.Get<InterfaceSQLite>().GetLocalFilePath("Coordinator.db3"));
                }
                return _coordinatorDatabase;
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
