using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using StructureOfHospital;

namespace ControlPanel
{
    public class Serialization
    {
        public static void SerializeList<T>(List<T> list, string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (Stream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(fs, list);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DeserializeList<T>(ref List<T> list, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    using (FileStream fs = File.OpenRead(fileName))
                    {
                        list = (List<T>)serializer.Deserialize(fs);
                    }
                }
                else
                {
                    Console.WriteLine("Plik {0} nie istnieje", fileName);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SerializationObj(Worker worker, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Doctor));
                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        serializer.Serialize(tw, worker);
                    }
                }
                else
                {
                    Console.WriteLine("Plik {0} nie istnieje", fileName);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public T DeserializationObj<T>(T type, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    TextReader reader = new StreamReader(fileName);
                    object obj = deserializer.Deserialize(reader);
                    reader.Close();
                    return (T)obj;
                }
                else
                {
                    Console.WriteLine("Plik {0} nie istnieje", fileName);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }
    }
}
