using System;
using System.Collections.Generic;

namespace Bead1
{
    internal class Program
    {
        class Person
        {
            public Person(string line)
            {
                var data = line.Split(' ');
                Education = new College(int.Parse(data[3]), int.Parse(data[4]));
                Birthday = new DateTime(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));
            }
            public College Education { get; set; }
            public DateTime Birthday { get; set; }
        }

        class College
        {
            public College(int startDate, int endDate)
            {
                StartDate = startDate;
                EndDate = endDate;
            }

            public int StartDate { get; set; }
            public int EndDate { get; set; }
        }

        static void Main(string[] args)
        {
            if(args.Length != 0)
            {

            }
            var seasons = new int[] { 0, 0, 0, 0 };
            var inputNum = int.Parse(Console.ReadLine());
            var people = new List<Person>();
            for (int i = 0; i < inputNum; i++)
            {
                var line = Console.ReadLine();
                if(line != null )
                    people.Add(new Person(line));
            }
            foreach (var person in people)
            {
                if (person.Birthday.Month >= 1 && person.Birthday.Month <= 2) seasons[3]++;
                else if (person.Birthday.Month >= 3 && person.Birthday.Month <= 5) seasons[0]++;
                else if (person.Birthday.Month >= 6 && person.Birthday.Month <= 8) seasons[1]++;
                else if (person.Birthday.Month >= 9 && person.Birthday.Month <= 11) seasons[2]++;
                else if (person.Birthday.Month == 12) seasons[3]++;
            }
            foreach (var season in seasons)
            {
                Console.Write($"{season} ");
            }
        }
    }
}