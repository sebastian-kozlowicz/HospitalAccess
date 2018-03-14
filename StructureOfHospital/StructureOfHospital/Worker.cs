using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StructureOfHospital
{
    public class Worker
    {
        protected string _name;
        protected string _surname;
        protected long _pesel;
        protected string _username;
        protected string _password;

        public Worker() { }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
            }
        }

        public long Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public Worker(string name, string surname, long pesel, string username, string password)
        {
            this._name = name;
            this._surname = surname;
            this._pesel = pesel;
            this._username = username;
            this._password = password;
        }
    }
}
