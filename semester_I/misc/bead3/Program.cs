using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace bead3
{
    class Competitor{
        public int ID {get;set;}
        public List<int> Points { get; set;}
        public int PointSum {get; set;}
        public Competitor(string line, int id, int pointPcs)
        {
	    ID = id;
	    Points = new List<int>();
            var sNum = line.Split(' ');
            foreach (var num in sNum){
                if (!string.IsNullOrWhiteSpace(num))
                {
                    var test = int.TryParse(num, out var intNum);
                    if (test)
                    {
                        Points.Add(intNum);
                    }
                    else
                    {
                        Console.WriteLine("Error With Input");
                    }
                }
            }
            var max = Points.Max();
            var min = Points.Min();
            PointSum = Points.Sum() - max - min;
        }
        public Competitor(int id, int pointPcs)
        {
            ID = id;
            Points = new List<int>();
            var rand = new Random();
            for(var i = 0; i < pointPcs; i++)
            {
                var num = rand.Next(0, 10);
                Points.Add(num);
                Console.Write($"{num} ");
            }
            Console.WriteLine();
            
            var max = Points.Max();
            var min = Points.Min();
            PointSum = Points.Sum() - max - min;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int length = 0;
            var competitors = new List<Competitor>();

            var @params = Console.ReadLine().Split(' ');
            length = int.Parse(@params[0]);
            for (int i = 0; i < length; i++){
                competitors.Add(new Competitor(Console.ReadLine(), i + 1, int.Parse(@params[1])));
            }

            competitors = competitors.OrderByDescending(c => c.PointSum).ThenBy(c => c.ID).ToList();
		    var place = 1;
            var currPoint = competitors[0].PointSum;
		    for(var i = 0; i < length; i++)
            {
		        var competitor = competitors[i];

                if(competitors[i].PointSum != currPoint)
                {
                    place = i + 1;
                    currPoint = competitor.PointSum;
                }
                Console.WriteLine($"{place} {competitor.ID} {competitor.PointSum}");
            }	
        }
    }
}
