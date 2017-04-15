using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels
{
    class UserRegistrationViewModel
    {
        private readonly IPageService _pageService;

        public Models.Coordinator Coordinator { get; private set; } = new Models.Coordinator();

        public UserRegistrationViewModel(IPageService PageService)
        {
            _pageService = PageService;
        }
       

        public async void CreateCoordinator()
        {
            await App.CoordinatorDatabase.SaveCoordinator(Coordinator);

            await _pageService.DisplayAlert("Coordenador Cadastrado", "Coordenador de Curso cadastrado com sucesso!", "OK");
        }

    }
}
