using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace progmet_assignment2
{
    /* CLASS: Person
     * PURPOSE: Is used in a List (List<Person>) to contain all Persons in a addressbook.
     */
    class Person
    {
        public string name, address, phone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }

        /* METHOD: Person() (static)
         * PURPOSE: Uses Console.Readline() and Console.Write() to ask and get input from user. Uses input to assign class-variables ( attributes=?).
         * PARAMETERS: no parameters used
         * RETURN VALUE: 
         */

        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }

        /* METHOD: Print (static)
         * PURPOSE: Outputs name, address, phone, and email using string interpolation and Console.WriteLine.
         * PARAMETERS: no parameters used
         * RETURN VALUE: void method
         */
        public void Print()
        {
            Console.WriteLine($"{name}, {address}, {phone}, {email}");
        }

        /* METHOD: Modify (static)
         * PURPOSE: Uses a Switch-Case to appoint newValue to correct object-variable.
         * PARAMETERS: string sectionToModify: contains "name" || "adress" || "telefon" || "email" depending on what section user wants to modify
         *             string newValue: contains the new value that replaces the saved value.
         * RETURN VALUE: void method
         */
        public void Modify(string sectionToModify, string newValue)
        {
            switch (sectionToModify)
            {
                case "namn": name = newValue; break;
                case "adress": address = newValue; break;
                case "telefon": phone = newValue; break;
                case "email": email = newValue; break;
                default: break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> dict = new List<Person>();

            LoadList(dict);

            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    dict.Add(new Person());

                }
                else if (command == "ta bort")
                {
                    RemovePerson(dict);
                }
                else if (command == "visa")
                {
                    foreach (Person P in dict)
                    {
                        P.Print();
                    }
                }
                else if (command == "ändra")
                {
                    ModifyPerson(dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        /* METHOD: LoadList (static)
         * PURPOSE: Uses StreamReader to open and read a textfile. Adds new Persons to List<Person>.
         * PARAMETERS: List<Person> dict, the list containing all Persons in the addresslist.
         * RETURN VALUE: void method
         */
        static void LoadList(List<Person> dict)
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"C:\Users\samka\addressbook.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    dict.Add(new Person(word[0], word[1], word[2], word[3]));
                }
            }
            Console.WriteLine("klart!");
        }

        /* METHOD: RemovePerson (static)
         * PURPOSE: Searches list for same name as input using List.Find(). Removes that Person using List.Remove();
         * PARAMETERS: List<Person> dict, the list containing all Persons in the addresslist.
         * RETURN VALUE: void method
         */
        static void RemovePerson(List<Person> dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string toRemove = Console.ReadLine();
            if (dict.Remove(dict.Find(x => x.name == toRemove)))
                return;
            else
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toRemove);

        }

        /* METHOD: ModifyPerson (static)
         * PURPOSE: Using input to search for matchning name with List.Find(). Calls Person.Modify method to modify a section of that Person.
         * PARAMETERS: List<Person> dict, the list containing all Persons in the addresslist.
         * RETURN VALUE: void method
         */
        static void ModifyPerson(List<Person> dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string toModify = Console.ReadLine();

            Person foundPerson = dict.Find(x => x.name == toModify);

            if (foundPerson == null)
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toModify);
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string sectionToModify = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", sectionToModify, toModify);
                string newValue = Console.ReadLine();

                foundPerson.Modify(sectionToModify, newValue);
            }

        }
    }
}