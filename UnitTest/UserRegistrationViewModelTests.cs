using NUnit.Framework;
using ForumDEG.ViewModels;
using ForumDEG.Utils;
using Moq;

namespace UnitTest {
    public class UserRegistrationViewModelTests {

        private UserRegistrationViewModel viewModel;
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            viewModel = new UserRegistrationViewModel(_pageService.Object);
        }
        
       

    }
}
