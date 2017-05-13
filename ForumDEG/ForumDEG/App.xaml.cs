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

            MainPage = new NavigationPage(new LoginPage()) {
                BarBackgroundColor = Color.FromHex("#ff8924")
            };
            //MainPage = new ForumDEG.MainPage();
        }

        protected override void OnStart() {
            if (AdministratorDatabase.getAdmDB.GetAll().Result.Count == 0) {
                PopulateForTest();
            }
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        // Testing Database
        private async void PopulateForTest() {
            
            // Test for administrator
            Debug.WriteLine("Begin adm");
            Administrator administrator = new Administrator();
            Debug.WriteLine("Adm constructor called");
            administrator.Name = "Adm";
            administrator.Password = "adm";
            administrator.Registration = "456";
            administrator.Email = "adm@adm.adm";
            administrator.CreatedOn = DateTime.Now;
            Debug.WriteLine("Adminstrator attr created");

            await AdministratorDatabase.getAdmDB.Save(administrator);
            Debug.WriteLine("End adm"); 

            Debug.WriteLine("Begin coord");
            // Test for coordinator
            Coordinator coordinator = new Coordinator();
            coordinator.Name = "Maria";
            coordinator.Password = "123";
            coordinator.Registration = "456";
            coordinator.Email = "Maria@oi.com";
            coordinator.CreatedOn = DateTime.Now;

            await CoordinatorDatabase.getCoordinatorDB.Save(coordinator);
            Debug.WriteLine("End coord"); 

            Debug.WriteLine("Begin Forum");
            // Test for Forum
            Forum forum = new Forum();
            forum.Title = "Forum 1";
            forum.Place = "Reitoria";
            forum.Schedules = "Varias pautas.";
            forum.Hour = DateTime.Now.TimeOfDay;
            forum.Date = DateTime.Now;

            await ForumDatabase.getForumDB.Save(forum);
            Debug.WriteLine("End Forum");

            Debug.WriteLine("Begin Form");
            // Test for Form
            Form form = new Form();
            form.ForumId = forum.Id;
            form.CreatedOn = DateTime.Now;
            
            await FormDatabase.getFormDB.Save(form);
            Debug.WriteLine("End Form");

            Debug.WriteLine("Begin FormAsk1");
            // Test for FormAsk
            FormAsk formAsk1 = new FormAsk();
            formAsk1.AskType = 1;
            formAsk1.Options = "opçao1; opçao2; opçao3;";
            formAsk1.FormId = form.Id;

            await FormAskDatabase.getFormDB.Save(formAsk1);
            Debug.WriteLine("End FormAsk1");

            Debug.WriteLine("Begin FormAsk2");
            FormAsk formAsk2 = new FormAsk();
            formAsk2 = new FormAsk();
            formAsk2.AskType = 2;
            formAsk2.Options = "opçao1; opçao2; opçao3;";
            formAsk2.FormId = form.Id;

            await FormAskDatabase.getFormDB.Save(formAsk2);
            Debug.WriteLine("End FormAsk2");

            Debug.WriteLine("Begin FormAsk3");
            FormAsk formAsk3 = new FormAsk();
            formAsk3 = new FormAsk();
            formAsk3.AskType = 3;
            formAsk3.Options = "Opçao dissertativa";
            formAsk3.FormId = form.Id;

            await FormAskDatabase.getFormDB.Save(formAsk3);
            Debug.WriteLine("End FormAsk3");

            Debug.WriteLine("Begin ForumConfirmation");
            // Test for forum confirmation
            ForumConfirmation forumConfirmation = new ForumConfirmation();
            forumConfirmation.ForumId = forum.Id;
            forumConfirmation.UserId = coordinator.Id;

            await ForumConfirmationDatabase.getForumConfirmationDB.Save(forumConfirmation);
            Debug.WriteLine("End ForumConfirmation");

            Debug.WriteLine("Begin FormAn1");
            // Test for form answers
            FormAnswer formAnswer1 = new FormAnswer();
            formAnswer1.FormAskId = formAsk1.Id;
            formAnswer1.OptionAnswerPosition = 1;
            formAnswer1.UserId = coordinator.Id;

            await FormAnswerDatabase.getFormDB.Save(formAnswer1);
            Debug.WriteLine("End FormAn1");

            Debug.WriteLine("Begin FormAn2");
            FormAnswer formAnswer2 = new FormAnswer();
            formAnswer2.FormAskId = formAsk2.Id;
            formAnswer2.MultipleAnswerPositions = "1; 2;";
            formAnswer2.UserId = coordinator.Id;

            await FormAnswerDatabase.getFormDB.Save(formAnswer2);
            Debug.WriteLine("End FormAn2");

            Debug.WriteLine("Begin FormAn3");
            FormAnswer formAnswer3 = new FormAnswer();
            formAnswer3.FormAskId = formAsk3.Id;
            formAnswer3.TextAnswer = "Resposta da pergunta.";
            formAnswer3.UserId = coordinator.Id;

            await FormAnswerDatabase.getFormDB.Save(formAnswer3);
            Debug.WriteLine("End FormAn3");
        }
    }
}
