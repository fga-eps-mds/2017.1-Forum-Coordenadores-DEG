using ForumDEG.Utils;
using ForumDEG.Views;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {
        static AdministratorDatabase _administratorDatabase;
        static CoordinatorDatabase _coordinatorDatabase;
        //static FormDatabase _formDatabase;
        //static ForumDatabase _forumDatabase;

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new AppMasterPage()) {
                BarBackgroundColor = Color.FromHex("#ff8924")
            };
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
