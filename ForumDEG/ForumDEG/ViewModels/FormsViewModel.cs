using ForumDEG.Interfaces;
using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class FormsViewModel : BaseViewModel {
        public ObservableCollection<FormDetailViewModel> Forms { get; private set; }

        private FormDetailViewModel _selectedForm;
        public FormDetailViewModel SelectedForm {
            get { return _selectedForm; }
            set { SetValue(ref _selectedForm, value); }
        }

        private readonly IPageService _pageService;
        private readonly Helpers.Form _formService;

        public ICommand SelectFormCommand { get; private set; }

        private static FormsViewModel _instance = null;
        public FormsViewModel(IPageService pageService) {
            _pageService = pageService;
            _formService = new Helpers.Form();
            SelectFormCommand = new Command<FormDetailViewModel>(async vm => await SelectForm(vm));
        }

        public static FormsViewModel GetInstance() {
            if (_instance == null)
                _instance = new FormsViewModel(new PageService());
            return _instance;
        }

        public async Task SelectForm(FormDetailViewModel form) {
            if (form == null)
                return;
            SelectedForm = form;
            await _pageService.PushAsync(new Views.Forms.FormDetailPage(SelectedForm));
        }

        public async void UpdateFormsList() {
            Forms = new ObservableCollection<FormDetailViewModel>();
            try {
                var formsList = await _formService.GetFormsAsync();

                foreach (Form _form in formsList) {
                    var formViewModel = new FormDetailViewModel(new PageService()) {
                        Id = _form.Id,
                        RemoteId = _form.RemoteId,
                        Title = _form.Title,
                        DiscursiveQuestions = _form.DiscursiveQuestions,
                        MultipleChoiceQuestions = _form.MultipleChoiceQuestions
                    };
                    formViewModel.SplitMultipleChoiceQuestions();
                    Forms.Add(formViewModel);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine("[Update forms list] " + ex.Message + "\n" + ex.StackTrace);
                await _pageService.DisplayAlert("Falha ao carregar formulários",
                                          "Houve um erro ao estabelecer conexão com o servidor. Por favor, tente novamente.",
                                          "Ok", "Cancel");
                await _pageService.PopAsync();
            }
        }
    }
}
