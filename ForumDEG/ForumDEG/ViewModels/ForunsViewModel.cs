using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForumDEG.Models;
using System.Collections.ObjectModel;

namespace ForumDEG.ViewModels {
    class ForunsViewModel {
        private ObservableCollection<Forum> _foruns;
        private Forum _selectedForum;
        private static ForunsViewModel _instance = null;

        private ForunsViewModel() {
            _foruns = new ObservableCollection<Forum>();
        }

        public static ForunsViewModel GetInstance() {
            if (_instance == null) _instance = new ForunsViewModel();
            return _instance;
        }

        public ObservableCollection<Forum> GetUpdatedList() {
            if (_foruns.Count == 0) {
                List<string> schedules = new List<string> {
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Que tal uma pauta com o texto bem grande de teste?",
                    "Lidando com o migué",
                    "Eleições do DCE"
                };
                _foruns = new ObservableCollection<Forum> {
                    new Forum { _title = "Nome do Forum", _place = "Departamento de Audiovisual", _date = new DateTime(2017, 1, 1), _schedules = schedules },
                    new Forum { _title = "Forum 02", _place = "Place 02", _date = new DateTime(2017, 4, 17, 18, 00, 00), _schedules = schedules },
                    new Forum { _title = "Forum 03", _place = "Place 03", _date = new DateTime(2017, 4, 17, 10, 00, 00), _schedules = schedules },
                    new Forum { _title = "Forum 04", _place = "Place 04", _date = new DateTime(1996, 3, 25), _schedules = schedules },
                    new Forum { _title = "Forum 05", _place = "Place 05", _date = new DateTime(2200, 9, 15), _schedules = schedules },
                    new Forum { _title = "Forum 06", _place = "Place 06", _date = new DateTime(2525, 8, 5), _schedules = schedules }
                };
            }
            return _foruns;
        }

        public void Select(object sender) {
            _selectedForum = sender as Forum;
        }

        public Forum GetSelected() {
            return _selectedForum;
        }
    }
}
