using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureOfHospital;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ControlPanel
{
    class Program
    {
        static bool userLogged = false;
        static int userTypeLogged = -1; //określa typ użytkownika 0 - Doctor, 1 - Nurse, 2 - Administrator

        Doctor d = new Doctor();
        Nurse n = new Nurse();
        Administrator a = new Administrator();

        static bool UserSearch(string username)
        {
            bool userFound = false;

            List<Worker> WorkerList = new List<Worker>();

            for (int i = 0; i < Doctor.DoctorList.Count(); i++)
            {
                WorkerList.Add(Doctor.DoctorList[i]);
            }

            for (int i = 0; i < Administrator.AdministratorList.Count(); i++)
            {
                WorkerList.Add(Administrator.AdministratorList[i]);
            }

            for (int i = 0; i < Nurse.NurseList.Count(); i++)
            {
                WorkerList.Add(Nurse.NurseList[i]);
            }

            for (int i = 0; i < WorkerList.Count(); i++)
            {
                if (WorkerList[i].Username == username)
                {
                    userFound = true;
                    return true;
                }
            }

            if (userFound == false)
            {
                Console.Clear();
                Console.WriteLine("Błędny login\n");
            }
            return false;
        }
        //########################################################################################################################
        void UserLogin(string username, string password)
        {
            bool userFound = false;
            while (userFound == false)
            {
                for (int i = 0; i < Doctor.DoctorList.Count(); i++)
                {
                    if (Doctor.DoctorList[i].Username == username && Doctor.DoctorList[i].Password == password)
                    {
                        Console.WriteLine("Zalogowano, witaj {0}\n", Doctor.DoctorList[i].Name);
                        userLogged = true;
                        userFound = true;
                        CreateUserInstance(0, i);
                        break;
                    }
                    else if (Doctor.DoctorList[i].Username == username && Doctor.DoctorList[i].Password != password)
                    {
                        Console.WriteLine("Podano błędne hasło\n");
                        userFound = true;
                        break;
                    }
                }

                if (userFound == true)
                {
                    break;
                }

                for (int i = 0; i < Nurse.NurseList.Count(); i++)
                {
                    if (Nurse.NurseList[i].Username == username && Nurse.NurseList[i].Password == password)
                    {
                        Console.WriteLine("Zalogowano, witaj {0}\n", Nurse.NurseList[i].Name);
                        userLogged = true;
                        userFound = true;
                        CreateUserInstance(1, i);
                        break;
                    }
                    else if (Nurse.NurseList[i].Username == username && Nurse.NurseList[i].Password != password)
                    {
                        Console.WriteLine("Podano błędne hasło\n");
                        userFound = true;
                        break;
                    }
                }

                if (userFound == true)
                {
                    break;
                }

                for (int i = 0; i < Administrator.AdministratorList.Count(); i++)
                {
                    if (Administrator.AdministratorList[i].Username == username && Administrator.AdministratorList[i].Password == password)
                    {
                        Console.WriteLine("Zalogowano, witaj {0}\n", Administrator.AdministratorList[i].Name);
                        userLogged = true;
                        userFound = true;
                        CreateUserInstance(2, i);
                        break;
                    }
                    else if (Administrator.AdministratorList[i].Username == username && Administrator.AdministratorList[i].Password != password)
                    {
                        Console.WriteLine("Podano błędne hasło\n");
                        userFound = true;
                        break;
                    }
                }

                if (userFound == false)
                {
                    Console.WriteLine("Podano zły login i hasło\n");
                    break;
                }
            }
        }
        //########################################################################################################################
        void SetLoginValues()
        {
            string username;
            string password;

            if (userLogged == false)
            {
                while (true)
                {
                    Console.WriteLine("Podaj login");

                    username = Console.ReadLine();
                    try
                    {
                        if (string.IsNullOrEmpty(username))
                        {
                            Console.Clear();
                            throw new ArgumentException("Podałeś pusty ciąg\nSpróbuj jeszcze raz");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (UserSearch(username))
                {
                    while (true)
                    {
                        Console.WriteLine("Podaj hasło");

                        password = Console.ReadLine();

                        try
                        {
                            if (string.IsNullOrEmpty(password))
                            {
                                Console.Clear();
                                throw new ArgumentException("Podałeś pusty ciąg\nSpróbuj jeszcze raz");
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (SystemException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.Clear();
                    UserLogin(username, password);
                }
            }
        }
        //########################################################################################################################
        object CreateUserInstance(int userType, int i) //parametr userType określa typ użytkownika 0 - Doctor, 1 - Nurse, 2 - Administrator
        {
            if (userType == 0)
            {
                d = new Doctor(Doctor.DoctorList[i].Username, Doctor.DoctorList[i].Surname,
                               Doctor.DoctorList[i].Pesel, Doctor.DoctorList[i].Username,
                               Doctor.DoctorList[i].Password, Doctor.DoctorList[i].Specialty, Doctor.DoctorList[i].Pzw);
                userTypeLogged = 0;
                return d;
            }
            else if (userType == 1)
            {
                n = new Nurse(Nurse.NurseList[i].Username, Nurse.NurseList[i].Surname,
                              Nurse.NurseList[i].Pesel, Nurse.NurseList[i].Username,
                              Nurse.NurseList[i].Password);
                userTypeLogged = 1;
                return n;
            }
            else if (userType == 2)
            {
                a = new Administrator(Administrator.AdministratorList[i].Username, Administrator.AdministratorList[i].Surname,
                                      Administrator.AdministratorList[i].Pesel, Administrator.AdministratorList[i].Username,
                                      Administrator.AdministratorList[i].Password);
                userTypeLogged = 2;
                return a;
            }
            return -1;
        }
        //########################################################################################################################
        static void DisplayWorkerList()
        {
            int index = 1;
            for (int i = 0; i < Doctor.DoctorList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} lekarz {3}", index, Doctor.DoctorList[i].Name,
                                 Doctor.DoctorList[i].Surname, Doctor.DoctorList[i].Specialty.ToString().ToLower());
                index++;
            }
            for (int i = 0; i < Nurse.NurseList.Count(); i++)
            {
                Console.WriteLine("{0}: {1} {2} pielęgniarka", index, Nurse.NurseList[i].Name,
                                 Nurse.NurseList[i].Surname);
                index++;
            }

            if (userTypeLogged == 2)
            {

                for (int i = 0; i < Administrator.AdministratorList.Count(); i++)
                {
                    Console.WriteLine("{0}: {1} {2} administrator", index, Administrator.AdministratorList[i].Name,
                                     Administrator.AdministratorList[i].Surname);
                    index++;
                }
            }
            Console.WriteLine();
        }
        //########################################################################################################################
        void AddUser()
        {
            int switchOption;

            while (true)
            {
                Console.WriteLine("Wybierz typ użytkownika, który chcesz dodać:");
                Console.WriteLine("1. Lekarz");
                Console.WriteLine("2. Pielęgniarka");
                Console.WriteLine("3. Administrator");
                Console.Write("Wybór: ");

                while (true)
                {
                    try
                    {
                        switchOption = int.Parse(Console.ReadLine());

                        if (switchOption > 3 || switchOption < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Spróbuj jeszcze raz\n");
                    Console.WriteLine("Wybierz typ użytkownika, który chcesz dodać:");
                    Console.WriteLine("1. Lekarz");
                    Console.WriteLine("2. Pielęgniarka");
                    Console.WriteLine("3. Administrator");
                    Console.Write("Wybór: ");
                }

                switch (switchOption)
                {
                    case 1:
                        SetUserData(0);
                        break;
                    case 2:
                        SetUserData(1);
                        break;
                    case 3:
                        SetUserData(2);
                        break;
                }
                break;
            }
        }
        //########################################################################################################################
        static bool IfLoginIsAlreadyUsed(string username)
        {
            List<Worker> WorkerList = new List<Worker>();

            for (int i = 0; i < Doctor.DoctorList.Count(); i++)
            {
                WorkerList.Add(Doctor.DoctorList[i]);
            }

            for (int i = 0; i < Administrator.AdministratorList.Count(); i++)
            {
                WorkerList.Add(Administrator.AdministratorList[i]);
            }

            for (int i = 0; i < Nurse.NurseList.Count(); i++)
            {
                WorkerList.Add(Nurse.NurseList[i]);
            }

            for (int i = 0; i < WorkerList.Count(); i++)
            {
                if (WorkerList[i].Username == username)
                {
                    Console.WriteLine("\nLogin jest już zajęty");
                    return true;
                }
            }
            return false;
        }
        //########################################################################################################################
        static bool PeselValidation(string pesel)
        {
            if (pesel.Length == 11)
            {
                int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                bool result = false;

                int controlSum = CalculateControlSum(pesel, weights);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(pesel[pesel.Length - 1].ToString());
                result = controlNum == lastDigit;

                if (result)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nPesel jest nieprawidłowy");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\nPesel musi zawierać 11 cyfr");
                return false;
            }
        }
        //########################################################################################################################
        static int CalculateControlSum(string pesel, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < pesel.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(pesel[i].ToString());
            }
            return controlSum;
        }

        //########################################################################################################################
        void SetUserData(int userType) //parametr userType określa typ użytkownika 0 - Doctor, 1 - Nurse, 2 - Administrator
        {
            string name;
            string surname;
            long pesel;
            string username;
            string password;

            Console.Clear();

            name = SetName();
            surname = SetSurname();
            pesel = SetPesel();
            username = SetUsername();
            password = SetPassword();

            if (userType == 0)
            {
                AddDoctor(name, surname, pesel, username, password);
            }
            else if (userType == 1)
            {
                n = new Nurse(name, surname, pesel, username, password);
                n.Add(n);
            }
            else if (userType == 2)
            {
                a = new Administrator(name, surname, pesel, username, password);
                a.Add(a);
            }
            Console.Clear();
            Console.WriteLine("Dodano użytkownika\n");
        }
        //########################################################################################################################
        static string SetName()
        {
            string name;

            Console.WriteLine("Podaj imię");
            while (true)
            {
                try
                {
                    name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        throw new ArgumentException("Podałeś pusty ciąg");
                    }
                    else
                    {
                        if (!IfLoginIsAlreadyUsed(name))
                            break;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj imię");
            }
            return name;
        }
        //########################################################################################################################
        static string SetSurname()
        {
            string surname;

            Console.WriteLine("Podaj nazwisko");
            while (true)
            {
                try
                {
                    surname = Console.ReadLine();

                    if (string.IsNullOrEmpty(surname))
                    {
                        throw new ArgumentException("Podałeś pusty ciąg");
                    }
                    else
                    {
                        if (!IfLoginIsAlreadyUsed(surname))
                            break;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj nazwisko");
            }
            return surname;
        }
        //########################################################################################################################
        static long SetPesel()
        {
            long pesel;

            Console.WriteLine("Podaj pesel");
            while (true)
            {
                string readPesel = Console.ReadLine();
                bool checkTryParse = long.TryParse(readPesel, out pesel);

                if (!checkTryParse)
                {
                    try
                    {
                        throw new ArgumentException("Podałeś pusty ciąg");
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine("\n" + e.Message);
                    }
                }
                else
                {
                    if (PeselValidation(readPesel))
                        break;
                }

                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj pesel");
            }
            return pesel;
        }
        //######################################################################################################################## 
        static string SetUsername()
        {
            string username;

            Console.WriteLine("Podaj nazwę użytkownika");
            while (true)
            {
                try
                {
                    username = Console.ReadLine();

                    if (string.IsNullOrEmpty(username))
                    {
                        throw new ArgumentException("Podałeś pusty ciąg");
                    }
                    else
                    {
                        if (!IfLoginIsAlreadyUsed(username))
                            break;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj nazwę użytkownika");
            }
            return username;
        }
        //########################################################################################################################
        static string SetPassword()
        {
            string password;

            Console.WriteLine("Podaj hasło użytkownika");
            while (true)
            {
                try
                {
                    password = Console.ReadLine();

                    if (string.IsNullOrEmpty(password))
                    {
                        throw new ArgumentException("Podałeś pusty ciąg");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj hasło użytkownika");
            }
            return password;
        }
        //######################################################################################################################## 
        static int SetPzw()
        {
            int pzw;

            Console.WriteLine("Podaj numer PZW");
            while (true)
            {
                try
                {
                    pzw = int.Parse(Console.ReadLine());
                    if (pzw.ToString().Length == 7)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nNumer PZW musi składać się z 7 cyfr");
                    }

                }
                catch (SystemException e)
                {
                    Console.WriteLine("\n" + e.Message);
                }
                Console.WriteLine("Spróbuj jeszcze raz\n");
                Console.WriteLine("Podaj numer PZW");
            }
            return pzw;
        }
        //########################################################################################################################
        static Specialty SetSpecialty()
        {
            int switchOption;
            while (true)
            {
                Console.WriteLine("Wybierz specjalizację lekarza:");
                Console.WriteLine("1. Kardiolog");
                Console.WriteLine("2. Urolog");
                Console.WriteLine("3. Neurolog");
                Console.WriteLine("4. Laryngolog");
                Console.Write("Wybór: ");

                while (true)
                {
                    try
                    {
                        switchOption = int.Parse(Console.ReadLine());

                        if (switchOption > 4 || switchOption < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine("\n" + e.Message);
                    }
                    Console.WriteLine("Spróbuj jeszcze raz\n");
                    Console.WriteLine("Wybierz specjalizację lekarza:");
                    Console.WriteLine("1. Kardiolog");
                    Console.WriteLine("2. Urolog");
                    Console.WriteLine("3. Neurolog");
                    Console.WriteLine("4. Laryngolog");
                    Console.Write("Wybór: ");
                }
                switch (switchOption)
                {
                    case 1:
                        return Specialty.Kardiolog;
                    case 2:
                        return Specialty.Urolog;
                    case 3:
                        return Specialty.Neurolog;
                    case 4:
                        return Specialty.Laryngolog;
                }
            }
        }
        //########################################################################################################################
        void AddDoctor(string name, string surname, long pesel, string username, string password)
        {
            int pzw;
            Specialty specialty;

            pzw = SetPzw();
            specialty = SetSpecialty();

            d = new Doctor(name, surname, pesel, username, password, specialty, pzw);
            d.Add(d);
        }
        //########################################################################################################################
        void RemoveUserMenu()
        {
            int switchOption;

            while (true)
            {
                Console.WriteLine("Wybierz typ użytkownika, który chcesz usunąć:");
                Console.WriteLine("1. Lekarz");
                Console.WriteLine("2. Pielęgniarka");
                Console.WriteLine("3. Administrator");
                Console.Write("Wybór: ");

                while (true)
                {
                    try
                    {
                        switchOption = int.Parse(Console.ReadLine());

                        if (switchOption > 3 || switchOption < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Spróbuj jeszcze raz\n");
                    Console.WriteLine("Wybierz typ użytkownika, który chcesz usunąć:");
                    Console.WriteLine("1. Lekarz");
                    Console.WriteLine("2. Pielęgniarka");
                    Console.WriteLine("3. Administrator");
                    Console.Write("Wybór: ");
                }

                switch (switchOption)
                {
                    case 1:
                        Console.Clear();
                        RemoveUser(d.DisplayList(), 0);
                        break;
                    case 2:
                        Console.Clear();
                        RemoveUser(n.DisplayList(), 1);
                        break;
                    case 3:
                        Console.Clear();
                        RemoveUser(a.DisplayList(), 2);
                        break;
                }
                break;
            }
        }
        //########################################################################################################################
        void RemoveUser(int lastIndex, int listType) // parametr listType określa listę użytkowników 0 - lista lekarzy, 1 - lista pielęgniarek, 2 - lista administratorów
        {
            int userIndex;
            while (true)
            {
                Console.WriteLine("\nPodaj indeks pracownika do usunięcia:");
                Console.Write("Wybór:");

                while (true)
                {
                    try
                    {
                        userIndex = int.Parse(Console.ReadLine());

                        if (userIndex > lastIndex || userIndex < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer\n");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message + "\n");
                    }
                    if (listType == 0)
                        d.DisplayList();
                    else if (listType == 1)
                        n.DisplayList();
                    else if (listType == 2)
                        a.DisplayList();

                    Console.WriteLine("\nPodaj indeks pracownika do usunięcia:");
                    Console.Write("Wybór:");
                }

                if (listType == 0)
                {
                    d.Remove(userIndex - 1);
                }
                else if (listType == 1)
                {
                    n.Remove(userIndex - 1);
                }
                else if (listType == 2)
                {
                    a.Remove(userIndex - 1);
                }

                Console.Clear();
                Console.WriteLine("Usunięto użytkownika\n");
                break;
            }
        }
        //########################################################################################################################
        void UpdateUserMenu()
        {
            int switchOption;

            while (true)
            {
                Console.WriteLine("Wybierz typ użytkownika, którego chcesz zmodyfikować:");
                Console.WriteLine("1. Lekarz");
                Console.WriteLine("2. Pielęgniarka");
                Console.WriteLine("3. Administrator");
                Console.Write("Wybór: ");

                while (true)
                {
                    try
                    {
                        switchOption = int.Parse(Console.ReadLine());

                        if (switchOption > 3 || switchOption < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Spróbuj jeszcze raz\n");
                    Console.WriteLine("Wybierz typ użytkownika, którego chcesz zmodyfikować:");
                    Console.WriteLine("1. Lekarz");
                    Console.WriteLine("2. Pielęgniarka");
                    Console.WriteLine("3. Administrator");
                    Console.Write("Wybór: ");
                }

                switch (switchOption)
                {
                    case 1:
                        Console.Clear();
                        UpdateUser(d.DisplayWholeList(), 0);
                        break;
                    case 2:
                        Console.Clear();
                        UpdateUser(n.DisplayWholeList(), 1);
                        break;
                    case 3:
                        Console.Clear();
                        UpdateUser(a.DisplayWholeList(), 2);
                        break;
                }
                break;
            }
        }
        //########################################################################################################################
        void UpdateUser(int lastIndex, int listType) // parametr listType określa listę użytkowników 0 - lista lekarzy, 1 - lista pielęgniarek, 2 - lista administratorów
        {
            int userIndex;
            while (true)
            {
                Console.WriteLine("\nPodaj indeks pracownika do zmodyfikowania:");
                Console.Write("Wybór:");

                while (true)
                {
                    try
                    {
                        userIndex = int.Parse(Console.ReadLine());

                        if (userIndex > lastIndex || userIndex < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer\n");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message + "\n");
                    }
                    if (listType == 0)
                        d.DisplayWholeList();
                    else if (listType == 1)
                        n.DisplayWholeList();
                    else if (listType == 2)
                        a.DisplayWholeList();

                    Console.WriteLine("\nPodaj indeks pracownika do zmodyfikowania:");
                    Console.Write("Wybór:");
                }

                SetUpdateValues(userIndex, listType);

                Console.Clear();
                Console.WriteLine("Dane użytkownika zostały zmodyfikowane\n");
                break;
            }
        }
        void SetUpdateValues(int userIndex, int listType) // parametr listType określa listę użytkowników 0 - lista lekarzy, 1 - lista pielęgniarek, 2 - lista administratorów
        {
            int switchOption;
            string name;
            string surname;
            long pesel;
            string username;
            string password;
            Specialty specialty;
            int pzw;

            Console.Clear();

            if (listType == 0)
            {
                do
                {
                    while (true)
                    {
                        Console.WriteLine("Wybierz atrybut, który chcesz zmodyfikować:");
                        Console.WriteLine("1. Imię");
                        Console.WriteLine("2. Nazwisko");
                        Console.WriteLine("3. Pesel");
                        Console.WriteLine("4. Login");
                        Console.WriteLine("5. Hasło");
                        Console.WriteLine("6. Specjalizacja");
                        Console.WriteLine("7. Numer PZW");
                        Console.WriteLine("8. Zakończ edycję");
                        Console.Write("Wybór: ");

                        while (true)
                        {
                            try
                            {
                                switchOption = int.Parse(Console.ReadLine());

                                if (switchOption > 8 || switchOption < 1)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Podano błędny numer\n");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch (SystemException e)
                            {
                                Console.Clear();
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("Spróbuj jeszcze raz\n");
                            Console.WriteLine("Wybierz atrybut, który chcesz zmodyfikować:");
                            Console.WriteLine("1. Imię");
                            Console.WriteLine("2. Nazwisko");
                            Console.WriteLine("3. Pesel");
                            Console.WriteLine("4. Login");
                            Console.WriteLine("5. Hasło");
                            Console.WriteLine("6. Specjalizacja");
                            Console.WriteLine("7. Numer PZW");
                            Console.WriteLine("8. Zakończ edycję");
                            Console.Write("Wybór: ");
                        }

                        switch (switchOption)
                        {
                            case 1:
                                Console.Clear();
                                name = SetName();
                                Doctor.DoctorList[userIndex - 1].Name = name;
                                break;
                            case 2:
                                Console.Clear();
                                surname = SetSurname();
                                Doctor.DoctorList[userIndex - 1].Surname = surname;
                                break;
                            case 3:
                                Console.Clear();
                                pesel = SetPesel();
                                Doctor.DoctorList[userIndex - 1].Pesel = pesel;
                                break;
                            case 4:
                                Console.Clear();
                                username = SetUsername();
                                Doctor.DoctorList[userIndex - 1].Username = username;
                                break;
                            case 5:
                                Console.Clear();
                                password = SetPassword();
                                Doctor.DoctorList[userIndex - 1].Password = password;
                                break;
                            case 6:
                                Console.Clear();
                                specialty = SetSpecialty();
                                Doctor.DoctorList[userIndex - 1].Specialty = specialty;
                                break;
                            case 7:
                                Console.Clear();
                                pzw = SetPzw();
                                Doctor.DoctorList[userIndex - 1].Pzw = pzw;
                                break;
                        }
                        Console.WriteLine();
                        break;
                    }
                }
                while (switchOption != 8);
            }
            else if (listType == 1 || listType == 2)
            {
                do
                {
                    while (true)
                    {
                        Console.WriteLine("Wybierz atrybut, który chcesz zmodyfikować:");
                        Console.WriteLine("1. Imię");
                        Console.WriteLine("2. Nazwisko");
                        Console.WriteLine("3. Pesel");
                        Console.WriteLine("4. Login");
                        Console.WriteLine("5. Hasło");
                        Console.WriteLine("6. Zakończ edycję");
                        Console.Write("Wybór: ");

                        while (true)
                        {
                            try
                            {
                                switchOption = int.Parse(Console.ReadLine());

                                if (switchOption > 6 || switchOption < 1)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Podano błędny numer\n");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch (SystemException e)
                            {
                                Console.Clear();
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("Spróbuj jeszcze raz\n");
                            Console.WriteLine("Wybierz atrybut, który chcesz zmodyfikować:");
                            Console.WriteLine("1. Imię");
                            Console.WriteLine("2. Nazwisko");
                            Console.WriteLine("3. Pesel");
                            Console.WriteLine("4. Login");
                            Console.WriteLine("5. Hasło");
                            Console.WriteLine("6. Zakończ edycję");
                            Console.Write("Wybór: ");
                        }

                        if (listType == 1)
                        {
                            switch (switchOption)
                            {
                                case 1:
                                    Console.Clear();
                                    name = SetName();
                                    Nurse.NurseList[userIndex - 1].Name = name;
                                    break;
                                case 2:
                                    Console.Clear();
                                    surname = SetSurname();
                                    Nurse.NurseList[userIndex - 1].Surname = surname;
                                    break;
                                case 3:
                                    Console.Clear();
                                    pesel = SetPesel();
                                    Nurse.NurseList[userIndex - 1].Pesel = pesel;
                                    break;
                                case 4:
                                    Console.Clear();
                                    username = SetUsername();
                                    Nurse.NurseList[userIndex - 1].Username = username;
                                    break;
                                case 5:
                                    Console.Clear();
                                    password = SetPassword();
                                    Nurse.NurseList[userIndex - 1].Password = password;
                                    break;
                            }
                        }
                        else if (listType == 2)
                        {
                            switch (switchOption)
                            {
                                case 1:
                                    Console.Clear();
                                    name = SetName();
                                    Administrator.AdministratorList[userIndex - 1].Name = name;
                                    break;
                                case 2:
                                    Console.Clear();
                                    surname = SetSurname();
                                    Administrator.AdministratorList[userIndex - 1].Surname = surname;
                                    break;
                                case 3:
                                    Console.Clear();
                                    pesel = SetPesel();
                                    Administrator.AdministratorList[userIndex - 1].Pesel = pesel;
                                    break;
                                case 4:
                                    Console.Clear();
                                    username = SetUsername();
                                    Administrator.AdministratorList[userIndex - 1].Username = username;
                                    break;
                                case 5:
                                    Console.Clear();
                                    password = SetPassword();
                                    Administrator.AdministratorList[userIndex - 1].Password = password;
                                    break;
                            }
                        }
                        break;
                    }
                }
                while (switchOption != 6);
            }
        }
        //########################################################################################################################
        static void UserNotLoggedMenu(Program p)
        {
            int switchOption;
            do
            {
                Console.WriteLine("MENU PROGRAMU:");
                Console.WriteLine("1. Zaloguj się");
                Console.WriteLine("2. Zamknij program");
                Console.Write("Wybór: ");

                while (true)
                {
                    try
                    {
                        switchOption = int.Parse(Console.ReadLine());

                        if (switchOption > 2 || switchOption < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podano błędny numer\n");
                        }
                        break;
                    }
                    catch (SystemException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Spróbuj jeszcze raz\n");
                    Console.WriteLine("MENU PROGRAMU:");
                    Console.WriteLine("1. Zaloguj się");
                    Console.WriteLine("2. Zamknij program");
                    Console.Write("Wybór: ");
                }

                switch (switchOption)
                {
                    case 1:
                        Console.Clear();
                        p.SetLoginValues();
                        if (userLogged)
                        {
                            if (UserLoggedMenu(p))
                            {
                                switchOption = 2;
                            }
                        }
                        break;
                }
            }
            while (switchOption != 2);

            Serialization.SerializeList(Doctor.DoctorList, "Doctors.xml");
            Serialization.SerializeList(Nurse.NurseList, "Nurses.xml");
            Serialization.SerializeList(Administrator.AdministratorList, "Administrators.xml");
        }

        static bool UserLoggedMenu(Program p)
        {
            int switchOption;

            if (userTypeLogged == 0 || userTypeLogged == 1)
            {
                do
                {
                    Console.WriteLine("MENU PROGRAMU:");
                    Console.WriteLine("1. Wyloguj się");
                    Console.WriteLine("2. Wyświetl listę pracowników");
                    Console.WriteLine("3. Zamknij program");
                    Console.Write("Wybór: ");

                    while (true)
                    {
                        try
                        {
                            switchOption = int.Parse(Console.ReadLine());

                            if (switchOption > 3 || switchOption < 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Podano błędny numer\n");
                            }

                            break;
                        }
                        catch (SystemException e)
                        {
                            Console.Clear();
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Spróbuj jeszcze raz\n");
                        Console.WriteLine("MENU PROGRAMU:");
                        Console.WriteLine("1. Wyloguj się");
                        Console.WriteLine("2. Wyświetl listę pracowników");
                        Console.WriteLine("3. Zamknij program");
                        Console.Write("Wybór: ");
                    }

                    switch (switchOption)
                    {
                        case 1:
                            Console.Clear();
                            userLogged = false;
                            Console.WriteLine("Wylogowano\n");
                            switchOption = 3;
                            break;
                        case 2:
                            Console.Clear();
                            DisplayWorkerList();
                            break;
                        case 3:
                            Console.Clear();
                            return true;
                    }
                }
                while (switchOption != 3);
            }
            else if (userTypeLogged == 2)
            {
                do
                {
                    Console.WriteLine("MENU PROGRAMU:");
                    Console.WriteLine("1. Wyloguj się");
                    Console.WriteLine("2. Wyświetl listę pracowników");
                    Console.WriteLine("3. Dodaj nowego użytkownika");
                    Console.WriteLine("4. Modyfikuj istniejącego użytkownika");
                    Console.WriteLine("5. Usuń istniejącego użytkownika");
                    Console.WriteLine("6. Zamknij program");

                    Console.Write("Wybór: ");

                    while (true)
                    {
                        try
                        {
                            switchOption = int.Parse(Console.ReadLine());

                            if (switchOption > 6 || switchOption < 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Podano błędny numer\n");
                            }
                            break;
                        }
                        catch (SystemException e)
                        {
                            Console.Clear();
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Spróbuj jeszcze raz\n");
                        Console.WriteLine("MENU PROGRAMU:");
                        Console.WriteLine("1. Wyloguj się");
                        Console.WriteLine("2. Wyświetl listę pracowników");
                        Console.WriteLine("3. Dodaj nowego użytkownika");
                        Console.WriteLine("4. Modyfikuj istniejącego użytkownika");
                        Console.WriteLine("5. Usuń istniejącego użytkownika");
                        Console.WriteLine("6. Zamknij program");
                        Console.Write("Wybór: ");
                    }

                    switch (switchOption)
                    {
                        case 1:
                            Console.Clear();
                            userLogged = false;
                            Console.WriteLine("Wylogowano\n");
                            switchOption = 6;
                            break;
                        case 2:
                            Console.Clear();
                            DisplayWorkerList();
                            break;
                        case 3:
                            Console.Clear();
                            p.AddUser();
                            break;
                        case 4:
                            Console.Clear();
                            p.UpdateUserMenu();
                            break;
                        case 5:
                            Console.Clear();
                            p.RemoveUserMenu();
                            break;
                        case 6:
                            return true;
                    }
                }
                while (switchOption != 6);
            }
            return false;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Serialization s = new Serialization();

            Serialization.DeserializeList(ref Doctor.DoctorList, "Doctors.xml");
            Serialization.DeserializeList(ref Nurse.NurseList, "Nurses.xml");
            Serialization.DeserializeList(ref Administrator.AdministratorList, "Administrators.xml");

            UserNotLoggedMenu(p);
        }
    }
}
