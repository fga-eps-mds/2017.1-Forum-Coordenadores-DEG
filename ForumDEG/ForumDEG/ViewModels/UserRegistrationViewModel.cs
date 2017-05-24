using ForumDEG.Models;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ForumDEG.Interfaces;
using ForumDEG.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ForumDEG.Views;

namespace ForumDEG.ViewModels {
    public class UserRegistrationViewModel : BaseViewModel {
        private readonly IPageService _pageService;
        private readonly Helpers.Coordinator _coordinatorService;
        private readonly Helpers.Administrator _administratorService;

        public ICommand RegisterNewUserCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public UserRegistrationViewModel(IPageService PageService) {
            _pageService = PageService;
            _coordinatorService = new Helpers.Coordinator();
            _administratorService = new Helpers.Administrator();

            RegisterNewUserCommand = new Command(async () => await RegisterNewUser());
            CancelCommand = new Command(async () => await Cancel());
        }

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

        public bool HasEmptySpace() {
            if (String.IsNullOrWhiteSpace(NameIn) ||
                String.IsNullOrWhiteSpace(RegistrationIn) ||
                String.IsNullOrWhiteSpace(EmailIn) ||
                String.IsNullOrWhiteSpace(PasswordIn)) {
              return true;
            } else {
                if(UserTypeIn == 0) {
                    return (String.IsNullOrWhiteSpace(CourseIn));
                } else {
                    return false;
                }
            }
        }

        public bool ValidateRegisterNumber(){
            var regex = @"^[0-9]{9}$";
            var match = Regex.Match(RegistrationIn, regex);

            if (!match.Success) {
                Debug.WriteLine("[User Registration]: Invalid!");
                return false;
            } else {
                Debug.WriteLine("[User Registration]: Valid!");
                return true;
            }   
        }
       

        public bool ValidateEmail(){
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

        public async Task RegisterNewUser(){
            if (!HasEmptySpace()){
                if (ValidateEmail()){
                    if (ValidateRegisterNumber()){
                        if (ValidatePassword()) {
                            if (UserTypeIn == 0){
                             RegisterNewCoordinator();
                                }
                            else {
                             RegisterNewAdministrator();
                                }       
                    CleanFields();
                        } else {
                            await _pageService.DisplayAlert("Erro!", "A senha deve conter de 8 a 15 caracteres, pelo menos uma letra maiúscula e uma minúscula, e pelo menos um número.", "ok", "cancel");
                        }

                    } else {
                        await _pageService.DisplayAlert("Erro!", "Matrícula Inválida!", "ok", "cancel");
                    }
                }
                else{
                    await _pageService.DisplayAlert("Erro!", "Email Inválido! Insira-o novamente.", "ok", "cancel");
                }
            }
            else{
                await _pageService.DisplayAlert("Erro!", "Você deve preencher todos os campos disponíveis!", "ok", "cancel");
            }
        }


        public async void RegisterNewAdministrator(){
            Administrator Admin = new Administrator(){
                Name = NameIn,
                Email = EmailIn,
                Registration = RegistrationIn,
                Password = PasswordIn,
                CreatedOn = DateTime.Now
            };
            if (await _administratorService.PostAdministratorAsync(Admin)) {
                await _pageService.DisplayAlert("Registrar novo usuário", 
                                                "Você salvou um novo adminstrador com sucesso! ", 
                                                "ok", "cancel");
            } else {
                await _pageService.DisplayAlert("Falha na conexão com o servidor",
                                                "Não foi possível cadastrar o administrador. Por favor tente novamente.",
                                                "ok", "cancel");
                Debug.WriteLine("[URVM] Couldn't save.");
            }
        }

        public async void RegisterNewCoordinator(){
            Coordinator Coord = new Coordinator(){
                Name = NameIn,
                Email = EmailIn,
                Registration = RegistrationIn,
                Password = PasswordIn,
                CreatedOn = DateTime.Now,
                Course = CourseIn
            };
            if (await _coordinatorService.PostCoordinatorAsync(Coord)) {
                await _pageService.DisplayAlert("Registrar novo usuário", 
                                                "Você salvou um novo Coordenador com sucesso!", 
                                                "ok", "cancel");
            } else {
                await _pageService.DisplayAlert("Falha na conexão com o servidor",
                                                "Não foi possível cadastrar o coordenador. Por favor tente novamente.",
                                                "ok", "cancel");
                Debug.WriteLine("[URVM] Couldn't save.");
            }
        }

        public void CleanFields(){
            UserTypeIn = 0;
            NameIn = null;
            RegistrationIn = null;
            EmailIn = null;
            PasswordIn = null;
            CourseIn = null;
        }

        private async Task Cancel() {
            CleanFields();
           await _pageService.PopAsync();
        }
    }
}
