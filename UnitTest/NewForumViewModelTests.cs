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

            viewModel.Forum.Title = "Title";
            viewModel.Forum.Place = "Place";
            viewModel.Forum.Schedules = "Schedules";
        }
        [Test()]
        public void IsAnyFieldBlank_TitleFieldIsBlank() {
            viewModel.Forum.Title = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_PlaceFieldIsBlank() {
            viewModel.Forum.Place = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_SchedulesFieldIsBlank() {
            viewModel.Forum.Schedules = "";

            Assert.True(viewModel.IsAnyFieldBlank());
        }
        [Test()]
        public void IsAnyFieldBlank_NoFieldIsBlank() {
            Assert.False(viewModel.IsAnyFieldBlank());
        }
    }
}