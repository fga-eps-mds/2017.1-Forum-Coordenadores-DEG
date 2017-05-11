using System;
using ForumDEG.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using ForumDEG.Helpers;

namespace ForumDEG.ViewModels {
    public class ForumDetailViewModel : PageService, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public ICommand PresenceCommand { get; private set; }
        public ICommand EditComand { get; private set; }

        public ForumDetailViewModel() {
            coordinatorService = new Helpers.Coordinator();

            EditComand = new Command(EditForum);
            PresenceCommand = new Command(HandlePresence);
            IsPast = HasPassed();

            getConfirmation();
        }

        private async void getConfirmation() {
            _isConfirmed = await coordinatorService.GetConfirmationStatusAsync(Constants.Registration, Constants.ForumId);
            HandleButtonUI();
        }

        public void HandleButtonUI() {
            if (_isConfirmed) {
                Debug.WriteLine("[ForumDetailVM]: isConfirmed true");
                ButtonText = "Cancelar presença";
                ButtonColor = Color.Red;
            } else {
                ButtonText = "Confirmar presença";
                ButtonColor = Color.Orange;
            }
        }

        public bool HasPassed() {
            return DateTime.Now > Date;
        }

        public void HandlePresence() {
            Debug.WriteLine("[ForumDetailVM]: Inside Presence Handler");
            if (!_isConfirmed) {
                coordinatorService.PostConfirmationStatusAsync(Constants.Registration, Constants.ForumId);
            } else {
                coordinatorService.DeleteConfirmationAsync(Constants.Registration, Constants.ForumId);
            }

            TogglePresence();
            HandleButtonUI();
        }

        public void TogglePresence() {
            _isConfirmed = !_isConfirmed;
        }

        private async void EditForum() {
            await PushAsync(new ForumEditPage(Registration)); 
        }
    }
}
