using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.ViewModels;
using ForumDEG.Interfaces;

namespace Tests {
    class UserEditTests {

        private UserEditViewModel viewModel;
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            viewModel = new UserEditViewModel(_pageService.Object,true);

            viewModel.NameIn = "Name";
            viewModel.EmailIn = "email@email.com";
            viewModel.RegistrationIn = "123456789";
            viewModel.PasswordIn = "Pb123456785";
            viewModel.CourseIn = "course";
            viewModel.UserTypeIn = 1;
        }

        [Test()]
        public void IsAnyFieldBlank_NoBlankFields() {
            Assert.False(viewModel.IsAnyFieldBlank());
        }

        [Test()]
        public void IsAnyFieldBlank_NameInBlank() {
            viewModel.NameIn = "";
            Assert.True(viewModel.IsAnyFieldBlank());
        }

        [Test()]
        public void IsAnyFieldBlank_EmailInBlank() {
            viewModel.EmailIn = "";
            Assert.True(viewModel.IsAnyFieldBlank());
        }

        [Test()]
        public void IsAnyFieldBlank_RegistrationInBlank() {
            viewModel.RegistrationIn = "";
            Assert.True(viewModel.IsAnyFieldBlank());
        }

        [Test()]
        public void IsAnyFieldBlank_PasswordInBlank() {
            viewModel.PasswordIn = "";
            Assert.True(viewModel.IsAnyFieldBlank());
        }

        [Test()]
        public void ValidateEmail_ValidEmail() {
            Assert.True(viewModel.ValidateEmail());
        }

        [Test()]
        public void ValidateEmail_InvalidEmail() {
            viewModel.EmailIn = "email";
            Assert.False(viewModel.ValidateEmail());

            viewModel.EmailIn = "email@email";
            Assert.False(viewModel.ValidateEmail());

            viewModel.EmailIn = "email@";
            Assert.False(viewModel.ValidateEmail());
        }


        [Test()]
        public void ValidatePassword_ValidPassWord() {
            Assert.True(viewModel.ValidatePassword());
        }

        [Test()]
        public void ValidatePassword_invalidPassWord() {
            //less chars than 8
            viewModel.PasswordIn = "Pb1";
            Assert.False(viewModel.ValidatePassword());

            //too many chars
            viewModel.PasswordIn = "Pb123456789012345";
            Assert.False(viewModel.ValidatePassword());

            //no uppercase chars
            viewModel.PasswordIn = "pb1234567";
            Assert.False(viewModel.ValidatePassword());

            //no lowercase chars
            viewModel.PasswordIn = "PB1234567";
            Assert.False(viewModel.ValidatePassword());

            //no numbers
            viewModel.PasswordIn = "Abcdefghij";
            Assert.False(viewModel.ValidatePassword());
        }
    }
}
