using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class ForumDetailViewModel {
        public string Title { get; set; }
        public string Place { get; set; }
        public string Schedules { get; set; }
        public DateTime Date { get; set; }
        public bool IsPast { get; private set; }

        public ICommand ConfirmCommand { get; private set; }
        public ICommand DisconfirmCommand { get; private set; }

        public ForumDetailViewModel() {
            ConfirmCommand = new Command(ConfirmPresence);
            DisconfirmCommand = new Command(DisconfirmPresence);
            IsPast = HasPassed();
        }

        public bool HasPassed() {
            return DateTime.Now > Date;
        }

        private void ConfirmPresence() { }

        private void DisconfirmPresence() { }
    }
}
