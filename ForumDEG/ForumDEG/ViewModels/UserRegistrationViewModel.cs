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

namespace ForumDEG.ViewModels {
    public class UserRegistrationViewModel : BaseViewModel {
        private readonly IPageService _pageService;

        public ICommand RegisterNewUserCommand { get; private set; }

        public UserRegistrationViewModel(IPageService PageService) {
            _pageService = PageService;
            RegisterNewUserCommand = new Command(async () => await RegisterNewUser());
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

        public bool IsNewUserValid(){
            //verifica se os novos dados são válidos
            return true;
        }

        public async Task RegisterNewUser(){
            if (!HasEmptySpace()){
                if (IsNewUserValid()){
                    if (UserTypeIn == 0){
                        RegisterNewCoordinator();
                    }
                    else{
                        RegisterNewAdministrator();
                    }
                    CleanFields();
                }
                else{
                    await _pageService.DisplayAlert("Erro!", "Dados inseridos inválidos!", "ok", "cancel");
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
            await AdministratorDatabase.getAdmDB.Save(Admin);
            await _pageService.DisplayAlert("Registrar novo usuário", "Você salvou um novo adminstrador com sucesso! ", "ok", "cancel");
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
            await CoordinatorDatabase.getCoordinatorDB.Save(Coord);
            await _pageService.DisplayAlert("Registrar novo usuário", "Você salvou um novo Coordenador com sucesso!", "ok", "cancel");
        }

        public void CleanFields(){
            UserTypeIn = 0;
            NameIn = null;
            RegistrationIn = null;
            EmailIn = null;
            PasswordIn = null;
            CourseIn = null;
        }
    }
}
