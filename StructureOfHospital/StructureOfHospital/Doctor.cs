using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace StructureOfHospital
{
    [Serializable()]
    public enum Specialty
    {
        [EnumMember(Value = "kardiolog")]
        Kardiolog,
        [EnumMember(Value = "urolog")]
        Urolog,
        [EnumMember(Value = "neurolog")]
        Neurolog,
        [EnumMember(Value = "laryngolog")]
        Laryngolog
    }

    [Serializable]
    public class Doctor : Worker, ISerializable
    {
        public static List<Doctor> DoctorList = new List<Doctor>();
        private Specialty _specialty;
        private int _pzw;

        public Specialty Specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                _specialty = value;
            }
        }

        public int Pzw
        {
            get
            {
                return _pzw;
            }
            set
            {
                _pzw = value;
            }
        }

        public Doctor() { }

        public Doctor(string name, string surname, long pesel, string username, string password, Specialty specialty, int pzw)
            : base(name, surname, pesel, username, password)
        {
            this._specialty = specialty;
            this._pzw = pzw;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",
                _name, _surname, _specialty, _pesel);
        }

        public void Add(Doctor doctor)
        {
            DoctorList.Add(doctor);
        }

        public void Remove(int doctorNumber)
        {
            DoctorList.RemoveAt(doctorNumber);
        }

        public int DisplayList()
        {
            int i;
            for (i = 0; i < DoctorList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3} specjalność: {4} pzw: {5}", i + 1, DoctorList[i]._name,
                                 DoctorList[i]._surname, DoctorList[i]._pesel, DoctorList[i]._specialty.ToString().ToLower(),
                                 DoctorList[i]._pzw);
            }
            return i;
        }

        public int DisplayWholeList()
        {
            int i;
            for (i = 0; i < DoctorList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3} specjalność: {4} pzw: {5} login: {6} hasło: {7}", i + 1, DoctorList[i]._name,
                                 DoctorList[i]._surname, DoctorList[i]._pesel, DoctorList[i]._specialty.ToString().ToLower(),
                                 DoctorList[i]._pzw, DoctorList[i]._username, DoctorList[i]._password);
            }
            return i;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", _name);
            info.AddValue("Surname", _surname);
            info.AddValue("Pesel", _pesel);
            info.AddValue("Username", _username);
            info.AddValue("Password", _password);
            info.AddValue("Specialty", _specialty);
            info.AddValue("PZW", _pzw);
        }

        public Doctor(SerializationInfo info, StreamingContext context)
        {
            _name = (string)info.GetValue("Name", typeof(string));
            _surname = (string)info.GetValue("Surname", typeof(string));
            _pesel = (int)info.GetValue("Pesel", typeof(int));
            _username = (string)info.GetValue("Username", typeof(string));
            _password = (string)info.GetValue("Password", typeof(string));
            _specialty = (Specialty)info.GetValue("Specialty", typeof(Specialty));
            _pzw = (int)info.GetValue("PZW", typeof(int));
        }
    }
}
