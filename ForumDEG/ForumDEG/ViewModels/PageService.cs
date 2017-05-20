using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.Interfaces;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace ForumDEG.ViewModels {
    public class PageService : IPageService {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel) {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async void DisplayAlert(string title, string message, string ok) {
            await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }
        public async Task PopAsync() {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page) {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
