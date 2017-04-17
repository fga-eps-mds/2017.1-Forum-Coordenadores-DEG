using ForumDEG.Models;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels
{
    class UserRegistrationViewModel
    {
        private readonly IPageService _pageService;

        public UserRegistrationViewModel(IPageService PageService)
        {
            _pageService = PageService;
        }

        int UserTypeIn;
        string NameIn;
        string RegistrationIn;
        string EmailIn;
        string PasswordIn;
        string CourseIn;

        public bool HasEmptySpace()
        {
            if (UserTypeIn == 0)
            {
                return (String.IsNullOrWhiteSpace(NameIn) ||
                        String.IsNullOrWhiteSpace(RegistrationIn) ||
                        String.IsNullOrWhiteSpace(EmailIn) ||
                        String.IsNullOrWhiteSpace(PasswordIn) ||
                        String.IsNullOrWhiteSpace(CourseIn));
            }
            else
            {
                return (String.IsNullOrWhiteSpace(NameIn) ||
                        String.IsNullOrWhiteSpace(RegistrationIn) ||
                        String.IsNullOrWhiteSpace(EmailIn) ||
                        String.IsNullOrWhiteSpace(PasswordIn));
            }

        }

        public bool IsNewUserValid()
        {
            //verifica se os novos dados são válidos
            return true;
        }

        public async void RegisterNewUser()
        {
            if (!HasEmptySpace())
            {
                if (IsNewUserValid())
                {
                    if (UserTypeIn == 0)
                    {
                        RegisterNewCoordinator();
                    }
                    else
                    {
                        RegisterNewAdministrator();
                    }
                }
                else
                {
                    await _pageService.DisplayAlert("Titulo", "campo invalido", "ok");
                }
            }
            else
            {
                await _pageService.DisplayAlert("Titulo", "campo em branco ", "ok");
            }
        }

        public async void RegisterNewAdministrator()
        {
            Administrator Admin = new Administrator()
            {
                Name = NameIn,
                Email = EmailIn,
                Registration = RegistrationIn,
                Password = PasswordIn,
                CreatedOn = DateTime.Now
            };
            await App.AdministratorDatabase.SaveAdministrator(Admin);
            await _pageService.DisplayAlert("Titulo", "Salvou adiministrador ", "ok");
        }

        public async void RegisterNewCoordinator()
        {
            Coordinator Coord = new Coordinator()
            {
                Name = NameIn,
                Email = EmailIn,
                Registration = RegistrationIn,
                Password = PasswordIn,
                CreatedOn = DateTime.Now,
                Course = CourseIn
            };
            await App.CoordinatorDatabase.SaveCoordinator(Coord);
            await _pageService.DisplayAlert("Titulo", "Salvou coordenador ", "ok");
        }
    }
}
