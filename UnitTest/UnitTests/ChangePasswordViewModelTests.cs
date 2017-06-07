using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.Models;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.UnitTests {
    class ChangePasswordViewModelTests {
        Mock<IPageService> _pageService;
        Mock<IUserDialogs> _dialog;
        ChangePasswordViewModel _viewmodel;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _dialog = new Mock<IUserDialogs>();
            _viewmodel = new ChangePasswordViewModel(_pageService.Object, _dialog.Object);
            _viewmodel._actualPassword = "123456aA";
            _viewmodel._newPassword = "123456bB";
            _viewmodel._repeatedPassword = "123456bB";
        }

        [Test]
        public void ChangePasswordViewModelTests_GetLoggedUser() {
            Coordinator LoggedUser = new Coordinator();
            LoggedUser.Id = 123;
            LoggedUser.Course = "Engenharia";
            LoggedUser.Email = "email@email.com";
            LoggedUser.Name = "Marigué";
            LoggedUser.Password = "123456aA";
            LoggedUser.Registration = "150151624";

            Assert.AreEqual(LoggedUser.Registration, _viewmodel.User.Registration);
        }
        [Test]
        public void ChangePasswordViewModelTests_ValidateFields() {
            _viewmodel._newPassword = " ";
            _viewmodel._repeatedPassword = " ";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreNotEqual(_viewmodel.User.Password, _viewmodel._newPassword);
        }

        [Test]
        public void ChangePasswordViewModelTests_MatchPasswords() {
            _viewmodel._repeatedPassword = "123456bN";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreNotEqual(_viewmodel.User.Password, _viewmodel._newPassword);
        }

        [Test]
        public void ChangePasswordViewModelTests_VerifyActualPassword() {
            _viewmodel._actualPassword = "123456aB";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreNotEqual(_viewmodel.User.Password, _viewmodel._newPassword);
        }

        [Test]
        public void ChangePasswordViewModelTests_ValidatePassword() {
            _viewmodel._newPassword = "123456bb";
            _viewmodel._repeatedPassword = "123456bb";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreNotEqual(_viewmodel.User.Password, _viewmodel._newPassword);

            _viewmodel._newPassword = "1234Bb";
            _viewmodel._repeatedPassword = "1234Bb";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreNotEqual(_viewmodel.User.Password, _viewmodel._newPassword);
        }

        [Test]
        public void ChangePasswordViewModelTests_UpdatePassword() {
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.AreEqual(_viewmodel.User.Password, _viewmodel._newPassword);
        }
    }
}
