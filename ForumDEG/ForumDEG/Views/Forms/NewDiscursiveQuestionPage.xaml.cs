using Acr.UserDialogs;
using ForumDEG.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDiscursiveQuestionPage : ContentPage {
        public NewDiscursiveQuestionViewModel _viewModel { get; private set; }

        public NewDiscursiveQuestionPage() {
            _viewModel = new NewDiscursiveQuestionViewModel(new PageService(), UserDialogs.Instance);
            BindingContext = _viewModel;
            InitializeComponent();
        }

        public void AddQuestion() {

        }
    }
}