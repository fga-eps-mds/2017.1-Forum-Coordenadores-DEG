using ForumDEG.Models;
using ForumDEG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ForumDEG.ViewModels {
    public class UserEditViewModel : BaseViewModel {


        private readonly IPageService _pageService;
        private readonly Helpers.Administrator _administratorService;
        private readonly Helpers.Coordinator _coordinatorService;

        public User User { get; private set; } = new User();
        public Administrator Administrator { get; private set; } = new Administrator();
        public Coordinator Coordinator { get; private set; } = new Coordinator();

        public ICommand CancelCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }

        int userTypeIn;
        bool isCoord = true;
        string nameIn;
        string registrationIn;
        string emailIn;
        string passwordIn;
        string courseIn = "Coordenador";

        public int UserTypeIn {
            get {
                return userTypeIn;
            }
            set {
                if (userTypeIn != value) {
                    userTypeIn = value;
                    if (userTypeIn == 0) {
                        IsCoord = true;
                    } else {
                        IsCoord = false;
                        CourseIn = null;
                    }
                    OnPropertyChanged("UserTypeIn");
                }
            }
        }

        public bool IsCoord {
            get {
                return isCoord;
            }
            set {
                if (isCoord != value) {
                    isCoord = value;
                    OnPropertyChanged("IsCoord");
                }
            }
        }

        public string NameIn {
            get {
                return nameIn;
            }
            set {
                if (nameIn != value) {
                    nameIn = value;
                    OnPropertyChanged("NameIn");
                }
            }
        }

        public string RegistrationIn {
            get {
                return registrationIn;
            }
            set {
                if (registrationIn != value) {
                    registrationIn = value;
                    OnPropertyChanged("RegistrationIn");
                }
            }
        }

        public string EmailIn {
            get {
                return emailIn;
            }
            set {
                if (emailIn != value) {
                    emailIn = value;
                    OnPropertyChanged("EmailIn");
                }
            }
        }

        public string PasswordIn {
            get {
                return passwordIn;
            }
            set {
                if (passwordIn != value) {
                    passwordIn = value;
                    OnPropertyChanged("PasswordIn");
                }
            }
        }

        public string CourseIn {
            get {
                return courseIn;
            }
            set {
                if (courseIn != value) {
                    courseIn = value;
                    OnPropertyChanged("CourseIn");
                }
            }
        }

        public UserEditViewModel(IPageService pageService, bool IsCoordinator) {
            _pageService = pageService;
            _coordinatorService = new Helpers.Coordinator();
            _administratorService = new Helpers.Administrator();
            isCoord = IsCoordinator;

            CancelCommand = new Command(Cancel);
            ConfirmCommand = new Command(() => ConfirmEdition());
        }

        public async void setOldAdministratorFields(string OldAdministratorId) {
           

            Debug.WriteLine("[Administrator edition]" + OldAdministratorId);
            var _oldAdministrator = await _administratorService.GetAdministratorAsync(OldAdministratorId);

            NameIn = _oldAdministrator.Name;
            EmailIn = _oldAdministrator.Email;
            PasswordIn = _oldAdministrator.Password;
            RegistrationIn = _oldAdministrator.Registration;

        }

        public async void setOldCoordinatorFields(string OldCoordinatorId) {

            Debug.WriteLine("[Coordinator edition]" + OldCoordinatorId);

            var _oldCoordinator = await _coordinatorService.GetCoordinatorAsync(OldCoordinatorId);

            NameIn = _oldCoordinator.Name;
            EmailIn = _oldCoordinator.Email;
            PasswordIn = _oldCoordinator.Password;
            RegistrationIn = _oldCoordinator.Registration;
            CourseIn = _oldCoordinator.Course;
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(NameIn) ||
                    String.IsNullOrWhiteSpace(RegistrationIn) ||
                    String.IsNullOrWhiteSpace(EmailIn) ||
                    String.IsNullOrWhiteSpace(PasswordIn) ||
            (isCoord && String.IsNullOrWhiteSpace(CourseIn)));
        }

        public bool ValidateEmail() {
            //verifica se os novos dados são válidos
            var regex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            var match = Regex.Match(emailIn, regex);

            if (!match.Success) {
                Debug.WriteLine("[User Email]: Invalid!");
                return false;
            } else {
                Debug.WriteLine("[User Email]: Valid!");
                return true;
            }
        }

        public bool ValidatePassword() {
            if (PasswordIn.Length < 8)
                return false;

            var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            var match = Regex.Match(PasswordIn, regex);

            if (!match.Success) {
                Debug.WriteLine("[User Password]: Invalid!");
                return false;
            } else {
                Debug.WriteLine("[User Password]: Valid!");
                return true;
            }
        }

        public async void ConfirmEdition() {
            if (!IsAnyFieldBlank()) {
                if (ValidateEmail()) {
                    if (ValidatePassword()) {
                        await EditUser();
                        await _pageService.PopAsync();

                    } else {
                        await _pageService.DisplayAlert("Erro!", "A senha deve conter de 8 a 15 caracteres, pelo menos uma letra maiúscula e uma minúscula, e pelo menos um número.", "ok", "cancel");
                    }
                } else {
                    await _pageService.DisplayAlert("Erro!", "Email Inválido! Insira-o novamente.", "ok", "cancel");
                }
            } else {
                await _pageService.DisplayAlert("Erro!", "Você deve preencher todos os campos disponíveis!", "ok", "cancel");
            }
        }    
    

        public async Task EditUser() {

            if (isCoord) {
                Coordinator.Name = NameIn;
                Coordinator.Email = EmailIn;
                Coordinator.Registration = RegistrationIn;
                Coordinator.Password = PasswordIn;
                Coordinator.Course = CourseIn;

                if (await _coordinatorService.PutCoordinatorAsync(Coordinator.Registration, Coordinator)) {
                    await _pageService.DisplayAlert("Editar Usuário", "Usuário editado com sucesso!", "OK", "Cancelar");
                } else {
                    await _pageService.DisplayAlert("Editar Usuário", "O usuário selecionado não pôde ser editado. Tente novamente!", "OK", "Cancelar");
                }

            } else {
                Administrator.Name = NameIn;
                Administrator.Email = EmailIn;
                Administrator.Registration = RegistrationIn;
                Administrator.Password = PasswordIn;

            if (await _administratorService.PutAdministratorAsync(Administrator.Registration, Administrator)) {
                await _pageService.DisplayAlert("Editar Usuário", "Usuário editado com sucesso!", "OK", "Cancelar");
            } else {
                await _pageService.DisplayAlert("Editar Usuário", "O usuário selecionado não pôde ser editado. Tente novamente!", "OK", "Cancelar");
            }
            }
        }

        public async void EditionFailed() {
            await _pageService.DisplayAlert("Erro na Edição"
                , "O usuário não foi editado. Você deve preencher todos os campos."
                , "OK", "cancel");
        }

        public async void Cancel() {
            await _pageService.PopAsync();
        }
    }


}
