using Acr.UserDialogs;
using ForumDEG.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class NewFormViewModel : BaseViewModel {
        private IPageService _pageService;
        private Helpers.Form _formService;

        public ObservableCollection<QuestionDetailViewModel> MultipleChoiceQuestions { get; set; }
        public ObservableCollection<string> DiscursiveQuestionsTitles { get; set; }
        public string Title { get; set; }
        private QuestionDetailViewModel _selectedQuestion;
        public QuestionDetailViewModel SelectedQuestion {
            get { return _selectedQuestion; }
            set {
                SetValue(ref _selectedQuestion, value);
                SelectQuestion(_selectedQuestion);
            }
        }

        private bool _activityIndicator = false;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    OnPropertyChanged("ActivityIndicator");
                }
            }
        }

        public ICommand PlusButtonClickedCommand { get; set; }
        public ICommand NewMultipleQuestionCommand { get; set; }
        public ICommand NewMultipleAnswersCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveQuestionCommand { get; set; }
        public ICommand NewDiscursiveQuestionCommand { get; set; }
        private readonly IUserDialogs _dialog;

        public NewFormViewModel(IUserDialogs dialog, IPageService _pageService) {
            ActivityIndicator = false;
            NewMultipleQuestionCommand = new Command(async () => await NewMultipleQuestion());
            NewMultipleAnswersCommand = new Command(async () => await NewMultipleAnswers());
            NewDiscursiveQuestionCommand = new Command(async () => await NewDiscursiveQuestion());
            DeleteQuestionCommand = new Command(async () => await DeleteQuestion());
            CancelCommand = new Command(async () => await Cancel());
            SaveQuestionCommand = new Command(async () => await SaveQuestion());


            MultipleChoiceQuestions = new ObservableCollection<QuestionDetailViewModel>();
            DiscursiveQuestionsTitles = new ObservableCollection<string>();

            _formService = new Helpers.Form();
            this._pageService = _pageService;

            _dialog = dialog;
        }

        public bool IsFieldBlank(string field) {
            return (String.IsNullOrEmpty(field) || String.IsNullOrWhiteSpace(field));
        }


        public async void SelectQuestion(QuestionDetailViewModel question) {
            if (question == null)
                return;
            await _pageService.PushAsync(new Views.Forms.QuestionDetailPage(this));
        }

        private async Task NewDiscursiveQuestion() {
            await _pageService.PushAsync(new Views.Forms.NewDiscursiveQuestionPage(this));
        }

        private async Task NewMultipleQuestion() {
           await _pageService.PushAsync(new Views.Forms.NewMultipleQuestionPage(false, this));
        }

        private async Task NewMultipleAnswers() {
            await _pageService.PushAsync(new Views.Forms.NewMultipleQuestionPage(true, this));
        }

        private async Task DeleteQuestion() {
            MultipleChoiceQuestions.Remove(SelectedQuestion);
            SelectedQuestion = null;
            await _pageService.PopAsync();
        }

        private async Task Cancel() {
            await _pageService.PopAsync();
        }

        private async Task SaveQuestion() {
            ActivityIndicator = true;
            if (IsFieldBlank(Title)) {
                ActivityIndicator = false;
                await _pageService.DisplayAlert("Formulário não pode ser criado", "O formulário deve possuir título", "ok");
                return;
            } else if (AreQuestionsListsEmpty()) {
                ActivityIndicator = false;
                await _pageService.DisplayAlert("Formulário não pode ser criado", "É necessário criar ao menos uma pergunta", "ok");
                return; 
            } else if (await _formService.PostFormAsync(this)) {
                ActivityIndicator = false;
                await _dialog.AlertAsync("O formulário foi criado com sucesso. Os coordenadores serão notificados em breve."
                , "Formulário Criado"
                , "OK");
                await _pageService.PopAsync();
            } else {
                ActivityIndicator = false;
                await _pageService.DisplayAlert("Formulário não pode ser criado", "Não foi possível estabelecer" +
                                           " conexão com o banco de dados. Por favor tente novamente.", "ok");
            }
            
        }
        
        public bool AreQuestionsListsEmpty() {
            if (MultipleChoiceQuestions.Count == 0 && DiscursiveQuestionsTitles.Count == 0) {
                return true;
            }
            return false;
        }
    }
}
