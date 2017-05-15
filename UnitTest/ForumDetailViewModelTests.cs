using ForumDEG.ViewModels;
using NUnit.Framework;
using Xamarin.Forms;
using Moq;
using ForumDEG.Interfaces;

namespace UnitTest {
    class ForumDetailViewModelTests {
        private ForumDetailViewModel _viewModel;
        private Mock<IPageService> _pageService;


        [SetUp]
        public void SetUp() {
            _pageService = new Mock<IPageService>();
            _viewModel = new ForumDetailViewModel(_pageService.Object);
        }

        [Test()]
        public void ChangeButtonUI_WhenCalled_CancelOptionShouldAppearIfPresenceIsConfirmed() {
            _viewModel.IsConfirmed = true;
            var cancelText = "Cancelar presença";

            _viewModel.HandleButtonUI();

            Assert.AreEqual(_viewModel.ButtonText, cancelText);
        }
        [Test()]
        public void ChangeButtonUI_WhenCalled_ConfirmOptionShouldAppearIfPresenceIsNotConfirmed() {
            _viewModel.IsConfirmed = false;
            var confirmText = "Confirmar presença";

            _viewModel.HandleButtonUI();

            Assert.AreEqual(_viewModel.ButtonText, confirmText);
        }
        [Test()]
        public void ChangeButtonUI_WhenCalled_ButtonColorShouldBeRedIfPresenceIsConfirmed() {
            _viewModel.IsConfirmed = true;
            Color cancelColor = Color.Red;

            _viewModel.HandleButtonUI();

            Assert.AreEqual(_viewModel.ButtonColor, cancelColor);
        }
        [Test()]
        public void ChangeButtonUI_WhenCalled_ButtonColorShouldBeOrangeIfPresenceIsNotConfirmed() {
            _viewModel.IsConfirmed = false;
            Color confirmColor = Color.Orange;

            _viewModel.HandleButtonUI();

            Assert.AreEqual(_viewModel.ButtonColor, confirmColor);
        }
        [Test()]
        public void TogglePresence_WhenCalled_ConfirmationStatusShouldSwitch() {
            _viewModel.IsConfirmed = true;
            _viewModel.TogglePresence();
            Assert.False(_viewModel.IsConfirmed);
            _viewModel.TogglePresence();
            Assert.True(_viewModel.IsConfirmed);
        }
    }
}
