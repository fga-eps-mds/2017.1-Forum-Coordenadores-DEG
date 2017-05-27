using ForumDEG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserEditPage : ContentPage {
        UserEditViewModel _viewModel;
        public UserEditPage(string Registration, bool IsCoordinator) {
            InitializeComponent();
            
            _viewModel = new UserEditViewModel(new PageService(), IsCoordinator);
            BindingContext = _viewModel;
            if (IsCoordinator) {
                _viewModel.setOldCoordinatorFields(Registration);
            } else {
                _viewModel.setOldAdministratorFields(Registration);
            }

        }
    }
}
