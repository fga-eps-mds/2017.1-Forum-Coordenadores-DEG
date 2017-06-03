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
                Title = "Formulário",
                DiscursiveQuestions = new List<Models.DiscursiveQuestion> {
                    new Models.DiscursiveQuestion {
                        Question = "Pergunta discursiva"
                    },
                    new Models.DiscursiveQuestion {
                        Question = "Outra pergunta"
                    }
                },
                MultipleChoiceQuestions = new List<MultipleChoiceQuestion>()
            });

            List<MultipleAnswersQuestion> multipleQuestions = new List<MultipleAnswersQuestion> {
                new MultipleAnswersQuestion("Questão 01") {
                    new Option {
                        OptionText = "Opção 01"
                    },
                    new Option {
                        OptionText = "Opção 02"
                    }
                },
                new MultipleAnswersQuestion("Questão 02") {
                    new Option {
                        OptionText = "Outra Opção"
                    },
                    new Option {
                        OptionText = "E outra"
                    }
                }
            };

            List<SingleAnswerQuestion> singleQuestions = new List<SingleAnswerQuestion> {
                new SingleAnswerQuestion {
                    Question = "Questão Radio",
                    Options = new ObservableCollection<string> { "Uma", "Duas", "Três" }
                },
                new SingleAnswerQuestion {
                    Question = "Radio questão",
                    Options = new ObservableCollection<string> {"Três", "Dois", "Um"}
                }
            };

            BindingContext = _viewModel;
            InitializeComponent();
            GroupedView.ItemsSource = multipleQuestions;
            SingleQuestionList.ItemsSource = singleQuestions;
        }
    }
}
