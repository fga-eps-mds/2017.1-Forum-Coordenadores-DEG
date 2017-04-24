using NUnit.Framework;
using ForumDEG.ViewModels;
using ForumDEG.Utils;
using Moq;
using ForumDEG.Interfaces;
using Acr.UserDialogs;

namespace UnitTest {
    public class NewForumViewModelTests {
        private NewForumViewModel viewModel;
        private Mock<IUserDialogs> _mockDialogs;
        private Mock<IPageService> _mockPageService;

        [SetUp]
        public void Setup() {
            _mockDialogs = new Mock<IUserDialogs>();
            _mockPageService = new Mock<IPageService>();
            viewModel = new NewForumViewModel(_mockDialogs.Object, _mockPageService.Object);

            viewModel.Forum._title = "Title";
            viewModel.Forum._place = "Place";
            viewModel.Forum._schedules = "Schedules";
        }
        [Test()]
        public void IsAnyFieldBlank_TitleFieldIsBlank() {
            viewModel.Forum._title = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_PlaceFieldIsBlank() {
            viewModel.Forum._place = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_SchedulesFieldIsBlank() {
            viewModel.Forum._schedules = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_NoFieldIsBlank() {
            Assert.False(viewModel.IsAnyFieldBlank());
        }
    }
}