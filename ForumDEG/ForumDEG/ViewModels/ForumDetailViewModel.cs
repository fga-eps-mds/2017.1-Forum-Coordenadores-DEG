using System;
using ForumDEG.Utils;
using ForumDEG.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using ForumDEG.Helpers;
using ForumDEG.Interfaces;
using System.Diagnostics;

namespace ForumDEG.ViewModels {
    public class ForumDetailViewModel : PageService, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;

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
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set;}


        public bool IsCoordinator {
            get {
                return _isCoordinator;
            }
            set {
                if (_isCoordinator != value) {
                    _isCoordinator = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCoordinator"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAdministrator"));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCoordinator"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAdministrator"));
                }
            }
        }

        public ForumDetailViewModel(IPageService pageService) {
            _pageService = pageService;
            EditCommand = new Command(EditForum);
            PresenceCommand = new Command(HandlePresence);
            DeleteCommand = new Command(DeleteForum);
            coordinatorService = new Helpers.Coordinator();
            IsPast = HasPassed();
            _forumService = new Helpers.Forum();

            //GetConfirmation();
        }

        public async void GetConfirmation() {
            _isConfirmed = await coordinatorService.GetConfirmationStatusAsync(ForumDEG.Helpers.Settings.UserReg, RemoteId);
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
                coordinatorService.PostConfirmationStatusAsync(App.Current.Properties["registration"].ToString(), RemoteId);
            } else {
                coordinatorService.DeleteConfirmationAsync(App.Current.Properties["registration"].ToString(), RemoteId);
            }

            TogglePresence();
            HandleButtonUI();
        }

        public void TogglePresence() {
            _isConfirmed = !_isConfirmed;
        }

        private async void EditForum() {
            Debug.WriteLine(" EDITAR FORUM ");
            await PushAsync(new ForumEditPage(RemoteId));
        }

        private async void DeleteForum() {
           var answer = await _pageService.DisplayAlert("Deletar Fórum", "Tem certeza que deseja deletar o fórum existente? Esta ação não poderá ser desfeita.", "Sim", "Não");
           Debug.WriteLine("Answer: " + answer);
            if (answer == true) {
                if (await _forumService.DeleteForumAsync(RemoteId) ){
                    await _pageService.PopAsync();
                } else {
                    await _pageService.DisplayAlert("Erro!", "O fórum não pôde ser deletado, tente novamente.", "OK", "Cancelar");
                }
            }
        }        
    }
}
