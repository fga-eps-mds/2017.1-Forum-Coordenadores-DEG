using System;
using ForumDEG.Utils;
using ForumDEG.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Windows.Input;
using Xamarin.Forms;
using ForumDEG.Models;

namespace ForumDEG.ViewModels {
    public class NewForumViewModel : BaseViewModel {
        private readonly IUserDialogs _dialog;
        private readonly IPageService _pageService;
        private readonly IDatabase<Forum> _forumDB;
        private readonly Helpers.Forum _forumService;

        public Forum Forum { get; private set; } = new Models.Forum();
        public ICommand CancelCommand { get; private set; }

        private bool _activityIndicator;
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

        public NewForumViewModel(IUserDialogs dialog, IPageService pageService, IDatabase<Forum> forumDB) {
            ActivityIndicator = false;
            _dialog = dialog;
            _pageService = pageService;
            _forumDB = forumDB;
            _forumService = new Helpers.Forum();

            CancelCommand = new Command(Cancel);
        }

        public bool IsAnyFieldBlank() {
            ActivityIndicator = true;
            return (String.IsNullOrWhiteSpace(Forum.Title) ||
                    String.IsNullOrWhiteSpace(Forum.Place) ||
                    String.IsNullOrWhiteSpace(Forum.Schedules));
        }

        public async Task<bool> CreateForum() {
            ActivityIndicator = true;
            return (await _forumDB.Save(Forum) == 1);
        }

        public async Task<bool> CreateForumRemote() {
            ActivityIndicator = true;
            return await _forumService.PostForumAsync(Forum);
        }

        public async void AlertSuccess() {
            ActivityIndicator = false;
            await _dialog.AlertAsync("O fórum foi criado com sucesso. Os coordenadores serão notificados em breve."
                , "Fórum Criado"
                , "OK");
        }

        public async void SavingFailed() {
            ActivityIndicator = false;
            await _dialog.AlertAsync("Não foi possível estabelecer conexão com o banco de dados. Por favor tente novamente."
                , "O fórum não pôde ser criado"
                , "OK");
        }

        public async void CreationFailed() {
            ActivityIndicator = false;
            await _dialog.AlertAsync("O fórum não pôde ser criado porque existem campos que não foram preenchidos. " +
                    "Verifique e tente novamente.", 
                    "O fórum não pôde ser criado"
                    , "OK");
        }

        public async void Cancel() {
            await _pageService.PopAsync();
        }
    }
}
