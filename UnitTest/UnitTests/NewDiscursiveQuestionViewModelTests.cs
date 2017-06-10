using Acr.UserDialogs;
using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;

namespace Tests {
    class NewDiscursiveQuestionViewModelTests {
        private Mock<IPageService> _pageService;
        private Mock<IUserDialogs> _dialog;
        private NewDiscursiveQuestionViewModel viewModel;
        private NewFormViewModel _form;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _dialog = new Mock<IUserDialogs>();
            _form = new NewFormViewModel(_dialog.Object,_pageService.Object);
            viewModel = new NewDiscursiveQuestionViewModel(_pageService.Object, _dialog.Object, _form);
        }

        [Test]
        public void Validate_ValidateFalse() {
            Assert.False(viewModel.Validate());
        }

        [Test]
        public void Validate_ValidateTrue() {
            viewModel._questionText = "Any Text";
            Assert.True(viewModel.Validate());
        }
    }
}
