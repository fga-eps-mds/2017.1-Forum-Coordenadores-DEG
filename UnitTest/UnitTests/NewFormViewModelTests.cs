using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    class NewFormViewModelTests {
        private Mock<IPageService> _pageService;
        private NewFormViewModel viewModel;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            viewModel = new NewFormViewModel(_pageService.Object);

        }

        [Test()]
        public void NewFormViewModelTests_IsFieldBlank_NoBlankFields() {
            Assert.False(viewModel.IsFieldBlank("teste"));
        }

        [Test()]
        public void NewFormViewModelTests_IsFieldBlank_BlankFields() {
            Assert.True(viewModel.IsFieldBlank(" "));
        }
        [Test()]
        public void NewFormViewModelTests_SelectedQuestion_Get_Set() {
            QuestionDetailViewModel test = new QuestionDetailViewModel();
            viewModel.SelectedQuestion = test;
            Assert.AreSame(test, viewModel.SelectedQuestion);
        }
        [Test()]
        public void NewFormViewModelTests_TapCount_Get_Set() {
            float test = 5;
            viewModel.TapCount = test;
            Assert.AreEqual(test, viewModel.TapCount);
        }
        [Test()]
        public void NewFormViewModelTests_ExtraButtonsVisibility_Get_Set() {
            bool test = true;
            viewModel.ExtraButtonsVisibility = test;
            Assert.True(viewModel.ExtraButtonsVisibility);
        }

    }
}
