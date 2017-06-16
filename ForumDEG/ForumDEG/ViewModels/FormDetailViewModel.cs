using ForumDEG.Helpers;
using ForumDEG.Interfaces;
using ForumDEG.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class FormDetailViewModel : PageService, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private IPageService _pageService;
        private readonly Helpers.Form _formService;

        public bool IsCurrentUserAdmin => Helpers.Settings.IsUserAdmin;
        public bool IsCurrentUserCoord => Helpers.Settings.IsUserCoord;


        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string Title { get; set; }

        public int QuestionsAmount {
            get { return (DiscursiveQuestions.Count + MultipleChoiceQuestions.Count); }
        }

        private bool _activityIndicator = false;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivityIndicator"));
                }
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand DeleteCommand { get; private set; }

        public List<Models.DiscursiveQuestion> DiscursiveQuestions { get; set; }
        public List<Models.MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public List<Models.MultipleAnswersQuestion> MultipleAnswersQuestions { get; set; }
        public List<Models.SingleAnswerQuestion> SingleAnswerQuestions { get; set; }

        public FormDetailViewModel(IPageService pageService) {
            ActivityIndicator = false;
            _pageService = pageService;
            MultipleAnswersQuestions = new List<Models.MultipleAnswersQuestion>();
            SingleAnswerQuestions = new List<Models.SingleAnswerQuestion>();

            CancelCommand = new Command(async () => await Cancel());
            SubmitCommand = new Command(Submit);
            _formService = new Helpers.Form();
            DeleteCommand = new Command(DeleteForm);
        }
        public void SplitMultipleChoiceQuestions() {
            ActivityIndicator = true;
            var multipleChoiceQuestions = MultipleChoiceQuestions;

            foreach (Models.MultipleChoiceQuestion multipleQuestion in multipleChoiceQuestions) {
                if (multipleQuestion.MultipleAnswers) {
                    var multipleAnswerQuestion = new Models.MultipleAnswersQuestion(multipleQuestion.Question);
                    foreach (Models.Option option in multipleQuestion) {
                        multipleAnswerQuestion.Add(option);
                    }
                    MultipleAnswersQuestions.Add(multipleAnswerQuestion);
                } else if (!multipleQuestion.MultipleAnswers) {
                    var singleAnswerQuestion = new Models.SingleAnswerQuestion {
                        Question = multipleQuestion.Question,
                        Options = new ObservableCollection<string>()
                    };
                    foreach (Models.Option option in multipleQuestion) {
                        singleAnswerQuestion.Options.Add(option.OptionText);
                    }
                    SingleAnswerQuestions.Add(singleAnswerQuestion);
                }
            }
            ActivityIndicator = false;
        }

        private async void Submit() {
            ActivityIndicator = true;

            List<MultipleChoiceAnswer> multipleChoiceAnswers = new List<MultipleChoiceAnswer>();

            foreach (DiscursiveQuestion discursiveQuestion in DiscursiveQuestions) {
                Debug.WriteLine("[Submit] Question: " + discursiveQuestion.Question);
                Debug.WriteLine("[Submit] Answer: " + discursiveQuestion.Answer);
            }

            if ((CheckBoxValidation(multipleChoiceAnswers) == false) || (RadioButtonValidation(multipleChoiceAnswers) == false)) {
                await blankAnswerAsync();
                return;
            }



            FormAnswer formAnswer = new FormAnswer {
                FormId = RemoteId,
                CoordinatorId = Settings.UserReg,
                DiscursiveAnswers = DiscursiveQuestions,
                MultipleChoiceAnswers = multipleChoiceAnswers
            };

            /* Uncomment after API is implemented
             * await _formService.PostFormAnswerAsync(formAnswer);
             */

            ActivityIndicator = false;
        }

        public bool CheckBoxValidation(List<MultipleChoiceAnswer> multipleChoiceAnswers) {
            ActivityIndicator = true;

            foreach (MultipleAnswersQuestion checkBoxQuestion in MultipleAnswersQuestions) {
                Debug.WriteLine("[Submit] Question: " + checkBoxQuestion.Question);
                List<string> selectedOptions = new List<string>();

                bool validateCheckbox = false;
                Debug.WriteLine("[Validate submit] " + validateCheckbox);

                foreach (Option option in checkBoxQuestion) {
                    if (option.IsSelected) {
                        validateCheckbox = true;
                        selectedOptions.Add(option.OptionText);
                        Debug.WriteLine("[Submit] Answer: " + option.OptionText);
                    }
                }
                if (validateCheckbox == false) {
                    ActivityIndicator = false;
                    return false;
                }

                MultipleChoiceAnswer answer = new MultipleChoiceAnswer {
                    Question = checkBoxQuestion.Question,
                    Answers = selectedOptions
                };
                multipleChoiceAnswers.Add(answer);
            }

            ActivityIndicator = false;
            return true;
        }

        public bool RadioButtonValidation(List<MultipleChoiceAnswer> multipleChoiceAnswers) {
            ActivityIndicator = true;
            foreach (SingleAnswerQuestion radioButtonQuestion in SingleAnswerQuestions) {
                if (radioButtonQuestion.SelectedOption == -1) {
                    Debug.WriteLine("nenhuma questï¿½o selecionada");
                    ActivityIndicator = false;
                    return false;
                }

                int answerIndex = radioButtonQuestion.SelectedOption;

                MultipleChoiceAnswer answer = new MultipleChoiceAnswer {
                    Question = radioButtonQuestion.Question,
                    Answers = new List<string> { radioButtonQuestion.Options[answerIndex] }
                };

                multipleChoiceAnswers.Add(answer);

                Debug.WriteLine("[Submit] Question: " + radioButtonQuestion.Question);
                Debug.WriteLine("[Submit] Answer: " + radioButtonQuestion.Options[answerIndex]);
            }
            ActivityIndicator = false;
            return true;
        }

        private async Task Cancel() {
            await _pageService.PopAsync();
        }

        public async Task blankAnswerAsync() {
            ActivityIndicator = false;
            await _pageService.DisplayAlert("Erro!", "Voce deve selecionar pelo menos uma opção!", "ok", "cancel");
        }

        private async void DeleteForm() {
            var answer = await _pageService.DisplayAlert("Deletar Formulário", "Tem certeza que deseja deletar o Formulário existente? Esta alteração não poderá ser desfeita.", "Sim", "Não");
            ActivityIndicator = true;
            Debug.WriteLine("Answer: " + answer);
            if (answer == true) {
                if (await _formService.DeleteFormAsync(RemoteId)) {
                    ActivityIndicator = false;
                    await _pageService.DisplayAlert("Formulário Deletado !", "O Formulário foi deletado com sucesso.", null, "OK");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await _pageService.PopAsync();
                }
                else {
                    ActivityIndicator = false;
                    await _pageService.DisplayAlert("Erro!", "O formulário não pode ser deletado, tente novamente.", "OK", "Cancelar");
                }
            }
            ActivityIndicator = false;
        }
    }
}
