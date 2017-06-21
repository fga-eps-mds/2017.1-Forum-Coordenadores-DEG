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
using System.ComponentModel;
using System.Diagnostics;
using ForumDEG.Views.Forms;

namespace ForumDEG.ViewModels {
    public class CoordinatorMasterPageViewModel : PageService, INotifyPropertyChanged {
        private string _forumTitle;
        public string ForumTitle {
            get {
                return _forumTitle;
            }
            set {
                if (_forumTitle != value) {
                    _forumTitle = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumTitle"));
                }
            }
        }

        private string _formTitle;
        public string FormTitle {
            get {
                return _formTitle;
            }
            set {
                if (_formTitle != value) {
                    _formTitle = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FormTitle"));
                }
            }
        }

        private string _formQuestions;
        public string FormQuestions {
            get {

                return _formQuestions;
            }
            set {
                if (_formQuestions != value) {
                    _formQuestions = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FormQuestions"));
                }
            }
        }

        private string _forumPlace;
        public string ForumPlace {
            get {
                return _forumPlace;
            }
            set {
                if (_forumPlace != value) {
                    _forumPlace = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumPlace"));
                }
            }
        }

        private string _forumSchedules;
        public string ForumSchedules {
            get {
                return _forumSchedules;
            }
            set {
                if (_forumSchedules != value) {
                    _forumSchedules = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumSchedules"));
                }
            }
        }

        private DateTime _forumDate;
        public DateTime ForumDate {
            get {
                return _forumDate;
            }
            set {
                if (_forumDate != value) {
                    _forumDate = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumDate"));
                }
            }
        }

        private TimeSpan _forumHour;
        public TimeSpan ForumHour {
            get {
                return _forumHour;
            }
            set {
                if (_forumHour != value) {
                    _forumHour = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumHour"));
                }
            }
        }

        private bool _forumVisibility;
        public bool ForumVisibility {
            get {
                return _forumVisibility;
            }
            set {
                if (_forumVisibility != value) {
                    _forumVisibility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForumVisibility"));
                }
            }
        }


        private bool _formVisibility;
        public bool FormVisibility {
            get {
                return _formVisibility;
            }
            set {
                if (_formVisibility != value) {
                    _formVisibility = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FormVisibility"));
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

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NoForumWarning"));
                }
            }
        }

        private bool _noFormWarning;
        public bool NoFormWarning {
            get {
                return _noFormWarning;
            }
            set {
                if (_noFormWarning != value) {
                    _noFormWarning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NoFormWarning"));
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

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivityIndicator"));
                }
            }
        }

        private bool _isLoaded;
        public bool IsLoaded {
            get {
                return _isLoaded;
            }
            set {
                if (_isLoaded != value) {
                    _isLoaded = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLoaded"));
                }
            }
        }

        private static CoordinatorMasterPageViewModel _instance = null;
        public ObservableCollection<ForumDetailViewModel> Forums { get; private set; }
        public ForumDetailViewModel SelectedForum { get; private set; }
        public FormDetailViewModel SelectedForm { get; private set; }
        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;
        private readonly Helpers.Form _formService;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ForumDetailPageCommand { get; set; }
        public ICommand FormDetailPageCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand DetailPageCommand { get; set; }

        public CoordinatorMasterPageViewModel(IPageService pageService) {
            ForumVisibility = false;
            NoForumWarning = false;
            ActivityIndicator = false;
            IsLoaded = false;
            _pageService = pageService;
            _forumService = new Helpers.Forum();
            _formService = new Helpers.Form();
            ForumDetailPageCommand = new Command(SeeForumDetailPage);
            FormDetailPageCommand = new Command(SeeFormDetailPage);
            ChangePasswordCommand = new Command(async () => await ChangePassword());
        }

        public static CoordinatorMasterPageViewModel GetInstance() {
            if (_instance == null)
                _instance = new CoordinatorMasterPageViewModel(new PageService());
            return _instance;
        }

        public async Task<Forum> SelectNextForum () {
            var listForum = await _forumService.GetForumsAsync();

            return GetLatestForum(listForum);
        }


