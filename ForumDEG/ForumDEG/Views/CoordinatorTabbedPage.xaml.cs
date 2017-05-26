using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoordinatorTabbedPage : TabbedPage {
        public CoordinatorTabbedPage() {
            InitializeComponent();
        }

        private async Task LogoutButtonClicked(object sender, EventArgs e) {
            ForumDEG.Helpers.Settings.IsUserLogged = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}