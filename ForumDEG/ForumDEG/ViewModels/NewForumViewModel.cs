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
    public class NewForumViewModel {
        private readonly IUserDialogs _dialog;
        private readonly IPageService _pageService;
        private readonly IDatabase<Forum> _forumDB;

        public Forum Forum { get; private set; } = new Models.Forum();
        public ICommand CancelCommand { get; private set; }

        public NewForumViewModel(IUserDialogs dialog, IPageService pageService, IDatabase<Forum> forumDB) {
            _dialog = dialog;
            _pageService = pageService;
            _forumDB = forumDB;

            CancelCommand = new Command(Cancel);
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(Forum.Title) ||
                    String.IsNullOrWhiteSpace(Forum.Place) ||
                    String.IsNullOrWhiteSpace(Forum.Schedules));
        }

        public async Task<bool> CreateForum() {
            return (await _forumDB.Save(Forum) == 1);
        }

        public async void AlertSuccess() {
            await _dialog.AlertAsync("O fórum foi criado com sucesso. Os coordenadores serão notificados em breve."
                , "Fórum Criado"
                , "OK");
        }

        public async void SavingFailed() {
            await _dialog.AlertAsync("Não foi possível estabelecer conexão com o banco de dados. Por favor tente novamente."
                , "O fórum não pôde ser criado"
                , "OK");
        }

        public async void CreationFailed() {
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
