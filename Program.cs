using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace progmet_assignment2
{
    class Person
    {
        public string name, address, phone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }

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
            //dict.Add(new Person(name, adress, telefon, email));
        }

        public void Print()
        {
            Console.WriteLine($"{name}, {address}, {phone}, {email}");
        }

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

        static void RemovePerson(List<Person> dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string toRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < dict.Count(); i++)
            {
                if (dict[i].name == toRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toRemove);
            }
            else
            {
                dict.RemoveAt(found);
            }
        }

        static void ModifyPerson(List<Person> dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string toModify = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < dict.Count(); i++)
            {
                if (dict[i].name == toModify) found = i;
            }

            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toModify);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string sectionToModify = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", sectionToModify, toModify);
                string newValue = Console.ReadLine();

                dict[found].Modify(sectionToModify, newValue);
            }
        }
    }
}
