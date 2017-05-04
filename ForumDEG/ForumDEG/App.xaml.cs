using ForumDEG.DAO;
using ForumDEG.Models;
using ForumDEG.Utils;
using ForumDEG.Views;
using System;
using Xamarin.Forms;

namespace ForumDEG {
    public partial class App : Application {
        static AdministratorDatabase _administratorDatabase;
        static CoordinatorDatabase _coordinatorDatabase;
        static FormAskDatabase _formAsksDatabase;
        static FormDatabase _formDatabase;
        static ForumConfirmationDatabase _forumConfirmationDatabase;

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
                    _administratorDatabase = new AdministratorDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Administrator.db3"));
                }
                return _administratorDatabase;
            }
        }

        public static CoordinatorDatabase CoordinatorDatabase{
            get{
                if (_coordinatorDatabase == null){
                    _coordinatorDatabase = new CoordinatorDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("Coordinator.db3"));
                }
                return _coordinatorDatabase;
            }
        }

        protected override void OnStart() {
            PopulateForTest();
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        // Testing Database
        private void PopulateForTest() {
            
            // Test for administrator
            Administrator administrator = new Administrator();
            administrator.Name = "João";
            administrator.Password = "123";
            administrator.Registration = "456";
            administrator.Email = "Joao@oi.com";
            administrator.CreatedOn = DateTime.Now;

            _administratorDatabase.Save(administrator);

            // Test for coordinator
            Coordinator coordinator = new Coordinator();
            coordinator.Name = "Maria";
            coordinator.Password = "123";
            coordinator.Registration = "456";
            coordinator.Email = "Maria@oi.com";
            coordinator.CreatedOn = DateTime.Now;

            _coordinatorDatabase.Save(coordinator);

            // Test for Forum
            Forum forum = new Forum();
            forum.Title = "Forum 1";
            forum.Place = "Reitoria";
            forum.Schedules = "Varias pautas.";
            forum.Hour = DateTime.Now.TimeOfDay;
            forum.Date = DateTime.Now;

            ForumDatabase.getForumDB.Save(forum);

            // Test for Form
            Form form = new Form();
            form.ForumId = forum.Id;
            form.CreatedOn = DateTime.Now;
            
            FormDatabase.getFormDB.Save(form);

            // Test for FormAsk
            FormAsk formAsk1 = new FormAsk();
            formAsk1.AskType = 1;
            formAsk1.Options = "opçao1; opçao2; opçao3;";
            formAsk1.FormId = form.Id;

            FormAskDatabase.getFormDB.Save(formAsk1);

            FormAsk formAsk2 = new FormAsk();
            formAsk2 = new FormAsk();
            formAsk2.AskType = 2;
            formAsk2.Options = "opçao1; opçao2; opçao3;";
            formAsk2.FormId = form.Id;

            FormAskDatabase.getFormDB.Save(formAsk2);

            FormAsk formAsk3 = new FormAsk();
            formAsk3 = new FormAsk();
            formAsk3.AskType = 3;
            formAsk3.Options = "Opçao dissertativa";
            formAsk3.FormId = form.Id;

            FormAskDatabase.getFormDB.Save(formAsk3);

            // Test for forum confirmation
            ForumConfirmation forumConfirmation = new ForumConfirmation();
            forumConfirmation.ForumId = forum.Id;
            forumConfirmation.UserId = coordinator.Id;

            ForumConfirmationDatabase.getForumConfirmationDB.Save(forumConfirmation);

            // Test for form answers
            FormAnswer formAnswer1 = new FormAnswer();
            formAnswer1.FormAskId = formAsk1.Id;
            formAnswer1.OptionAnswerPosition = 1;
            formAnswer1.UserId = coordinator.Id;

            FormAnswerDatabase.getFormDB.Save(formAnswer1);

            FormAnswer formAnswer2 = new FormAnswer();
            formAnswer2.FormAskId = formAsk2.Id;
            formAnswer2.MultipleAnswerPositions = "1; 2;";
            formAnswer2.UserId = coordinator.Id;

            FormAnswerDatabase.getFormDB.Save(formAnswer2);

            FormAnswer formAnswer3 = new FormAnswer();
            formAnswer3.FormAskId = formAsk1.Id;
            formAnswer3.TextAnswer = "Resposta da pergunta.";
            formAnswer3.UserId = coordinator.Id;

            FormAnswerDatabase.getFormDB.Save(formAnswer3);
        }
    }
}
