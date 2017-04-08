using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForumDEG.ViewModels;
using ForumDEG.Models;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForunsPage : MasterDetailPage {
        //Simulatio
        
        List<Forum>  GetForum() {
            return new List<Forum> { 
                //example
                new Forum {_title = "Forum 01", _place ="Auditorio1"},
                new Forum {_title = "Forum 02", _place ="Auditorio2"},
                new Forum {_title = "Forum 03", _place ="Auditorio3"},
                new Forum {_title = "Forum 04", _place ="Auditorio4"},
                new Forum {_title = "Forum 05", _place ="Auditorio5"},
                new Forum {_title = "Forum 06", _place ="Auditorio6"},
                new Forum {_title = "Forum 07", _place ="Auditorio7"}
            };
        }
        // ------ until Here
        void Handle_Refreshing(object sender, System.EventArgs e) {
            ForumList.ItemsSource = GetForum();
            ForumList.EndRefresh();
        }

        public ForunsPage() {
            InitializeComponent();
            
            ForumList.ItemsSource = GetForum();
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

        void Handle_ClieckedPauta(object sender, System.EventArgs e) {

        }
    }
}
