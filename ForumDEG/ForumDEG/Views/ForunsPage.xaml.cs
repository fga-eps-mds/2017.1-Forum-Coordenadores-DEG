using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForunsPage : MasterDetailPage {
        private ForunsViewModel _fvm;
        
        public ForunsPage() {
            InitializeComponent();

            _fvm = new ForunsViewModel();
            ForumList.ItemsSource = _fvm.GetUpdatedList();
        }

        private async void ShowDetail(object sender, EventArgs e) {
            //Implementation Here
        }

        void Handle_Refreshing(object sender, System.EventArgs e) {
            ForumList.ItemsSource = _fvm.GetUpdatedList();
            ForumList.EndRefresh();
        }

        void Handle_Selected(object sender, System.EventArgs e) {
            if (e == null) return;
            ((ListView)sender).SelectedItem = null;
        }

        void Handle_ClickedHome(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedForum(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedCriarForum(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedCoordenador(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedCadastrarCoordenador(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedFormulario(object sender, System.EventArgs e) {
            //            Implementation here
        }

        void Handle_ClickedCriarFormulario(object sender, System.EventArgs e) {
            //            Implementation here
        }
    }
}
