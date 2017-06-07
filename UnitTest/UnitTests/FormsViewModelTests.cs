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
    class FormsViewModelTests {
        FormsViewModel _viewModel;
        Mock<IPageService> _pageservice;

        [SetUp]
        public void SetUp() {
            _pageservice = new Mock<IPageService>();
            _viewModel = new FormsViewModel(_pageservice.Object);
        }

        [Test]
        public void FormsViewModel_GetIntance() {
            Assert.NotNull(FormsViewModel.GetInstance());
        }
        
        [Test]
        public void FormsViewModel_SelectedForms_Get_Set() {
            FormDetailViewModel test = new FormDetailViewModel(_pageservice.Object);
            _viewModel.SelectedForm = test;
            Assert.AreSame(test, _viewModel.SelectedForm);
        }
    }
}
