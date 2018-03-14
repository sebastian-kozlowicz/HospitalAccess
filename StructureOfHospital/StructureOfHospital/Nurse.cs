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
    public class Nurse : Worker, ISerializable
    {
        public static List<Nurse> NurseList = new List<Nurse>();

        public Nurse(string name, string surname, long pesel, string username, string password)
            : base(name, surname, pesel, username, password)
        {

        }

        public Nurse()
        {

        }

        public void Add(Nurse nurse)
        {
            NurseList.Add(nurse);
        }

        public void Remove(int nurseNumber)
        {
            NurseList.RemoveAt(nurseNumber);
        }

        public int DisplayList()
        {
            int i;
            for (i = 0; i < NurseList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3}", i + 1, NurseList[i]._name, NurseList[i]._surname, NurseList[i]._pesel);
            }
            return i;
        }

        public int DisplayWholeList()
        {
            int i;
            for (i = 0; i < NurseList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3} login: {4} hasło: {5}", i + 1, NurseList[i]._name, NurseList[i]._surname, NurseList[i]._pesel,
                                NurseList[i]._username, NurseList[i]._password);
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
        }

        public Nurse(SerializationInfo info, StreamingContext context)
        {
            _name = (string)info.GetValue("Name", typeof(string));
            _surname = (string)info.GetValue("Surname", typeof(string));
            _pesel = (int)info.GetValue("Pesel", typeof(int));
            _username = (string)info.GetValue("Username", typeof(string));
            _password = (string)info.GetValue("Password", typeof(string));
        }
    }
}
