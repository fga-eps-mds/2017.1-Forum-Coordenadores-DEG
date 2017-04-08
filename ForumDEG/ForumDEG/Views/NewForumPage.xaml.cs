using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewForumPage : ContentPage {
        public NewForumPage() {
            InitializeComponent();
        }

        private void OnNewForumButtonClicked(object sender, EventArgs e) {
            DisplayAlert("Fórum Criado", "O novo fórum foi criado com sucesso. Os coordenadores serão notificados em breve.", "OK");
        }
    }
}
