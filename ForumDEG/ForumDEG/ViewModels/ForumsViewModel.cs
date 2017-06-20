using System;
using ForumDEG.Models;
using ForumDEG.Utils;
using System.Collections.ObjectModel;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ForumDEG.ViewModels {
    public class ForumsViewModel : BaseViewModel {
        public ObservableCollection<ForumDetailViewModel> Forums { get; private set; }

        private ForumDetailViewModel _selectedForum;
        public ForumDetailViewModel SelectedForum {
            get { return _selectedForum; }
            set { SetValue(ref _selectedForum, value); }
        }

        private bool _forumVisibility;
        public bool ForumVisibility {
            get {
                return _forumVisibility;
            }
            set {
                if (_forumVisibility != value) {
                    _forumVisibility = value;

                    OnPropertyChanged("ForumVisibility");
                }
            }
        }

        private bool _noForumWarning;
        public bool NoForumWarning {
            get {
                return _noForumWarning;
            }
            set {
                if (_noForumWarning != value) {
                    _noForumWarning = value;

                    OnPropertyChanged("NoForumWarning");
                }
            }
        }

        private bool _activityIndicator;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    OnPropertyChanged("ActivityIndicator");
                }
            }
        }

        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;

        public ICommand SelectForumCommand { get; private set; }

        private static ForumsViewModel _instance = null;
        public ForumsViewModel(IPageService pageService) {
            _pageService = pageService;
            _forumService = new Helpers.Forum();
            ForumVisibility = false;
            NoForumWarning = false;
            ActivityIndicator = false;
            SelectForumCommand = new Command<ForumDetailViewModel>(async vm => await SelectForum(vm));
        }

        public static ForumsViewModel GetInstance() {
            if (_instance == null) _instance = new ForumsViewModel(new PageService());
            return _instance;
        }

        public async Task SelectForum(ForumDetailViewModel forum) {
            if (forum == null)
                return;
            SelectedForum = forum;
            await _pageService.PushAsync(new ForumDetailPage(SelectedForum));
        }

        public async void UpdateForumsList() {
            ActivityIndicator = true;
            Forums = new ObservableCollection<ForumDetailViewModel>();
            Debug.WriteLine("[FORUMS LIST]: starts looking for forums" + ActivityIndicator);
            try {
                var forumsList = await _forumService.GetForumsAsync();

                foreach (Forum forum in forumsList) {
                    Forums.Add(new ForumDetailViewModel (new PageService()) {
                        Title = forum.Title,
                        Place = forum.Place,
                        Schedules = forum.Schedules,
                        Date = forum.Date,
                        Hour = forum.Hour,
                        Registration = forum.Id, // local id
                        RemoteId = forum.RemoteId // remote id, ideally should only use this one
                    });
                    ActivityIndicator = false;
                    Debug.WriteLine("[FORUMS LIST]: finishes looking for forums" + ActivityIndicator);
                }

                if(forumsList.Count == 0) {
                    NoForumWarning = true;
                    ForumVisibility = false;
                    ActivityIndicator = false;
                }
                else {
                    NoForumWarning = false;
                    ForumVisibility = true;
                    ActivityIndicator = false;
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Update forums list] " + ex.Message);
                await _pageService.DisplayAlert("Falha ao carregar fóruns",
                                          "Houve um erro ao estabelecer conexão com o servidor. Por favor, tente novamente.",
                                          null ,"Ok");
                await _pageService.PopAsync();
            }
        }
    }
}
