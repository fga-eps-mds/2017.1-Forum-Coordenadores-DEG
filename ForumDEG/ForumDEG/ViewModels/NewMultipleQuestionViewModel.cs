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

        public ICommand CancelCommand { get; set; }
        public ICommand AddOptionCommand { get; set; }
        public ICommand RemoveOptionCommand { get; set; }

        public NewMultipleQuestionViewModel (bool _multipleAnswers, PageService _pageService, IUserDialogs dialog) {
            this._pageService = _pageService;
            _dialog = dialog;


            Options = new ObservableCollection<string> { "One", "Two", "Three" };

            CancelCommand = new Command(async () => await Cancel());
            AddOptionCommand = new Command(AddOption);
            RemoveOptionCommand = new Command(RemoveOption);

        }

        public void AddOption() {
            if(String.IsNullOrEmpty(OptionEntry) || String.IsNullOrWhiteSpace(OptionEntry)) {
                _dialog.Alert(message: "Não se pode adicionar uma opção vazia!", okText: "OK");

            } else {
                Options.Add(OptionEntry);
                OptionEntry = null;
            }

        }

        public void RemoveOption() {
            Options.Remove(SelectedOption);
        }

        public async Task Cancel() {
            await _pageService.PopAsync();
        }

        
    }
}
