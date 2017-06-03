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
            try {
                _viewModel = new ViewModels.FormDetailViewModel(new Models.Form {
                    Title = "Formulário",
                    DiscursiveQuestions = new List<Models.DiscursiveQuestion> {
                    new Models.DiscursiveQuestion {
                        Question = "Pergunta discursiva"
                    },
                    new Models.DiscursiveQuestion {
                        Question = "Outra pergunta"
                    }
                },
                    MultipleChoiceQuestions = new List<MultipleChoiceQuestion> {
                    new MultipleChoiceQuestion("Questão 01", true) {
                        new Option {
                        OptionText = "Opção 01"
                        },
                        new Option {
                        OptionText = "Opção 02"
                        }
                    },
                    new MultipleChoiceQuestion("Questão 02", true) {
                        new Option {
                        OptionText = "Opção 01"
                        },
                        new Option {
                        OptionText = "Opção 02"
                        }
                    },
                    new MultipleChoiceQuestion("Questão 03", false) {
                        new Option {
                        OptionText = "Opção 01"
                        },
                        new Option {
                        OptionText = "Opção 02"
                        }
                    },
                    new MultipleChoiceQuestion("Questão 04", false) {
                        new Option {
                        OptionText = "Opção 01"
                        },
                        new Option {
                        OptionText = "Opção 02"
                        }
                    }
                }
                });
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
