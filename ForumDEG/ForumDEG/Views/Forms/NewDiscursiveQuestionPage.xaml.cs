using Acr.UserDialogs;
using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDiscursiveQuestionPage : ContentPage {
        private bool v;
        private NewFormViewModel newFormViewModel;

        public NewDiscursiveQuestionViewModel _viewModel { get; private set; }

        public NewDiscursiveQuestionPage(NewFormViewModel formViewModel) {
            _viewModel = new NewDiscursiveQuestionViewModel(new PageService(), UserDialogs.Instance, formViewModel);
            BindingContext = _viewModel;
            InitializeComponent();
        }

        public void AddQuestion() {

        }
    }
}