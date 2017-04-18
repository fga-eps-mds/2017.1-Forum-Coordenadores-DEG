using ForumDEG.Models;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels
{
    class UserRegistrationViewModel : INotifyPropertyChanged{
        private readonly IPageService _pageService;

        public UserRegistrationViewModel(IPageService PageService){
            _pageService = PageService;
        }

        int userTypeIn;
        string nameIn;
        string registrationIn;
        string emailIn;
        string passwordIn;
        string courseIn;

        public int UserTypeIn{
            get{
                return userTypeIn;
            }
            set{
                if (userTypeIn != value){
                    userTypeIn = value;
                    OnPropertyChanged("UserTypeIn");
                }
            }
        }

        public string NameIn{
            get{
                return nameIn;
            }
            set{
                if (nameIn != value){
                    nameIn = value;
                    OnPropertyChanged("NameIn");
                }
            }
        }

        public string RegistrationIn{
            get{
                return registrationIn;
            }
            set{
                if (registrationIn != value){
                    registrationIn = value;
                    OnPropertyChanged("RegistrationIn");
                }
            }
        }

        public string EmailIn{
            get{
                return emailIn;
            }
            set{
                if (emailIn != value){
                    emailIn = value;
                    OnPropertyChanged("EmailIn");
                }
            }
        }

        public string PasswordIn{
            get{
                return passwordIn;
            }
            set{
                if (passwordIn != value){
                    passwordIn = value;
                    OnPropertyChanged("PasswordIn");
                }
            }
        }

        public string CourseIn{
            get{
                return courseIn;
            }
            set{
                if (courseIn != value){
                    courseIn = value;
                    OnPropertyChanged("CourseIn");
                }
            }
        }

        public bool HasEmptySpace(){
            if (UserTypeIn == 0){
                return (String.IsNullOrWhiteSpace(NameIn) ||
                        String.IsNullOrWhiteSpace(RegistrationIn) ||
                        String.IsNullOrWhiteSpace(EmailIn) ||
                        String.IsNullOrWhiteSpace(PasswordIn) ||
                        String.IsNullOrWhiteSpace(CourseIn));
            }
            else{
                return (String.IsNullOrWhiteSpace(NameIn) ||
                        String.IsNullOrWhiteSpace(RegistrationIn) ||
                        String.IsNullOrWhiteSpace(EmailIn) ||
                        String.IsNullOrWhiteSpace(PasswordIn));
            }

        }

        public bool IsNewUserValid(){
            //verifica se os novos dados são válidos
            return true;
        }

        public async void RegisterNewUser(){

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
                    await _pageService.DisplayAlert("Erro!", "Você deve preencher todos os campos disponíveis!", "ok");
                }
            }
            else{
                await _pageService.DisplayAlert("Erro!", "Você deve preencher todos os campos disponíveis!", "ok");
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
            await App.AdministratorDatabase.SaveAdministrator(Admin);
            await _pageService.DisplayAlert("Registrar novo usuário", "Você salvou um novo adminstrador com sucesso! ", "ok");
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
            await App.CoordinatorDatabase.SaveCoordinator(Coord);
            await _pageService.DisplayAlert("Registrar novo usuário", "Você salvou um novo Coordenador com sucesso!", "ok");
        }

        public void CleanFields(){
            UserTypeIn = 0;
            NameIn = null;
            RegistrationIn = null;
            EmailIn = null;
            PasswordIn = null;
            CourseIn = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName){
            var changed = PropertyChanged;
            if (changed != null){
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
