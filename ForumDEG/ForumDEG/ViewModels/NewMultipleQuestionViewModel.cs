using Acr.UserDialogs;
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
    public class NewMultipleQuestionViewModel : BaseViewModel {

        public ObservableCollection<string> Options { get; set; }

        private string _selectedOption;
        public string SelectedOption {
            get { return _selectedOption; }
            set {
                SetValue(ref _selectedOption, value);
                RemoveOption();
            }
        }
        public string Title { get; set; }

        private string _optionEntry;
        public string OptionEntry { 
           get {
               return _optionEntry;
           }
           set {
               if (_optionEntry != value) {
                   _optionEntry = value;
                    OnPropertyChanged("OptionEntry");

               }
           }
       }
        private bool _multipleAnswers { get; set; }
        private PageService _pageService;
        private IUserDialogs _dialog;
        private NewFormViewModel _formViewModel;

        public ICommand CancelCommand { get; set; }
        public ICommand AddOptionCommand { get; set; }
        public ICommand RemoveOptionCommand { get; set; }
        public ICommand SaveQuestionCommand { get; set; }

        public NewMultipleQuestionViewModel (bool _multipleAnswers, 
                                             PageService _pageService, 
                                             IUserDialogs dialog, 
                                             NewFormViewModel formViewModel) {
            this._pageService = _pageService;
            _dialog = dialog;
            _formViewModel = formViewModel;


            Options = new ObservableCollection<string> { "One", "Two", "Three" };

            CancelCommand = new Command(async () => await Cancel());
            AddOptionCommand = new Command(AddOption);
            RemoveOptionCommand = new Command(RemoveOption);
            SaveQuestionCommand = new Command(async () => await SaveQuestion());
        }

        public bool IsFieldBlank(string field) {
            return (String.IsNullOrEmpty(field) || String.IsNullOrWhiteSpace(field));
        }

        public bool IsOptionsListEmpty() {
            return Options.Count == 0;
        }

        public void AddOption() {
            if (IsFieldBlank(OptionEntry)) {
                _dialog.Alert(message: "Não se pode adicionar uma opção vazia!", okText: "OK");

            } else {
                Options.Add(OptionEntry);
                OptionEntry = null;
            }
        }

        public void RemoveOption() {
            Options.Remove(SelectedOption);
        }

        public async Task SaveQuestion() {
            if (IsFieldBlank(Title)) {
                _dialog.Alert(message: "A pergunta deve possuir título!", okText: "OK");
            } else if (IsOptionsListEmpty()) {
                _dialog.Alert(message: "A pergunta deve possuir opções!", okText: "OK");
            } else {

                _formViewModel.MultipleChoiceQuestions.Add(new QuestionDetailViewModel {
                    Title = this.Title,
                    Options = this.Options,
                    MultipleAnswers = _multipleAnswers
                });
                await _pageService.PopAsync();
            }
        }

        public async Task Cancel() {
            await _pageService.PopAsync();
        }

        
    }
}
