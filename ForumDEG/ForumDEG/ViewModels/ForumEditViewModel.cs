using ForumDEG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class ForumEditViewModel {
        private readonly IPageService _pageService;

        public Models.Forum Forum { get; private set; } = new Models.Forum();
        public ICommand CancelCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }

        public ForumEditViewModel(IPageService pageService) {
            _pageService = pageService;

            CancelCommand = new Command(Cancel);
            ConfirmCommand = new Command(() => ConfirmEdition());
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(Forum._title) ||
                    String.IsNullOrWhiteSpace(Forum._place) ||
                    String.IsNullOrWhiteSpace(Forum._schedules));
        }

        public async void ConfirmEdition() {
            if (IsAnyFieldBlank()) {
                EditionFailed();
            } else {
                await EditForum();
                await _pageService.PopAsync();
            }
        }

        public async Task EditForum() {
            //await ForumDatabase.getForumDB.SaveForum(Forum);

            await _pageService.DisplayAlert("Fórum Editado"
                , "O fórum foi editado com sucesso. Os coordenadores serão notificados em breve."
                , "OK", "cancel");
        }

        public async void EditionFailed() {
            await _pageService.DisplayAlert("Erro na Edição"
                , "O fórum não foi editado. Você deve preencher todos os campos."
                , "OK", "cancel");
        }

        public async void Cancel() {
            await _pageService.PopAsync();
        }
    }
}