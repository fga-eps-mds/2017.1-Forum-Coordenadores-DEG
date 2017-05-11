using ForumDEG.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailPage : ContentPage {
        private Helpers.Forum _forumService;
        private Models.Forum forum;

        public ForumDetailPage() {
            //BindingContext = ForumsViewModel.GetInstance().SelectedForum;
            _forumService = new Helpers.Forum();
            forum = new Models.Forum();

            getForum();
        }

        private async void getForum() {
            forum = await _forumService.GetForumAsync("b3153003-b444-4d94-b4a5-b3b749ae6726");
            BindingContext = new ForumDetailViewModel {
                Title = forum.Title,
                Place = forum.Place,
                Schedules = forum.Schedules,
                Date = forum.Date,
                Hour = forum.Hour
            };
            InitializeComponent();
        }
    }
}
