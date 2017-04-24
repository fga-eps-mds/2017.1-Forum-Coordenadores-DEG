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

namespace ForumDEG.ViewModels {
    public class NewForumViewModel {
        private readonly IUserDialogs _dialog;
        private readonly IPageService _pageService;

        public Models.Forum Forum { get; private set; } = new Models.Forum();
        public ICommand CancelCommand { get; private set; }

        public NewForumViewModel(IUserDialogs dialog, IPageService pageService) {
            _dialog = dialog;
            _pageService = pageService;

            CancelCommand = new Command(Cancel);
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(Forum._title) ||
                    String.IsNullOrWhiteSpace(Forum._place) ||
                    String.IsNullOrWhiteSpace(Forum._schedules));
        }

        public async void CreateForum() {
            await ForumDatabase.getForumDB.SaveForum(Forum);

            await _dialog.AlertAsync("O fórum foi criado com sucesso. Os coordenadores serão notificados em breve."
                , "Fórum Criado"
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
