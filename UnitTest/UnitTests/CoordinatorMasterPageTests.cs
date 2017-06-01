using ForumDEG.ViewModels;
using ForumDEG.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ForumDEG.Interfaces;

namespace Tests {
    class CoordinatorMasterPageTests {
        private CoordinatorMasterPageViewModel _viewModel;
        private Mock<IPageService> _pageServiceMock;

        [SetUp]
        public void Setup() {
            _pageServiceMock = new Mock<IPageService>();
            _viewModel = new CoordinatorMasterPageViewModel(_pageServiceMock.Object);
        }

        [Test()]
        public void GetLatestForum_WhenCalled_ShouldReturnLatestForum() {
            Forum earliestForum = new Forum {
                Date = DateTime.Now.AddDays(5)
            };
            Forum latestForum = new Forum {
                Date = DateTime.Now.AddDays(2)
            };
            List<Forum> forums = new List<Forum>();
            forums.Add(earliestForum);
            forums.Add(latestForum);

            var result = _viewModel.GetLatestForum(forums);

            Assert.AreEqual(result, latestForum);
        }

        [Test()]
        public void GetLatestForum_WhenCalled_ShouldReturnNull_IfNoForumsAvailable() {
            Forum passedForum = new Forum {
                Date = DateTime.Now.AddDays(-2)
            };
            List<Forum> forums = new List<Forum>();
            forums.Add(passedForum);

            var result = _viewModel.GetLatestForum(forums);

            Assert.IsNull(result);
        }

        [Test()]
        public void GetLatestForum_WhenCalled_ShouldReturnNull_IfNoForumsExist() {
            List<Forum> forums = new List<Forum>();

            var result = _viewModel.GetLatestForum(forums);

            Assert.IsNull(result);
        }

        [Test()]
        public void SetLatestForumFields_DisplayWarningIfNoForumAvailable() {
            Forum latestForum = null;
            _viewModel.SetLatestForumFields(latestForum);

            Assert.False(_viewModel.ForumVisibility);
            Assert.True(_viewModel.NoForumWarning);
        }

        [Test()]
        public void SetLatestForumFields_ForumVisible() {
            Forum latestForum = new Forum();
            _viewModel.SetLatestForumFields(latestForum);

            Assert.True(_viewModel.ForumVisibility);
            Assert.False(_viewModel.NoForumWarning);
        }

        [Test()]
        public void SetLatestForumFields_AssignCorrectFields() {
            Forum latestForum = new Forum {
                Title = "Teste",
                Place = "Teste",
                Schedules = "Teste",
                Date = DateTime.Now,
                Hour = TimeSpan.FromHours(1)
            };
            _viewModel.SetLatestForumFields(latestForum);

            Assert.AreEqual(latestForum.Title, _viewModel.Title);
            Assert.AreEqual(latestForum.Place, _viewModel.Place);
            Assert.AreEqual(latestForum.Schedules, _viewModel.Schedules);
            Assert.AreEqual(latestForum.Date, _viewModel.Date);
            Assert.AreEqual(latestForum.Hour, _viewModel.Hour);
            Assert.NotNull(_viewModel.SelectedForum);
        }
    }
}
