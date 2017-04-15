using NUnit.Framework;
using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    public class NewForumViewModelTests {
        private NewForumViewModel viewModel;

        [SetUp]
        public void Setup() {
            viewModel = new NewForumViewModel();
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
