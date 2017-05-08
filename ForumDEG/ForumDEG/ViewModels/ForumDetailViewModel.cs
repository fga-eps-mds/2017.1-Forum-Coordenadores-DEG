using System;
using ForumDEG.Utils;
using ForumDEG.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using ForumDEG.Helpers;
using ForumDEG.Interfaces;

namespace ForumDEG.ViewModels {
    public class ForumDetailViewModel : PageService, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IPageService _pageService;
        private string _currentUser = App.Current.Properties["registration"].ToString();

        private string _buttonText;
        private Color _buttonColor;
        private bool _isConfirmed;
        public bool IsConfirmed {
            get {
                return _isConfirmed;
            }
            set {
                _isConfirmed = value;
            }
        }

        private Helpers.Coordinator coordinatorService;

        /* Forum properties */
        
        public string Title { get; set; }
        public string Place { get; set; }
        public string Schedules { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public bool IsPast { get; private set; }
        public int Registration { get; set; }
        public string RemoteId { get; set; }

        /* Button UI properties */
        public string ButtonText {
            get {
                return _buttonText;
            }
            set {
                if (_buttonText != value) {
                    _buttonText = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonText"));
                }
            }
        }

        public Color ButtonColor {
            get {
                return _buttonColor;
            }
            set {
                if (_buttonColor != value) {
                    _buttonColor = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonColor"));
                }
            }
        }
        private bool _isCoordinator;
        
        public ICommand PresenceCommand { get; private set; }
        public ICommand EditComand { get; private set; }
        public ICommand DeleteCommand { get; private set;}
        
        public ForumDetailViewModel() {
            coordinatorService = new Helpers.Coordinator();

        public bool IsCoordinator {
            get {
                return _isCoordinator;
            }
            set {
                if (_isCoordinator != value) {
                    _isCoordinator = value;
                    OnPropertyChanged("IsCoordinator");
                    OnPropertyChanged("IsAdiministrator");
                }
            }
        }

        public bool IsAdiministrator {
            get {
                return !_isCoordinator;
            }
            set {
                if (_isCoordinator == value) {
                    _isCoordinator = !value;
                    OnPropertyChanged("IsCoordinator");
                    OnPropertyChanged("IsAdiministrator");
                }
            }
        }

        public ForumDetailViewModel(IPageService pageService) {
            _pageService = pageService;
            ConfirmCommand = new Command(ConfirmPresence);
            DisconfirmCommand = new Command(DisconfirmPresence);
            EditComand = new Command(EditForum);
            PresenceCommand = new Command(HandlePresence);
            DeleteCommand = new Command(DeleteForum);
            IsPast = HasPassed();

            //GetConfirmation();
        }

        public async void GetConfirmation() {
            _isConfirmed = await coordinatorService.GetConfirmationStatusAsync(_currentUser, RemoteId);
            HandleButtonUI();
        }

        public void HandleButtonUI() {
            if (_isConfirmed) {
                Debug.WriteLine("[ForumDetailVM]: isConfirmed true");
                ButtonText = "Cancelar presença";
                ButtonColor = Color.Red;
            } else {
                Debug.WriteLine("[ForumDetailVM]: isConfirmed false, " + Title + " forum");
                ButtonText = "Confirmar presença";
                ButtonColor = Color.Orange;
            }
            SetUserType();
        }

        public void SetUserType() {
            //[TO DO]
            //If logged user type coordinator set is coordinator true 
            IsCoordinator = false;
        }

        public bool HasPassed() {
            return DateTime.Now > Date;
        }

        public void HandlePresence() {
            Debug.WriteLine("[ForumDetailVM]: Inside Presence Handler");
            if (!_isConfirmed) {
                coordinatorService.PostConfirmationStatusAsync(_currentUser, RemoteId);
            } else {
                coordinatorService.DeleteConfirmationAsync(_currentUser, RemoteId);
            }

            TogglePresence();
            HandleButtonUI();
        }

        public void TogglePresence() {
            _isConfirmed = !_isConfirmed;
        }

        private async void EditForum() {
            await PushAsync(new ForumEditPage(RemoteId)); 
            await _pageService.PushAsync(new ForumEditPage(Registration));
        }

        private async void DeleteForum() {
            var _toDeleteForum = await ForumDatabase.getForumDB.Get(Registration);
            await ForumDatabase.getForumDB.Delete(_toDeleteForum);
            await _pageService.PopAsync();
        }
    }
}
