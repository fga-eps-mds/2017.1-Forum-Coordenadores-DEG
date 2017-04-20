using System;
using ForumDEG.Utils;
using ForumDEG.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    public class NewForumViewModel {
        private readonly IPageService _pageService;

        public Models.Forum Forum { get; private set; } = new Models.Forum();

        public NewForumViewModel(IPageService pageService) {
            _pageService = pageService;
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(Forum._title) ||
                    String.IsNullOrWhiteSpace(Forum._place) ||
                    String.IsNullOrWhiteSpace(Forum._schedules));
        }

        public async void CreateForum() {
            await ForumDatabase.getForumDB.SaveForum(Forum);
            await _pageService.DisplayAlert("Fórum Criado"
                , "O fórum foi criado com sucesso. Os coordenadores serão notificados em breve."
                , "OK");
        }

        public async void CreationFailed() {
            await _pageService.DisplayAlert("O fórum não pôde ser criado"
                    , "O fórum não pôde ser criado porque existem campos que não foram preenchidos. " +
                    "Verifique e tente novamente."
                    , "OK");
        }
    }
}
