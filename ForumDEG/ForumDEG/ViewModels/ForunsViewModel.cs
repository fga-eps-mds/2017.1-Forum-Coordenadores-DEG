using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDEG.Models;
using System.Collections.ObjectModel;

namespace ForumDEG.ViewModels {
    class ForunsViewModel : BaseViewModel {
        public ObservableCollection<Forum> Foruns { get ; private set; } = new ObservableCollection<Forum>();

        private Forum _selectedForum;
        public Forum SelectedForum {
            get { return _selectedForum; }
            set { SetValue(ref _selectedForum, value); }
        }

        private static ForunsViewModel _instance = null;

        private ForunsViewModel() {

        }

        public static ForunsViewModel GetInstance() {
            if (_instance == null) _instance = new ForunsViewModel();
            return _instance;
        }

        public ObservableCollection<Forum> GetUpdatedList() {
                string schedules = "O migué do falcão ta sem condicão. Como Proceder ?";

                Foruns = new ObservableCollection<Forum> {
                    new Forum { _title = "Nome do Forum", _place = "Departamento de Audiovisual", _date = new DateTime(2017, 1, 1), _schedules = schedules },
                    new Forum { _title = "Forum 02", _place = "Place 02", _date = new DateTime(2017, 4, 17, 22, 00, 00), _schedules = schedules },
                    new Forum { _title = "Forum 03", _place = "Place 03", _date = new DateTime(2017, 4, 17, 10, 00, 00), _schedules = schedules },
                    new Forum { _title = "Forum 04", _place = "Place 04", _date = new DateTime(1996, 3, 25), _schedules = schedules },
                    new Forum { _title = "Forum 05", _place = "Place 05", _date = new DateTime(2017, 4, 17, 18, 15, 00), _schedules = schedules },
                    new Forum { _title = "Forum 06", _place = "Place 06", _date = new DateTime(2525, 8, 5), _schedules = schedules }
                };
            return Foruns;
        }

        public void Select(object sender) {
            _selectedForum = sender as Forum;
        }

        public Forum GetSelected() {
            return _selectedForum;
        }
    }
}
