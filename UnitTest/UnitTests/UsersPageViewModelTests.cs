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
    class UsersPageViewModelTests {
        [TestFixture()]
        class ForumsViewModelTests {
            private Mock<IPageService> _pageService;
            private UsersViewModel _viewModel;

            [SetUp]
            public void Setup() {
                _pageService = new Mock<IPageService>();
               _viewModel = UsersViewModel.GetInstance();
            }

            [Test()]
            public void SelectAdministrator_WhenCalled_AdministratorShouldBeSelected() {
                var administrator = new UserDetailViewModel(_pageService.Object);
                _viewModel.SelectAdministratorCommand.Execute(administrator);

                Assert.AreSame(administrator, _viewModel.SelectedAdministrator);
            }

            [Test()]
            public void SelectCoordinator_WhenCalled_CoordinatorShouldBeSelected() {
                var coordinator = new UserDetailViewModel(_pageService.Object);
                _viewModel.SelectCoordinatorCommand.Execute(coordinator);

                Assert.AreSame(coordinator, _viewModel.SelectedCoordinator);
            }

            [Test()]
            public void UsersViewModel_ActivityIndicatorTrueWhenLookingForUsers() {
                _viewModel.UpdateUsersList();
                Assert.True(_viewModel.ActivityIndicator);
            }

            [Test()]
            public void UsersViewModel_IsLoadedFalse_WhenLookingForUsers() {
                _viewModel.UpdateUsersList();
                Assert.False(_viewModel.IsLoaded);
            }
        }
    }
}
