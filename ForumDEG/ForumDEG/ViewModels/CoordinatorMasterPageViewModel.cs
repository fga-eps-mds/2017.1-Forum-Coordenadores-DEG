using System;
using ForumDEG.Models;
using ForumDEG.Utils;
using System.Collections.ObjectModel;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ForumDEG.ViewModels {
    public class CoordinatorMasterPageViewModel : ContentView {

        private static CoordinatorMasterPageViewModel _instance = null;
        public ObservableCollection<ForumDetailViewModel> Forums { get; private set; }
        public ForumDetailViewModel SelectedForum { get; private set; }


        public CoordinatorMasterPageViewModel() {
            Content = new Label { Text = "Hello View" };
        }


        public static CoordinatorMasterPageViewModel GetInstance() {
            if (_instance == null)
                _instance = new CoordinatorMasterPageViewModel();
            return _instance;
        }

        private void SelectForum(ForumDetailViewModel forum) {
            if (forum == null)
                return;
            SelectedForum = forum;
        }

        public void UpdateForumsList() {
            List<Forum> listforum;
            listforum = App.ForumDatabase.GetAllForums().Result;
            Forum latestForum = new Forum();
                latestForum._title = "Teste";
            latestForum._date = DateTime.Parse("08-05-2017");
            latestForum._hour = TimeSpan.Parse("10:00");
            SelectForum(new ForumDetailViewModel {
                Title = latestForum._title,
                Place = latestForum._place,
                Schedules = latestForum._schedules,
                Date = latestForum._date,
                Hour = latestForum._hour
            });
        }

    }
}
