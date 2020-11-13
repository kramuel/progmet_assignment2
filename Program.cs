using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace progmet_assignment2
{
    class Person
    {
        public string namn, adress, telefon, email;
        public Person(string N, string A, string T, string E)
        {
            namn = N; adress = A; telefon = T; email = E;
        }

        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            namn = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            telefon = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
            //dict.Add(new Person(name, adress, telefon, email));
        }

        public void Print()
        {
            Console.WriteLine($"{namn}, {adress}, {telefon}, {email}");
        }

        public void Modify(string fältAttÄndra, string nyttVärde)
        {
            switch (fältAttÄndra)
            {
                case "namn": namn = nyttVärde; break;
                case "adress": adress = nyttVärde; break;
                case "telefon": telefon = nyttVärde; break;
                case "email": email = nyttVärde; break;
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
                    ChangePerson(dict);
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
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }

        static void RemovePerson(List<Person> dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string villTaBort = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < dict.Count(); i++)
            {
                if (dict[i].namn == villTaBort) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", villTaBort);
            }
            else
            {
                dict.RemoveAt(found);
            }
        }

        static void ChangePerson(List<Person> dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string villÄndra = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < dict.Count(); i++)
            {
                if (dict[i].namn == villÄndra) found = i;
            }

            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", villÄndra);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fältAttÄndra = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fältAttÄndra, villÄndra);
                string nyttVärde = Console.ReadLine();

                dict[found].Modify(fältAttÄndra, nyttVärde);
            }
        }
    }
}
