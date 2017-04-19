using NUnit.Framework;
using ForumDEG.ViewModels;
using ForumDEG.Utils;
using Moq;

namespace UnitTest {
    public class UserRegistrationViewModelTests {

        private UserRegistrationViewModel viewModel;
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            viewModel = new UserRegistrationViewModel(_pageService.Object);

            viewModel.NameIn = "Name";
            viewModel.EmailIn = "Email";
            viewModel.RegistrationIn= "Registration";
            viewModel.PasswordIn = "Passoword";
        }

        [Test()]
        public void HasEmptySpace_NameInBlank() {
            viewModel.NameIn = "";
           Assert.True(viewModel.HasEmptySpace());
        }

        [Test()]
        public void HasEmptySpace_EmailInBlank() {
            viewModel.EmailIn = "";
            Assert.True(viewModel.HasEmptySpace());
        }

        [Test()]
        public void HasEmptySpace_RegistrationInBlank() {
            viewModel.RegistrationIn = "";
            Assert.True(viewModel.HasEmptySpace());
        }

        [Test()]
        public void HasEmptySpace_PasswordInBlank() {
            viewModel.PasswordIn = "";
            Assert.True(viewModel.HasEmptySpace());
        }
    }
}
