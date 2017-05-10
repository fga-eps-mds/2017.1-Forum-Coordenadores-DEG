using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {

    class AppMasterViewModel : INotifyPropertyChanged {
        public ICommand HomeClickedCommand { get; private set; }
        public ICommand ForumsClickedCommand { get; private set; }
        public ICommand UsersClickedCommand { get; private set; }
        public ICommand FormsClickedCommand { get; private set; }
        public ICommand NewForumClickedCommand { get; private set; }
        public ICommand RegisterUserClickedCommand { get; private set; }
        public ICommand NewFormClickedCommand { get; private set; }
        public ICommand ChangePasswordClickedCommand { get; set; }
        public ICommand PlusButtonClickedCommand { get; set; }

        private float TapCount = 0;

        public float _tapCount {
            get {
                return _tapCount;
            }
            set {
                if (_tapCount != value)
                    _tapCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TapCount"));
            }
        }

        private bool _extraButtonsVisibility;

        public bool ExtraButtonsVisibility {
            get {
                return _extraButtonsVisibility;
            }
            set {
                if (_extraButtonsVisibility != value) {
                    _extraButtonsVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExtraButtonsVisibility"));
                }
            }
        }


        private readonly IPageService _pageService;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppMasterViewModel(IPageService pageService) {
            _pageService = pageService;

            HomeClickedCommand = new Command(async () => await HomeClicked());
            ForumsClickedCommand = new Command(async () => await ForumsClicked());
            UsersClickedCommand = new Command(async () => await UsersClicked());
            FormsClickedCommand = new Command(async () => await FormsClicked());
            NewForumClickedCommand = new Command(async () => await NewForumClicked());
            RegisterUserClickedCommand = new Command(async () => await RegisterUserClicked());
            NewFormClickedCommand = new Command(async () => await NewFormClicked());
            ChangePasswordClickedCommand = new Command(async () => await ChangePasswordClicked());
            PlusButtonClickedCommand = new Command(async () => await PlusButtonClicked());

            ExtraButtonsVisibility = false;
            TapCount = 0;
        }

        private async Task HomeClicked() {
            await _pageService.PushAsync(new AppMasterPage());
        }

        private async Task ForumsClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task UsersClicked() {
            await _pageService.PushAsync(new TestViewUsers());
        }

        private async Task FormsClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task NewForumClicked() {
            await _pageService.PushAsync(new NewForumPage());
        }

        private async Task RegisterUserClicked() {
            await _pageService.PushAsync(new UserRegistrationPage());
        }

        private async Task NewFormClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task ChangePasswordClicked() {
            await _pageService.PushAsync(new ChangePasswordPage());
        }

        private async Task PlusButtonClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            }
            else {
                ExtraButtonsVisibility = true;
            }
        }
    }

}
