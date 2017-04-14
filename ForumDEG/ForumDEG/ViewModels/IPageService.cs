using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public interface IPageService {
        Task PushAsync(Page page);
        Task DisplayAlert(string title, string message, string ok);
    }
}
