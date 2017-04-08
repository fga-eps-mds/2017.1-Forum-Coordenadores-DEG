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

        public ForunsViewModel() {
            _foruns = new List<Forum>();
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
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules },
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules },
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules },
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules },
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules },
                    new Forum { _title = "Forum 01", _place = "Place 01", _date = new DateTime(2017, 8, 24), _schedules = schedules }
                };
            }
            return _foruns;
        }
    }
}
