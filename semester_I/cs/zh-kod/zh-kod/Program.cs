using System;
using System.Collections.Generic;
using System.Linq;

namespace zh_kod
{
    class Kereso
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public int Salary { get; set; }
        public Kereso(string data, int id)
        {
            Id = id;
            var fData = data.Split(' ');
            if (fData.Length > 3)
            {
                Name = $"{fData[2]} {fData[3]}";
            }
            else
            {
                Name = $"{fData[2]}";
            }
            Month = int.Parse(fData[1]);
            Salary = int.Parse(fData[0]);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = Console.ReadLine().Split(' ');
            var people = new List<Kereso>();

            for (var i = 0; i < int.Parse(options[0]); i++)
            {
                people.Add(new Kereso(Console.ReadLine(), i + 1));
            }

            Console.WriteLine("#");
            var maxP = people.Where(n => n.Salary == people.Max(p => p.Salary));
            Console.WriteLine(maxP.FirstOrDefault()?.Name);

            Console.WriteLine("#");
            // 3ik feladat
            var grouped = people.GroupBy(x => x.Name).Select(
                g => new
                {
                    Type = g.Key,
                    People = g.Select(person => new { person.Name, person.Salary })
                });
            var groups = grouped.ToList();
            
            // 2ik feladat
            Console.WriteLine(groups.Count);
            
            var dict = new Dictionary<string, int>();
            foreach (var person in people)
            {
                if (!dict.Keys.Contains(person.Name))
                {
                    dict.Add(person.Name, person.Salary * person.Month * 100000);
                }
                else
                {
                    dict[person.Name] += person.Salary * person.Month * 100000;
                }
            }
            foreach (var pair in dict)
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }

            Console.WriteLine("#");
            
            var hasMono = false;
            foreach (var peopleList in groups.Select(group => group.People.ToList()))
            {
                for (var i = 0; i < peopleList.Count; i++)
                {
                    var person = peopleList[i];
                    if (i + 1 < peopleList.Count)
                    {
                        // Console.WriteLine($"{person.Name}: {person.Salary} -> {peopleList[i + 1].Salary}");
                        if (peopleList[i].Salary < peopleList[i + 1].Salary && !hasMono)
                        {
                            Console.WriteLine($"{person.Name}");
                            hasMono = true;
                            break;
                        }
                    }

                }
            }
            if (!hasMono)
            {
                Console.WriteLine("NINCS");
            }

            Console.WriteLine("#");
            dict = new Dictionary<string, int>();
            foreach (var person in people)
            {
                // Console.WriteLine($"Test: {person.Name}");
                if (!dict.Keys.Contains(person.Name))
                {
                    dict.Add(person.Name, person.Salary);
                }
                else
                {
                    if (dict[person.Name] < person.Salary)
                    {
                        dict[person.Name] = person.Salary;
                    }
                }
            }
            var bigger = dict.Where(x => x.Value > int.Parse(options[1])).ToList();
            Console.WriteLine(bigger.Count);
            foreach (var pair in bigger)
            {
                Console.WriteLine(pair.Key);
            }
        }
    }
}
