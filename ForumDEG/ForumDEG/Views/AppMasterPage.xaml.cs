using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMasterPage : MasterDetailPage {
        public AppMasterPage() {
            InitializeComponent();

        }

        async void Handle_ClickedForum(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new ForumsPage());
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

