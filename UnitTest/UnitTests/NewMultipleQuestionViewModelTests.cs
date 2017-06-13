using ForumDEG.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.ViewModels;
using Acr.UserDialogs;

namespace Tests {
    class NewMultipleQuestionViewModelTests {
        private Mock<IPageService> _pageService;
        private Mock<IUserDialogs> _dialog;
        private NewMultipleQuestionViewModel viewModel;
        private NewFormViewModel _form;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _dialog = new Mock<IUserDialogs>();
            _form = new NewFormViewModel(_dialog.Object, _pageService.Object);
            viewModel = new NewMultipleQuestionViewModel(true,_pageService.Object,_dialog.Object,_form);

        }

        [Test()]
        public void IsFieldBlank_NoBlankFields() {
            Assert.False(viewModel.IsFieldBlank("teste"));
        }
        [Test()]
        public void IsFieldBlank_BlankFields() {
            Assert.True(viewModel.IsFieldBlank(" "));
        }

        [Test()]
        public void IsOptionsListEmpty_EmptyList() {
            Assert.True(viewModel.IsOptionsListEmpty());
        }

        [Test()]
        public void IsOptionsListEmpty_NotEmptyList() {

            viewModel.Options.Add("teste");
            Assert.False(viewModel.IsOptionsListEmpty());
        }

        [Test()]
        public void AddOption_EntryOptionIsBlank() {
            viewModel.OptionEntry = " ";
            viewModel.AddOption();
            Assert.AreEqual(0,viewModel.Options.Count);
        }

        [Test()]
        public void AddOption_EntryOptionValid() {
            viewModel.OptionEntry = "teste";
            viewModel.AddOption();
            Assert.AreEqual(1, viewModel.Options.Count);
        }

        [Test()]
        public void RemoveOption() {
            viewModel.OptionEntry = "teste";
            viewModel.AddOption();
            viewModel.SelectedOption = "teste";
            viewModel.RemoveOption();
            Assert.AreEqual(0, viewModel.Options.Count);
        }

        [Test()]
        public async void SaveQuestion_BlankTitle() {
            viewModel.Title = " ";
            viewModel.OptionEntry = "teste";
            viewModel.AddOption();
            await viewModel.SaveQuestion();
            Assert.AreEqual(0, _form.MultipleChoiceQuestions.Count);
        }

        [Test()]
        public async void SaveQuestion_EmptyList() {
            viewModel.Title = "teste";
            
            await viewModel.SaveQuestion();
            Assert.AreEqual(0, _form.MultipleChoiceQuestions.Count);
        }

        [Test()]
        public void SaveQuestion_ValidQuestion() {
            viewModel.Title = "teste";
            viewModel.OptionEntry = "teste";
            viewModel.AddOption();
            viewModel.SaveQuestion();
            Assert.AreEqual(1, _form.MultipleChoiceQuestions.Count);
        }
    }
}
