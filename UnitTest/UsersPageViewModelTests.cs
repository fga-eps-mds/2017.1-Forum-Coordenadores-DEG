using ForumDEG.Interfaces;
using ForumDEG.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    class UsersPageViewModelTests {
        [TestFixture()]
        class ForumsViewModelTests {
            private Mock<IPageService> _pageService;
            private UsersPageViewModel _viewModel;

            [SetUp]
            public void Setup() {
                _pageService = new Mock<IPageService>();
               _viewModel = UsersPageViewModel.GetInstance();
            }

            [Test()]
            public void SelectAdministrator_WhenCalled_AdministratorShouldBeSelected() {
                var administrator = new AdministratorDetailPageViewModel();
                _viewModel.SelectAdministratorCommand.Execute(administrator);

                Assert.AreSame(administrator, _viewModel.SelectedAdministrator);
            }

            [Test()]
            public void SelectCoordinator_WhenCalled_CoordinatorShouldBeSelected() {
                var coordinator = new CoordinatorDetailPageViewModel();
                _viewModel.SelectCoordinatorCommand.Execute(coordinator);

                Assert.AreSame(coordinator, _viewModel.SelectedCoordinator);
            }
        }
    }
}
