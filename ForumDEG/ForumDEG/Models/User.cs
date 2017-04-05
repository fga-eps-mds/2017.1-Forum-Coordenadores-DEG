using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Models
{
    public class User {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        private string _name;
        public string Name { get => _name; set => _name = value; }

        private string _registration;
        public string Registration { get => _registration; set => _registration = value; }

        private string _email;
        public string Email { get => _email; set => _email = value; }

        private string _password;
        public string Password { get => _password; set => _password = value; }

        private DateTime createdOn;
        public DateTime CreatedOn { get => createdOn; set => createdOn = value; }
        

        public User () { 

        }
    }
}
