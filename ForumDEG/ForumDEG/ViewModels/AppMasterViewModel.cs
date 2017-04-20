using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class AppMasterViewModel {
        public ICommand ForumsClickedCommand { get; private set; }
        public ICommand CoordinatorsClickedCommand { get; private set; }
        public ICommand FormsClickedCommand { get; private set; }
        public ICommand NewForumClickedCommand { get; private set; }
        public ICommand RegisterUserClickedCommand { get; private set; }
        public ICommand NewFormClickedCommand { get; private set; }

        private readonly IPageService _pageService;

        public AppMasterViewModel(IPageService pageService) {
            _pageService = pageService;

            ForumsClickedCommand = new Command(async () => await ForumsClicked());
            CoordinatorsClickedCommand = new Command(async () => await CoordinatorsClicked());
            FormsClickedCommand = new Command(async () => await FormsClicked());
            NewForumClickedCommand = new Command(async () => await NewForumClicked());
            RegisterUserClickedCommand = new Command(async () => await RegisterUserClicked());
            NewFormClickedCommand = new Command(async () => await NewFormClicked());
        }
        
        private async Task ForumsClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task CoordinatorsClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task FormsClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task NewForumClicked() {
            await _pageService.PushAsync(new NewForumPage());
        }

        private async Task RegisterUserClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }

        private async Task NewFormClicked() {
            await _pageService.PushAsync(new ForumsPage());
        }
    }
}