        public Forum GetLatestForum(List<Forum> forums) {
            ActivityIndicator = true;
            Forum latestForum = null;
            foreach (Forum forum in forums) {
                Debug.WriteLine("[SelectNextForum]: picks a new forum");
                if (DateTime.Compare(DateTime.Today, forum.Date) <= 0) {
                    if (latestForum == null) {
                        latestForum = forum;
                        Debug.WriteLine("[SelectNextForum]: sets a value to the latest forum variable");
                    } else {
                        if (DateTime.Compare(forum.Date, latestForum.Date) < 0) {
                            latestForum = forum;
                            Debug.WriteLine("[SelectNextForum]: updates the next forum");
                        }
                    }
                }
            }
            ActivityIndicator = false;
            return latestForum;
        }


        public async void SelectForum() {
            Debug.WriteLine("[SelectForum]");
            Forum latestForum = null;
            latestForum = await SelectNextForum();

            SetLatestForumFields(latestForum);
        }



        public void SetLatestForumFields(Forum latestForum) {
            if (latestForum == null) {
                ForumVisibility = false;
                NoForumWarning = true;
                Debug.WriteLine("[SelectForum]: noForumWarning is set to: " + _noForumWarning);
            } else {
                ForumVisibility = true;
                NoForumWarning = false;

                ForumTitle = latestForum.Title;
                ForumPlace = latestForum.Place;
                ForumSchedules = latestForum.Schedules;
                ForumDate = latestForum.Date;
                ForumHour = latestForum.Hour;

                Debug.WriteLine("[SelectForum]: gets a non null forum");
                Debug.WriteLine("[SelectForum]: title: " + ForumTitle);
                Debug.WriteLine("[SelectForum]: place: " + ForumPlace);
                Debug.WriteLine("[SelectForum]: schedules: " + ForumSchedules);

                SelectedForum = new ForumDetailViewModel(_pageService) {
                    Title = latestForum.Title,
                    Place = latestForum.Place,
                    Schedules = latestForum.Schedules,
                    Date = latestForum.Date,
                    Hour = latestForum.Hour,
                    RemoteId = latestForum.RemoteId
                };

                IsLoaded = true;

                Debug.WriteLine("[SelectNextForum]: title " + SelectedForum.Title);
                Debug.WriteLine("[SelectNextForum]: place " + SelectedForum.Place);
                Debug.WriteLine("[SelectNextForum]: schedules " + SelectedForum.Schedules);
            }
        }

        public async Task<Form> SelectNextForm() {
            var listForm = await _formService.GetFormsAsync();

            return GetLatestForm(listForm);
        }

        public Form GetLatestForm(List<Form> forms) {
            if (forms == null)
                return null;
            if (forms.Count == 0)
                return null;
            return forms[0];
        }

        public async void SelectForm() {
            Debug.WriteLine("[SelectForm]");
            Form latestForm = null;
            latestForm = await SelectNextForm();

            SetLatestFormFields(latestForm);
        }

        public void SetLatestFormFields(Form latestForm) {

            if (latestForm == null) {
                FormVisibility = false;
                NoFormWarning = true;
                Debug.WriteLine("[SelectForm]: noFormWarning is set to: " + _noFormWarning);
                return;
            } else {
                FormVisibility = true;
                NoFormWarning = false;

                FormTitle = latestForm.Title;
                int size = latestForm.MultipleChoiceQuestions.Count + latestForm.DiscursiveQuestions.Count;
                FormQuestions = (size).ToString() + " Questões";
            }

            Debug.WriteLine("[SelectForm]: gets a non null forum");
            Debug.WriteLine("[SelectForm]: title: " + FormTitle);
            Debug.WriteLine("[SelectForm]: questions: " + FormQuestions);


            SelectedForm = new FormDetailViewModel(_pageService) {
                Title = latestForm.Title,
                MultipleChoiceQuestions = latestForm.MultipleChoiceQuestions,
                DiscursiveQuestions = latestForm.DiscursiveQuestions
            };

            IsLoaded = true;

            Debug.WriteLine("[SelectNextForm]: title " + SelectedForm.Title);
        }

        public async void SeeForumDetailPage() {
            Debug.WriteLine("[Coord. Main] - Inside SeeForumDetailPage");
            await _pageService.PushAsync(new ForumDetailPage(SelectedForum));
        }
        public async void SeeFormDetailPage() {
            Debug.WriteLine("[Coord. Main] - Inside SeeFormDetailPage");
            await _pageService.PushAsync(new FormDetailPage(SelectedForm));
        }


        private async Task ChangePassword() {
            Debug.WriteLine("[Coord. Main] - Inside ChangePassword");
            await _pageService.PushAsync(new ChangePasswordPage());
        }
    }
}
