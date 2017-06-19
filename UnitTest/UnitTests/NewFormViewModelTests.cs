using Acr.UserDialogs;
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
        private Mock<IUserDialogs> _dialog;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _dialog = new Mock<IUserDialogs>();
            viewModel = new NewFormViewModel(_dialog.Object, _pageService.Object);

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
        public void NewFormViewModel_QuestionsListsValidation_EmptyLists() {
            Assert.True(viewModel.AreQuestionsListsEmpty());
        }
        [Test()]
        public void NewFormViewModel_QuestionsListsValidation_EmptyDiscursive() {
            viewModel.MultipleChoiceQuestions.Add(new QuestionDetailViewModel {
                Title = "test",
                MultipleAnswers = true,
                Options = new System.Collections.ObjectModel.ObservableCollection<string> {
                    "test"
                }
            });
            Assert.False(viewModel.AreQuestionsListsEmpty());
        }
        [Test()]
        public void NewFormViewModel_QuestionsListsValidation_EmptyMultipleChoice() {
            viewModel.DiscursiveQuestionsTitles.Add("questão");
            Assert.False(viewModel.AreQuestionsListsEmpty());
        }
    }
}
