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

        public Forum SelectNextForum(List<Forum> listforum) {
            Forum latestForum = null;
            foreach (Forum forum in  listforum) {
                if (DateTime.Compare(DateTime.Now, forum.Date) <= 0) {
                    if (latestForum == null)
                        latestForum = forum;
                    else {
                        if (DateTime.Compare(forum.Date, latestForum.Date)<0)
                            latestForum = forum;
                    }
                }
            }
            return latestForum;
        }


        public void SelectForum() {
            List<Forum> listforum = null;
            Task<List<Forum>> task;
            task = ForumDatabase.getForumDB.GetAll();
            listforum = task.Result;
            Forum latestForum = null;
            if (listforum.Count >0)
                 latestForum = SelectNextForum(listforum);
            if(latestForum==null){
                latestForum = new Forum();;
                latestForum.Title = "Nenhum forum disponivel";
            }
            
            SelectedForum = new ForumDetailViewModel {
                Title = latestForum.Title,
                Place = latestForum.Place,
                Schedules = latestForum.Schedules,
                Date = latestForum.Date,
                Hour = latestForum.Hour
            };
        }

    }
}
