using ForumDEG.Interfaces;
using ForumDEG.Utils;
using ForumDEG.Models;
using ForumDEG.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class UserDetailViewModel {
        private readonly IPageService _pageService;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int ForumsPresent { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsCoordinator { get; set; }
        public bool IsLoggedUserAdmin { get; set; }

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public UserDetailViewModel(IPageService PageService) {
            _pageService = PageService;
            SetLoggedUserType();
            EditCommand = new Command(EditUser);
            DeleteCommand = new Command(DeleteUser);
        }

        public void SetLoggedUserType() {
            //TO DO
            //if logge user is admin set IsLoggedUserAdmin to true else set to false
            IsLoggedUserAdmin = true;
        }

        public void EditUser() {
            //_pageService.PushAsync(new UserEditPage());
        }

        public async void DeleteUser() {
            Administrator _toDeleteAdmin;
            Coordinator _toDeleteCoord;
            bool answer;

            if (IsAdministrator) {
                _toDeleteAdmin = await AdministratorDatabase.getAdmDB.Get(Id);
                answer = await _pageService.DisplayAlert("Deletar Administrador",
                    "Tem certeza que deseja deletar o Administrador " + _toDeleteAdmin.Name + "?\nEsta ação não poderá ser desfeita.", "Sim", "Não");
                if (answer == true) {
                    await AdministratorDatabase.getAdmDB.Delete(_toDeleteAdmin);
                    await _pageService.PopAsync();
                } else {
                    //do nothing
                }
            } else {
                _toDeleteCoord = await CoordinatorDatabase.getCoordinatorDB.Get(Id);
                answer = await _pageService.DisplayAlert("Deletar Coordenador",
                    "Tem certeza que deseja deletar o Coordenador " + _toDeleteCoord.Name + "?\nEsta ação não poderá ser desfeita.", "Sim", "Não");
                if (answer == true) {
                    await CoordinatorDatabase.getCoordinatorDB.Delete(_toDeleteCoord);
                    await _pageService.PopAsync();
                } else {
                    //do nothing
                }
            }
        }
    }
}
