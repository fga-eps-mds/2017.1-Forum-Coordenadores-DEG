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
    public partial class ForumDetailPage : ContentPage {
        ForunsViewModel _fvm = ForunsViewModel.GetInstance();

        public ForumDetailPage() {
            InitializeComponent();
            if (_fvm.GetSelected() == null)
                throw new Exception("Forum detail page with no selection");
            BindingContext = ForunsViewModel.GetInstance().GetSelected();
            
            foreach (var theme in _fvm.GetSelected()._schedules) {
                var themeLabel = new Label {
                    Text = "- " + theme,
                    TextColor = Color.Gray
                };
                themesStack.Children.Add(themeLabel);
            }

            if (HasPassed(DateTime.Now, _fvm.GetSelected()._date)) {
                presenceButton.IsEnabled = false;
                unpresenceButton.IsEnabled = false;
            }
        }

        private void themesList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            var s = sender as ListView;
            s.SelectedItem = null;
        }

        public bool HasPassed(DateTime fromDate, DateTime expireDate) {
            return expireDate - fromDate < TimeSpan.FromMinutes(0);
        }
    }
}
