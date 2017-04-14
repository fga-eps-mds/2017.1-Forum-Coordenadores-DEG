using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    class NewForumViewModel {
        private readonly IPageService _pageService;

        public Models.Forum Forum { get; private set; } = new Models.Forum();

        public NewForumViewModel(IPageService pageService) {
            _pageService = pageService;
        }

        public async void CreateForum() {
            await App.ForumDatabase.SaveForum(Forum);
            await _pageService.DisplayAlert("Fórum Criado", "Título: " + Forum._title
                + "\nLocal: " + Forum._place
                + "\nData: " + Forum._date.ToString("dd/MM/yyyy")
                + "\nHora: " + Forum._hour.ToString()
                + "\nPautas:\n" + Forum._schedules
                , "OK");
        }
    }
}
