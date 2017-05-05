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
using Android.Util;

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

        private Forum SelectNextForum(List<Forum> listforum) {
            Forum latestForum = null;
            Log.Info("SelectForum", "passou1");
            foreach (Forum forum in  listforum) {
                Log.Info("SelectForum", "passou");
                if (DateTime.Compare(DateTime.Now, forum._date) <= 0) {
                    if (latestForum == null)
                        latestForum = forum;
                    else {
                        if (DateTime.Compare(forum._date, latestForum._date)<0)
                            latestForum = forum;
                    }
                }
            }
            return latestForum;
        }


        public void SelectForum() {
            List<Forum> listforum = null;
            Task<List<Forum>> task;
            task = ForumDatabase.getForumDB.GetAllForums();
            listforum = task.Result;
            Forum latestForum = null;
            if (listforum.Count >0)
                 latestForum = SelectNextForum(listforum);
            if(latestForum==null){
                latestForum = new Forum();
                Log.Info("SelectForum", "Nao passou");
                latestForum._title = "Nenhum forum disponivel";
            }
            
            SelectedForum = new ForumDetailViewModel {
                Title = latestForum._title,
                Place = latestForum._place,
                Schedules = latestForum._schedules,
                Date = latestForum._date,
                Hour = latestForum._hour
            };
        }

    }
}
