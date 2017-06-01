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
        public void IsFieldBlank_NoBlankFields() {
            Assert.False(viewModel.IsFieldBlank("teste"));
        }

        [Test()]
        public void IsFieldBlank_BlankFields() {
            Assert.True(viewModel.IsFieldBlank(" "));
        }

    }
}
