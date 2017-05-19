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

        public NewDiscursiveQuestionViewModel(IPageService page, IUserDialogs dialog) {
            _page = page;
            _dialog = dialog;

            AddQuestionCommand = new Command(async () => await AddQuestion());
            CancelQuestionCommand = new Command(async () => await CancelQuestion());
        }

        private async Task AddQuestion() {
            if (!VerifyQuestionField()) return;
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