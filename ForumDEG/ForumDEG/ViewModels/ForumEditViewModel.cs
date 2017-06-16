using ForumDEG.Interfaces;
using ForumDEG.Models;
using ForumDEG.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    public class ForumEditViewModel : BaseViewModel {
        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;
        
        public Forum Forum { get; private set; } = new Forum();
        public ICommand CancelCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }

        public string ForumTitle {
            get {
                return Forum.Title;
            }
            set {
                if (Forum.Title != value) {
                    Forum.Title = value;

                    OnPropertyChanged("ForumTitle");
                }
            }
        }

        public DateTime ForumDate {
            get {
                return Forum.Date;
            }
            set {
                if (Forum.Date != value) {
                    Forum.Date = value;

                    OnPropertyChanged("ForumDate");
                }
            }
        }

        public TimeSpan ForumHour {
            get {
                return Forum.Hour;
            }
            set {
                if (Forum.Hour != value) {
                    Forum.Hour = value;

                    OnPropertyChanged("ForumHour");
                }
            }
        }

        public string ForumPlace {
            get {
                return Forum.Place;
            }
            set {
                if (Forum.Place != value) {
                    Forum.Place = value;

                    OnPropertyChanged("ForumPlace");
                }
            }
        }

        public string ForumSchedules {
            get {
                return Forum.Schedules;
            }
            set {
                if (Forum.Schedules != value) {
                    Forum.Schedules = value;

                    OnPropertyChanged("ForumSchedules");
                }
            }
        }

        private bool _activityIndicator = false;
        public bool ActivityIndicator {
            get {
                return _activityIndicator;
            }
            set {
                if (_activityIndicator != value) {
                    _activityIndicator = value;

                    OnPropertyChanged("ActivityIndicator");
                }
            }
        }

        public ForumEditViewModel(IPageService pageService) {
            ActivityIndicator = false;
            _pageService = pageService;
            _forumService = new Helpers.Forum();

            CancelCommand = new Command(Cancel);
            ConfirmCommand = new Command(() => ConfirmEdition());
        }

        public async void setOldForumFields(string OldForumId) {
            //var _oldForum = await ForumDatabase.getForumDB.Get(OldForumId);
            var _oldForum = await _forumService.GetForumAsync(OldForumId);

            ForumTitle = _oldForum.Title;
            ForumDate = _oldForum.Date;
            ForumHour = _oldForum.Hour;
            ForumPlace = _oldForum.Place;
            ForumSchedules = _oldForum.Schedules;
            Forum.RemoteId = _oldForum.RemoteId;
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(ForumTitle) ||
                    String.IsNullOrWhiteSpace(ForumPlace) ||
                    String.IsNullOrWhiteSpace(ForumSchedules));
        }

        public async void ConfirmEdition() {
            ActivityIndicator = true;
            if (IsAnyFieldBlank()) {
                ActivityIndicator = false;
                EditionFailed();
            } else {
                await EditForum();
                await _pageService.PopAsync();
            }
        }

        public async Task EditForum() {
            if (await _forumService.PutForumAsync(Forum.RemoteId, Forum) ){
                ActivityIndicator = false;
                await _pageService.DisplayAlert("Editar Fórum", "O fórum foi editado com sucesso!", "OK", "Cancelar");
            } else {
                ActivityIndicator = false;
                await _pageService.DisplayAlert("Erro!", "O fórum não pôde ser editado. Tente novamente!", "OK", "Cancelar");
            }
        }

        public async void EditionFailed() {
            await _pageService.DisplayAlert("Erro na Edição"
                , "O fórum não foi editado. Você deve preencher todos os campos."
                , "OK", "cancel");
        }

        public async void Cancel() {
            await _pageService.PopAsync();
        }
    }
}