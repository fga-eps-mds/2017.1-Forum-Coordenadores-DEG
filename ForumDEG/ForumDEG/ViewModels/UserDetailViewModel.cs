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
using System.Diagnostics;

namespace ForumDEG.ViewModels {
    public class UserDetailViewModel : BaseViewModel{
        private readonly IPageService _pageService;
        private readonly Helpers.Administrator _administratorService;
        private readonly Helpers.Coordinator _coordinatorService;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int ForumsPresent { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsCoordinator { get; set; }

        private bool _activityIndicator = false;
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

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public bool IsCurrentUserAdmin => Helpers.Settings.IsUserAdmin;

        public UserDetailViewModel(IPageService PageService) {
            ActivityIndicator = false;
            _pageService = PageService;
            EditCommand = new Command(EditUser);
            DeleteCommand = new Command(DeleteUser);
            _administratorService = new Helpers.Administrator();
            _coordinatorService = new Helpers.Coordinator();
        }

        public void EditUser() {
            _pageService.PushAsync(new UserEditPage(Registration, IsCoordinator));
        }

        private async void DeleteUser() {
            //Administrator _toDeleteAdmin;
           // Coordinator _toDeleteCoord;
            //bool answer;

            if (IsAdministrator) {
                var answer = await _pageService.DisplayAlert("Deletar Usuário", "Tem certeza que deseja deletar esse usuário? Esta ação não poderá ser desfeita.", "Sim", "Não");
                Debug.WriteLine("Answer: " + answer);
                if (answer == true) {
                    ActivityIndicator = true;
                    if (await _administratorService.DeleteAdministratorAsync(Registration)) {
                        ActivityIndicator = false;
                        await _pageService.DisplayAlert("Usuário deletado", "O usuário foi excluído do sistema com sucesso.", "OK");
                        await _pageService.PopAsync();
                    } else {
                        ActivityIndicator = false;
                        await _pageService.DisplayAlert("Erro!", "O usuário não pôde ser deletado, tente novamente.", "OK");
                    }
                }
                      //LOCAL DATABASE//
                //_toDeleteAdmin = await AdministratorDatabase.getAdmDB.Get(Id);
                //answer = await _pageService.DisplayAlert("Deletar Administrador",
                //    "Tem certeza que deseja deletar o Administrador " + _toDeleteAdmin.Name + "?\nEsta ação não poderá ser desfeita.", "Sim", "Não");
                //if (answer == true) {
                //    await AdministratorDatabase.getAdmDB.Delete(_toDeleteAdmin);
                //    await _pageService.PopAsync();
                //} else {
                //    //do nothing
                //}
            } else {
                var answer = await _pageService.DisplayAlert("Deletar Usuário", "Tem certeza que deseja deletar esse usuário? Esta ação não poderá ser desfeita.", "Sim", "Não");
                Debug.WriteLine("Answer: " + answer);
                if (answer == true) {
                    ActivityIndicator = true;
                    if (await _coordinatorService.DeleteCoordinatorAsync(Registration)) {
                        ActivityIndicator = false;
                        await _pageService.PopAsync();
                    } else {
                        ActivityIndicator = false;
                        await _pageService.DisplayAlert("Erro!", "O usuário não pôde ser deletado, tente novamente.", "OK", "Cancelar");
                    }
                }
                //LOCAL DATABASE//
                //_toDeleteCoord = await CoordinatorDatabase.getCoordinatorDB.Get(Id);
                //answer = await _pageService.DisplayAlert("Deletar Coordenador",
                //    "Tem certeza que deseja deletar o Coordenador " + _toDeleteCoord.Name + "?\nEsta ação não poderá ser desfeita.", "Sim", "Não");
                //if (answer == true) {
                //    await CoordinatorDatabase.getCoordinatorDB.Delete(_toDeleteCoord);
                //    await _pageService.PopAsync();
                //} else {
                //    //do nothing
                //}
            }
        }
    }
}
