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
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _viewModel = new CoordinatorMasterPageViewModel(_pageService.Object);
        }

        [Test()]
        public void CoordinatorMasterPageTests_GetLatestForum_WhenCalled_ShouldReturnLatestForum() {
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
        public void CoordinatorMasterPageTests_GetLatestForum_WhenCalled_ShouldReturnNull_IfNoForumsAvailable() {
            Forum passedForum = new Forum {
                Date = DateTime.Now.AddDays(-2)
            };
            List<Forum> forums = new List<Forum>();
            forums.Add(passedForum);

            var result = _viewModel.GetLatestForum(forums);

            Assert.IsNull(result);
        }

        [Test()]
        public void CoordinatorMasterPageTests_GetLatestForum_WhenCalled_ShouldReturnNull_IfNoForumsExist() {
            List<Forum> forums = new List<Forum>();

            var result = _viewModel.GetLatestForum(forums);

            Assert.IsNull(result);
        }

        [Test()]
        public void CoordinatorMasterPageTests_SetLatestForumFields_DisplayWarningIfNoForumAvailable() {
            Forum latestForum = null;
            _viewModel.SetLatestForumFields(latestForum);

            Assert.False(_viewModel.ForumVisibility);
            Assert.True(_viewModel.NoForumWarning);
        }

        [Test()]
        public void CoordinatorMasterPageTests_SetLatestForumFields_ForumVisible() {
            Forum latestForum = new Forum();
            _viewModel.SetLatestForumFields(latestForum);

            Assert.True(_viewModel.ForumVisibility);
            Assert.False(_viewModel.NoForumWarning);
        }

        [Test()]
        public void CoordinatorMasterPageTests_SetLatestForumFields_AssignCorrectFields() {
            Forum latestForum = new Forum {
                Title = "Teste",
                Place = "Teste",
                Schedules = "Teste",
                Date = DateTime.Now,
                Hour = TimeSpan.FromHours(1)
            };
            _viewModel.SetLatestForumFields(latestForum);

            Assert.AreEqual(latestForum.Title, _viewModel.ForumTitle);
            Assert.AreEqual(latestForum.Place, _viewModel.ForumPlace);
            Assert.AreEqual(latestForum.Schedules, _viewModel.ForumSchedules);
            Assert.AreEqual(latestForum.Date, _viewModel.ForumDate);
            Assert.AreEqual(latestForum.Hour, _viewModel.ForumHour);
            Assert.NotNull(_viewModel.SelectedForum);
        }
        [Test()]
        public void CoordinatorMasterPageTests_GetInstance() {
            Assert.IsInstanceOf<CoordinatorMasterPageViewModel>(CoordinatorMasterPageViewModel.GetInstance());
        }

        [Test()]
        public void CoordinatorMasterPageTests_GetLatestForm() {
            Assert.Null(_viewModel.GetLatestForm(null));
            Assert.Null(_viewModel.GetLatestForm(new List<Form>()));
            List<Form> test = new List<Form>();
            Form form = new Form();
            test.Add(form);
            Assert.AreSame(form,_viewModel.GetLatestForm(test));
        }

        [Test()]
        public void CoordinatorMasterPageTests_SetLatestFormFields() {
            Form form = null;
            _viewModel.SetLatestFormFields(form);
            Assert.False(_viewModel.FormVisibility);
            Assert.True(_viewModel.NoFormWarning);
            form = new Form();
            form.Title = "teste";
            form.MultipleChoiceQuestions = new List<MultipleChoiceQuestion>();
            form.DiscursiveQuestions = new List<DiscursiveQuestion>();
            _viewModel.SetLatestFormFields(form);
            Assert.True(_viewModel.FormVisibility);
            Assert.False(_viewModel.NoFormWarning);
            Assert.AreEqual(form.Title, _viewModel.FormTitle);
        }
        [Test()]
        public void CoordinatorMasterPageTests_ActivityIndicator_GetandSet() {
            _viewModel.ActivityIndicator = true;
            Assert.True(_viewModel.ActivityIndicator);
            _viewModel.ActivityIndicator = false;
            Assert.False(_viewModel.ActivityIndicator);
        }

        [Test()]
        public void CoordinatorMasterPageTests_IsLoaded_GetandSet() {
            _viewModel.IsLoaded = true;
            Assert.True(_viewModel.IsLoaded);
            _viewModel.IsLoaded = false;
            Assert.False(_viewModel.IsLoaded);
        }

    }
}
