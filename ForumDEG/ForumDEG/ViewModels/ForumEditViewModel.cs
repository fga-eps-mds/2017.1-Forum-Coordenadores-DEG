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
    class ForumEditViewModel : BaseViewModel {
        private readonly IPageService _pageService;
        
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

        public ForumEditViewModel(IPageService pageService) {
            _pageService = pageService;

            CancelCommand = new Command(Cancel);
            ConfirmCommand = new Command(() => ConfirmEdition());
        }

        public async void setOldForumFields(int OldForumId) {
            var _oldForum = await ForumDatabase.getForumDB.Get(OldForumId);

            ForumTitle = _oldForum.Title;
            ForumDate = _oldForum.Date;
            ForumHour = _oldForum.Hour;
            ForumPlace = _oldForum.Place;
            ForumSchedules = _oldForum.Schedules;
            Forum.Id = _oldForum.Id;
        }

        public bool IsAnyFieldBlank() {
            return (String.IsNullOrWhiteSpace(Forum.Title) ||
                    String.IsNullOrWhiteSpace(Forum.Place) ||
                    String.IsNullOrWhiteSpace(Forum.Schedules));
        }

        public async void ConfirmEdition() {
            if (IsAnyFieldBlank()) {
                EditionFailed();
            } else {
                await EditForum();
                await _pageService.PopAsync();
            }
        }

        public async Task EditForum() {
            await ForumDatabase.getForumDB.Save(Forum);

            await _pageService.DisplayAlert("Fórum Editado"
                , "O fórum foi editado com sucesso. Os coordenadores serão notificados em breve."
                , "OK", "cancel");
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