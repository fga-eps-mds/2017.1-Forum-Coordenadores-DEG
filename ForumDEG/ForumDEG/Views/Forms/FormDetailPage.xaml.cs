using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForumDEG.Views.Forms {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormDetailPage : ContentPage {
        private ViewModels.FormDetailViewModel _viewModel;

        public FormDetailPage(ViewModels.FormDetailViewModel viewModel) {
            try {
                _viewModel = viewModel;
            } catch (Exception ex) {
                Debug.WriteLine("[Form Page]" + ex.StackTrace + ex.Message);
            }
            BindingContext = _viewModel;
            InitializeComponent();
            GroupedView.ItemsSource = _viewModel.MultipleAnswersQuestions;
            SingleQuestionList.ItemsSource = _viewModel.SingleAnswerQuestions;
        }
    }
}
