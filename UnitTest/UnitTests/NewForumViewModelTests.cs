using NUnit.Framework;
using ForumDEG.ViewModels;
using ForumDEG.Models;
using Moq;
using ForumDEG.Interfaces;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace Tests {
    public class NewForumViewModelTests {
        private NewForumViewModel viewModel;
        private Mock<IUserDialogs> _mockDialogs;
        private Mock<IPageService> _mockPageService;
        private Mock<IDatabase<Forum>> _mockDB;

        [SetUp]
        public void Setup() {
            _mockDialogs = new Mock<IUserDialogs>();
            _mockPageService = new Mock<IPageService>();
            _mockDB = new Mock<IDatabase<Forum>>();
            viewModel = new NewForumViewModel(_mockDialogs.Object, _mockPageService.Object, _mockDB.Object);

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

        [Test()]
        public async void CreateForum_WhenClickedForumIsSaved() {
            _mockDB.Setup(m => m.Save(viewModel.Forum)).Returns(Task.FromResult(1));

            Assert.True(await viewModel.CreateForum());
        }
        [Test()]
        public async void CreateForum_SavingFailsWhenReturnIsNotOne() {
            _mockDB.Setup(m => m.Save(viewModel.Forum)).Returns(Task.FromResult(0));

            Assert.False(await viewModel.CreateForum());
        }
    }
}