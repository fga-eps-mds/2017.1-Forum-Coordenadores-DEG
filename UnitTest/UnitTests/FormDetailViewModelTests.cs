using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.ViewModels;
using ForumDEG.Interfaces;
using Moq;

namespace UnitTest.UnitTests {
    public class FormDetailViewModelTests {
        private FormDetailViewModel _viewModel;
        private Mock<IPageService> _pageService;
        private List<ForumDEG.Models.MultipleChoiceQuestion> _multipleChoiceQuestions;
        private static string _multipleAnswer = "Questão check";
        private static string _singleAnswer = "Questão radio";
        private static int testQuestionsAmount;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _viewModel = new FormDetailViewModel(_pageService.Object);
            _multipleChoiceQuestions = new List<ForumDEG.Models.MultipleChoiceQuestion> {
                new ForumDEG.Models.MultipleChoiceQuestion(_multipleAnswer, true) {
                    new ForumDEG.Models.Option { OptionText = "Opção 01" },
                    new ForumDEG.Models.Option { OptionText = "Opção 02" }
                },
                new ForumDEG.Models.MultipleChoiceQuestion(_singleAnswer, false) {
                    new ForumDEG.Models.Option { OptionText = "Opção 01" },
                    new ForumDEG.Models.Option { OptionText = "Opção 02" }
                }
            };
            _viewModel.MultipleChoiceQuestions = _multipleChoiceQuestions;
            _viewModel.DiscursiveQuestions = new List<ForumDEG.Models.DiscursiveQuestion>();
            testQuestionsAmount = 2;
        }

        [Test]
        public void FormDetailViewModelTests_SplitMultipleChoiceQuestions_MultipleAnswersList() {
            _viewModel.SplitMultipleChoiceQuestions();
            Assert.AreEqual(1, _viewModel.MultipleAnswersQuestions.Count);
            Assert.AreEqual(_multipleAnswer, _viewModel.MultipleAnswersQuestions[0].Question);
        }
        [Test]
        public void FormDetailViewModelTests_SplitMultipleChoiceQuestions_SingleAnswerList() {
            _viewModel.SplitMultipleChoiceQuestions();
            Assert.AreEqual(1, _viewModel.SingleAnswerQuestions.Count);
            Assert.AreEqual(_singleAnswer, _viewModel.SingleAnswerQuestions[0].Question);
        }
        [Test]
        public void FormDetailViewModelTests_QuestionsAmount_Get() {
            Assert.AreEqual(testQuestionsAmount,_viewModel.QuestionsAmount);
        }


    }
}
