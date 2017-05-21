using System;
using ForumDEG.Models;
using ForumDEG.Utils;
using System.Collections.ObjectModel;
using ForumDEG.Interfaces;
using ForumDEG.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace ForumDEG.ViewModels {
    public class CoordinatorMasterPageViewModel : PageService, INotifyPropertyChanged {
        private string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                if (_title != value) {
                    _title = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        private string _place;
        public string Place {
            get {
                return _place;
            }
            set {
                if (_place != value) {
                    _place = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Place"));
                }
            }
        }

        private string _schedules;
        public string Schedules {
            get {
                return _schedules;
            }
            set {
                if (_schedules != value) {
                    _schedules = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Schedules"));
                }
            }
        }

        private DateTime _date;
        public DateTime Date {
            get {
                return _date;
            }
            set {
                if (_date != value) {
                    _date = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }

        private TimeSpan _hour;
        public TimeSpan Hour {
            get {
                return _hour;
            }
            set {
                if (_hour != value) {
                    _hour = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hour"));
                }
            }
        }

        private static CoordinatorMasterPageViewModel _instance = null;
        public ObservableCollection<ForumDetailViewModel> Forums { get; private set; }
        public ForumDetailViewModel SelectedForum { get; private set; }
        private readonly IPageService _pageService;
        private readonly Helpers.Forum _forumService;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DetailPageCommand { get; set; }

        public CoordinatorMasterPageViewModel(IPageService pageService) {
            _pageService = pageService;
            _forumService = new Helpers.Forum();
            DetailPageCommand = new Command(SeeDetailPage);
        }

        public static CoordinatorMasterPageViewModel GetInstance() {
            if (_instance == null)
                _instance = new CoordinatorMasterPageViewModel(new PageService());
            return _instance;
        }

        public async Task<Forum> SelectNextForum () {
            var listforum = await _forumService.GetForumsAsync();

            Forum latestForum = null;
            foreach (Forum forum in listforum) {
                Debug.WriteLine("[SelectNextForum]: picks a new forum");
                if (DateTime.Compare(DateTime.Now, forum.Date) <= 0) { 
                    if (latestForum == null) { 
                        latestForum = forum;
                        Debug.WriteLine("[SelectNextForum]: sets a value to the latest forum variable");
                    }
                    else {
                        if (DateTime.Compare(forum.Date, latestForum.Date) < 0) {
                            latestForum = forum;
                            Debug.WriteLine("[SelectNextForum]: updates the next forum");
                        }
                    }
                }
            }
            return latestForum;
        }

        public async void SelectForum() {
            Debug.WriteLine("[SelectForum]");
            Forum latestForum = null;
            latestForum = await SelectNextForum();
            if(latestForum==null) {
                latestForum = new Forum();
                latestForum.Title = "Nenhum forum disponivel";
            }
            else {
                Title = latestForum.Title;
                Place = latestForum.Place;
                Schedules = latestForum.Schedules;
                Date = latestForum.Date;
                Hour = latestForum.Hour;
                Debug.WriteLine("[SelectForum]: gets a non null forum");
                Debug.WriteLine("[SelectForum]: title: " + Title);
                Debug.WriteLine("[SelectForum]: place: " + Place);
                Debug.WriteLine("[SelectForum]: schedules: " + Schedules);
            }
            
            SelectedForum = new ForumDetailViewModel(_pageService) {
                Title = latestForum.Title,
                Place = latestForum.Place,
                Schedules = latestForum.Schedules,
                Date = latestForum.Date,
                Hour = latestForum.Hour
            };

            Debug.WriteLine("[SelectNextForum]: title " + SelectedForum.Title);
            Debug.WriteLine("[SelectNextForum]: place " + SelectedForum.Place);
            Debug.WriteLine("[SelectNextForum]: schedules " + SelectedForum.Schedules);
        }

        public async void SeeDetailPage() {
            Debug.WriteLine("[Coord. Main] - Inside SeeDetailPage");
            await _pageService.PushAsync(new ForumDetailPage(SelectedForum));
        }
    }
}
