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
    public class Administrator : Worker, ISerializable
    {
        public static List<Administrator> AdministratorList = new List<Administrator>();

        public Administrator(string name, string surname, long pesel, string username, string password)
            : base(name, surname, pesel, username, password)
        {

        }

        public Administrator()
        {

        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                Name, Pesel);
        }

        public void Add(Administrator administrator)
        {
            AdministratorList.Add(administrator);
        }

        public void Remove(int administratorNumber)
        {
            AdministratorList.RemoveAt(administratorNumber);
        }

        public int DisplayList()
        {
            int i;
            for (i = 0; i < AdministratorList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3}", i + 1, AdministratorList[i]._name, AdministratorList[i]._surname, AdministratorList[i]._pesel);
            }
            return i;
        }

        public int DisplayWholeList()
        {
            int i;
            for (i = 0; i < AdministratorList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pesel: {3} login: {4} hasło: {5}", i + 1, AdministratorList[i]._name, AdministratorList[i]._surname, AdministratorList[i]._pesel,
                                AdministratorList[i]._username, AdministratorList[i]._password);
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

        public Administrator(SerializationInfo info, StreamingContext context)
        {
            _name = (string)info.GetValue("Name", typeof(string));
            _surname = (string)info.GetValue("Surname", typeof(string));
            _pesel = (int)info.GetValue("Pesel", typeof(int));
            _username = (string)info.GetValue("Username", typeof(string));
            _password = (string)info.GetValue("Password", typeof(string));
        }
    }
}
