using Acr.UserDialogs;
using ForumDEG.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class NewDiscursiveQuestionViewModel {
        public string _questionText { get; set; }

        public ICommand AddQuestionCommand { get; set; }
        public ICommand CancelQuestionCommand { get; set; }

        private readonly IPageService _page;
        private readonly IUserDialogs _dialog;

        NewFormViewModel _formViewModel;

        public NewDiscursiveQuestionViewModel(IPageService page, IUserDialogs dialog, NewFormViewModel formViewModel) {
            _page = page;
            _dialog = dialog;
            _formViewModel = formViewModel;

            AddQuestionCommand = new Command(async () => await AddQuestion());
            CancelQuestionCommand = new Command(async () => await CancelQuestion());
        }

        public bool Validate() {
            return VerifyQuestionField();
        }

        private async Task AddQuestion() {
            if (!Validate()) return;
            _formViewModel.DiscursiveQuestionsTitles.Add(_questionText);
            await _page.PopAsync();
        }

        private async Task CancelQuestion() {
            await _page.PopAsync();
        }

        private bool VerifyQuestionField() {
            if (string.IsNullOrWhiteSpace(_questionText)) {
                _dialog.ShowError("Não é possível ter uma pergunta em branco!");
                return false;
            }
            return true;
        }
    }
}