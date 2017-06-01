using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    class LoginViewModelTests {
        private LoginViewModel _viewModel;
        private Mock<IPageService> _pageService;
        private Mock<IUserDialogs> _dialog;


        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _dialog = new Mock<IUserDialogs>();
            _viewModel = new LoginViewModel(_pageService.Object,_dialog.Object);
            _viewModel._userRegistration = "12345678";
            _viewModel._userPassword = "Coordinator1";

        }

        [Test()]
        public void ValidateLogin_WithNullRegistration() {
            _viewModel._userRegistration = null;
            Assert.False(_viewModel.ValidateLogin().Result);
        }

        [Test()]
        public void ValidateLogin_WithNullPassWord() {
            _viewModel._userPassword = null;
            Assert.False(_viewModel.ValidateLogin().Result);
        }

        [Test()]
        public void ValidateLogin_WithInvalidRegistration() {
            _viewModel._userRegistration = "123";
            Assert.False(_viewModel.ValidateLogin().Result);
        }

        [Test()]
        public void ValidateLogin_WithInvalidPassword() {
            _viewModel._userPassword = "123";
            Assert.False(_viewModel.ValidateLogin().Result);
        }
    }
}
