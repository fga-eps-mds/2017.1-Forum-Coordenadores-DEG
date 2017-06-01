using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using ForumDEG.Views;
using Moq;
using NUnit.Framework;

namespace Tests {
    [TestFixture()]
    class ForumsViewModelTests {
        private Mock<IPageService> _pageService;
        private ForumsViewModel _viewModel;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _viewModel = ForumsViewModel.GetInstance();
        }

        [Test()]
        public void SelectForum_WhenCalled_ForumShouldBeSelected() {
            var forum = new ForumDetailViewModel(_pageService.Object);
            _viewModel.SelectForumCommand.Execute(forum);

            Assert.AreSame(forum, _viewModel.SelectedForum);
        }
    }
}
