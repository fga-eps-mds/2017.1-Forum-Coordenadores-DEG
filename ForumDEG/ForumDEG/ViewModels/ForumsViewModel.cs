using System;
using ForumDEG.Models;
using ForumDEG.Utils;
using System.Collections.ObjectModel;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class ForumsViewModel : BaseViewModel {
        public ObservableCollection<ForumDetailViewModel> Forums { get; private set; }

        private ForumDetailViewModel _selectedForum;
        public ForumDetailViewModel SelectedForum {
            get { return _selectedForum; }
            set { SetValue(ref _selectedForum, value); }
        }

        private readonly IPageService _pageService;

        public ICommand SelectForumCommand { get; private set; }

        private static ForumsViewModel _instance = null;
        private ForumsViewModel(IPageService pageService) {
            _pageService = pageService;
            SelectForumCommand = new Command<ForumDetailViewModel>(async vm => await SelectForum(vm));
        }

        public static ForumsViewModel GetInstance() {
            if (_instance == null) _instance = new ForumsViewModel(new PageService());
            return _instance;
        }

        private async Task SelectForum(ForumDetailViewModel forum) {
            if (forum == null)
                return;
            SelectedForum = forum;
            await _pageService.PushAsync(new ForumDetailPage());
        }

        // method for simulating local database
        public async void UpdateForumsList() {
            Forums = new ObservableCollection<ForumDetailViewModel>();
            var forumsList = await ForumDatabase.getForumDB.GetAllForums();

            foreach (Forum forum in forumsList) {
                Forums.Add(new ForumDetailViewModel {
                    Title = forum.Title,
                    Place = forum.Place,
                    Schedules =  forum.Schedules,
                    Date = forum.Date,
                    Hour = forum.Hour
                });
            }
        }
    }
}
