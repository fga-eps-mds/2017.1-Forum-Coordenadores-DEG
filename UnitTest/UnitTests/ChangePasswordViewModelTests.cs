using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;

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
            _viewmodel.CurrentPassword = "123456aA";
            _viewmodel.NewPassword = "123456bB";
            _viewmodel.RepeatedPassword = "123456bB";
        }

        [Test]
        public void ChangePasswordViewModelTests_ValidateFields() {
            _viewmodel.NewPassword = " ";
            _viewmodel.RepeatedPassword = " ";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.IsNull(_viewmodel.ChangedCoordinator);
        }

        [Test]
        public void ChangePasswordViewModelTests_MatchPasswords() {
            _viewmodel.RepeatedPassword = "123456bN";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.IsNull(_viewmodel.ChangedCoordinator);
        }

        [Test]
        public void ChangePasswordViewModelTests_VerifyActualPassword() {
            _viewmodel.CurrentPassword = "123456aB";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.IsNull(_viewmodel.ChangedCoordinator);
        }

        [Test]
        public void ChangePasswordViewModelTests_ValidatePassword() {
            _viewmodel.NewPassword = "123456bb";
            _viewmodel.RepeatedPassword = "123456bb";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.IsNull(_viewmodel.ChangedCoordinator);

            _viewmodel.NewPassword = "1234Bb";
            _viewmodel.RepeatedPassword = "1234Bb";
            _viewmodel.ChangePasswordClickedCommand.Execute(null);
            Assert.IsNull(_viewmodel.ChangedCoordinator);
        }
    }
}
