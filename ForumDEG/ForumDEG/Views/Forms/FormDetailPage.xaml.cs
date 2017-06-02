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

        public FormDetailPage() {
            _viewModel = new ViewModels.FormDetailViewModel(new Models.Form {
                Title = "Teste",
                DiscursiveQuestions = new List<Models.DiscursiveQuestion> {
                    new Models.DiscursiveQuestion {
                        Question = "Pergunta discursiva"
                    },
                    new Models.DiscursiveQuestion {
                        Question = "Outra pergunta"
                    }
                },
                MultipleChoiceQuestions = new List<Models.MultipleChoiceQuestion> {
                    new Models.MultipleChoiceQuestion("Questão 1", true) {
                        new Option {
                            OptionText = "Opção 1"
                        },
                        new Option {
                            OptionText = "Opção 2"
                        }
                    },
                    new Models.MultipleChoiceQuestion("Questão 2", false) {
                        new Option {
                            OptionText = "Opção 1"
                        },
                        new Option {
                            OptionText = "Opção 2"
                        }
                    }
                }
            });

            BindingContext = _viewModel;
            InitializeComponent();
            GroupedView.ItemsSource = _viewModel.MultipleAnswersQuestions;
            SingleQuestionList.ItemsSource = _viewModel.SingleAnswerQuestions;
        }
    }
}
