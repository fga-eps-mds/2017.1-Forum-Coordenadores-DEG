using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForumDEG.Models;
using System.Collections.ObjectModel;

namespace ForumDEG.ViewModels {
    class ForunsViewModel {
        private List<Forum> _foruns;
        private Forum _selectedForum;
        private static ForunsViewModel _instance = null;

        private ForunsViewModel() {
            _foruns = new List<Forum>();
        }

        public static ForunsViewModel GetInstance() {
            if (_instance == null) _instance = new ForunsViewModel();
            return _instance;
        }

        public List<Forum> GetUpdatedList() {
            if (_foruns.Count == 0) {
                List<string> schedules = new List<string> {
                    "Pauta 01",
                    "Pauta 02",
                    "Pauta 03"
                };
                _foruns = new List<Forum> { 
                    //example
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 1, 1), _schedules = schedules },
                    new Forum { _title = "Forum 02", _place = "Place 02", _date = new DateTime(2016, 4, 10), _schedules = schedules },
                    new Forum { _title = "Forum 03", _place = "Place 03", _date = new DateTime(1996, 12, 20), _schedules = schedules },
                    new Forum { _title = "Forum 04", _place = "Place 04", _date = new DateTime(2018, 3, 25), _schedules = schedules },
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
