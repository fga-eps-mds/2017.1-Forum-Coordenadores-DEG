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
        private PageService _pageService;

        public ObservableCollection<QuestionDetailViewModel> Questions { get; set; }
        private QuestionDetailViewModel _selectedQuestion;
        public QuestionDetailViewModel SelectedQuestion {
            get { return _selectedQuestion; }
            set {
                SetValue(ref _selectedQuestion, value);
                SelectQuestion(_selectedQuestion);
            }
        }

        public ICommand PlusButtonClickedCommand { get; set; }
        public ICommand NewMultipleQuestionCommand { get; set; }
        public ICommand NewMultipleAnswersCommand { get; set; }

        private float TapCount = 0;

        public float _tapCount {
            get {
                return _tapCount;
            }
            set {
                if (_tapCount != value)
                    _tapCount = value;
                OnPropertyChanged("TapCount");
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
                    OnPropertyChanged("ExtraButtonsVisibility");
                }
            }
        }

        public NewFormViewModel(PageService _pageService) { 
            PlusButtonClickedCommand = new Command(async () => await PlusButtonClicked());
            NewMultipleQuestionCommand = new Command(async () => await NewMultipleQuestion());
            NewMultipleAnswersCommand = new Command(async () => await NewMultipleAnswers());
            Questions = new ObservableCollection<QuestionDetailViewModel>();

            this._pageService = _pageService;

            ExtraButtonsVisibility = false;
            TapCount = 0;
        }

        public async void SelectQuestion(QuestionDetailViewModel question) {
            if (question == null)
                return;
            await _pageService.PushAsync(new Views.Forms.QuestionDetailPage(this));
        }

        private async Task PlusButtonClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            } else {
                ExtraButtonsVisibility = true;
            }
        }
        private async Task NewMultipleQuestion() {
           await _pageService.PushAsync(new Views.Forms.NewMultipleQuestionPage(false, this));
        }

        private async Task NewMultipleAnswers() {
            await _pageService.PushAsync(new Views.Forms.NewMultipleQuestionPage(true, this));
        }
    }
}
