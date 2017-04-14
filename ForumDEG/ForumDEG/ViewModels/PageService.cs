using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class PageService : IPageService {
        public async Task DisplayAlert(string title, string message, string ok) {
            await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }

        public Task PushAsync(Page page) {
            throw new NotImplementedException();
        }
    }
}
