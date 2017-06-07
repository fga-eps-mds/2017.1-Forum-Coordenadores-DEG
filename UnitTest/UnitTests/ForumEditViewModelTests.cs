using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.UnitTests {
    class ForumEditViewModelTests {
        ForumEditViewModel _viewmodel;
        Mock<IPageService> _pageservice;

        [SetUp]
        public void SetUp() {
            _pageservice = new Mock<IPageService>();
            _viewmodel = new ForumEditViewModel(_pageservice.Object);
            _viewmodel.ForumTitle = "Titulo";
            _viewmodel.ForumPlace = "Local";
            _viewmodel.ForumSchedules = "teste";
            _viewmodel.ForumDate = DateTime.Now;
            _viewmodel.ForumHour = DateTime.Now.TimeOfDay;

        }

        [Test]
        public void ForumEditViewModel_IsAnyFieldBlank() {
            Assert.False(_viewmodel.IsAnyFieldBlank());

            _viewmodel.ForumTitle = "";
            Assert.True(_viewmodel.IsAnyFieldBlank());

            _viewmodel.ForumTitle = "Titulo";
            _viewmodel.ForumPlace = "";
            Assert.True(_viewmodel.IsAnyFieldBlank());

            _viewmodel.ForumPlace = "Local";
            _viewmodel.ForumSchedules = "";
            Assert.True(_viewmodel.IsAnyFieldBlank());

        }

        [Test]
        public void ForumEditViewModel_ForumDateGet() {
            Assert.AreEqual(_viewmodel.ForumDate, _viewmodel.Forum.Date);
        }

        [Test]
        public void ForumEditViewModel_ForumHourGet() {
            Assert.AreEqual(_viewmodel.ForumHour, _viewmodel.Forum.Hour);
        }


    }
}
