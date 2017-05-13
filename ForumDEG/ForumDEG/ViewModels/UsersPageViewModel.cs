using ForumDEG.Interfaces;
using ForumDEG.Models;
using ForumDEG.Utils;
using ForumDEG.Views;
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
    public class UsersPageViewModel : BaseViewModel {
        public ObservableCollection<AdministratorDetailPageViewModel> Administrators { get; private set; }
        public ObservableCollection<CoordinatorDetailPageViewModel> Coordinators { get; private set; }

        private AdministratorDetailPageViewModel _selectedAdministrator;
        public AdministratorDetailPageViewModel SelectedAdministrator {
            get { return _selectedAdministrator; }
            set { SetValue(ref _selectedAdministrator, value); }
        }

        private CoordinatorDetailPageViewModel _selectedCoordinator;
        public CoordinatorDetailPageViewModel SelectedCoordinator {
            get { return _selectedCoordinator; }
            set { SetValue(ref _selectedCoordinator, value); }
        }

        private readonly IPageService _pageService;
        private readonly Helpers.Coordinator _coordinatorService;
        private readonly Helpers.Administrator _administratorService;

        public ICommand SelectAdministratorCommand { get; private set; }
        public ICommand SelectCoordinatorCommand { get; private set; }

        private static UsersPageViewModel _instance = null;

        private UsersPageViewModel(IPageService pageService) {
            _pageService = pageService;
            _coordinatorService = new Helpers.Coordinator();
            _administratorService = new Helpers.Administrator();

            SelectAdministratorCommand = new Command<AdministratorDetailPageViewModel>(async vm => await SelectAdministrator(vm));
            SelectCoordinatorCommand = new Command<CoordinatorDetailPageViewModel>(async vm => await SelectCoordinator(vm));
        }

        public static UsersPageViewModel GetInstance() {
            if (_instance == null) _instance = new UsersPageViewModel(new PageService());
            return _instance;
        }

        private async Task SelectAdministrator(AdministratorDetailPageViewModel administrator) {
            if (administrator == null)
                return;
            SelectedAdministrator = administrator;
            await _pageService.PushAsync(new ForumDetailPage());
        }

        private async Task SelectCoordinator(CoordinatorDetailPageViewModel coordinator) {
            if (coordinator == null)
                return;
            SelectedCoordinator = coordinator;
            await _pageService.PushAsync(new ForumDetailPage());
        }

        public async void UpdateUsersLists() {
            Administrators = new ObservableCollection<AdministratorDetailPageViewModel>();
            Coordinators = new ObservableCollection<CoordinatorDetailPageViewModel>();

            try {
                var administratorsList = await _administratorService.GetAdministratorsAsync();
                var coordinatorsList = await _coordinatorService.GetCoordinatorsAsync();

                foreach (Coordinator coordinator in coordinatorsList) {
                    Coordinators.Add(new CoordinatorDetailPageViewModel {
                        Name = coordinator.Name,
                        Id = coordinator.Id,
                        Password = coordinator.Password,
                        Email = coordinator.Email,
                        Registration = coordinator.Registration,
                        Course = coordinator.Course
                    });
                }

                foreach (Administrator administrator in administratorsList) {
                    Administrators.Add(new AdministratorDetailPageViewModel {
                        Name = administrator.Name,
                        Id = administrator.Id,
                        Password = administrator.Password,
                        Email = administrator.Email,
                        Registration = administrator.Registration
                    });
                }
            } catch (Exception ex) {
                Debug.WriteLine("[Update users list] " + ex.Message);
                await _pageService.DisplayAlert("Falha ao carregar usuários",
                                          "Houve um erro ao estabelecer conexão com o servidor. Por favor, tente novamente.",
                                          "Ok", "Cancel");
                await _pageService.PopAsync();
            }
        }
    }
}
