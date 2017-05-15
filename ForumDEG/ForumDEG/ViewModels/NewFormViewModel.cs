using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForumDEG.ViewModels {
    class NewFormViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PlusButtonClickedCommand { get; set; }

        private float TapCount = 0;

        public float _tapCount {
            get {
                return _tapCount;
            }
            set {
                if (_tapCount != value)
                    _tapCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TapCount"));
            }
        }

        private bool _extraButtonsVisibility;

        public bool ExtraButtonsVisibility {
            get {
                return _extraButtonsVisibility;
            }
            set {
                if (_extraButtonsVisibility != value) {
                    _extraButtonsVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExtraButtonsVisibility"));
                }
            }
        }

        public NewFormViewModel() {
            PlusButtonClickedCommand = new Command(async () => await PlusButtonClicked());

            ExtraButtonsVisibility = false;
            TapCount = 0;
        }

        private async Task PlusButtonClicked() {
            TapCount++;
            if (TapCount % 2 == 0) {
                ExtraButtonsVisibility = false;
            } else {
                ExtraButtonsVisibility = true;
            }
        }
    }
}
