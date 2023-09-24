using System;
using System.Collections.Generic;
using System.IO;
using Implementation.IO.Factories;
using Implementation.Logger.Factories;
using Infrastructure.IO;

namespace Horgaszverseny
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            var ioFactory = new IOFactory();
            using var logger = new LoggerFactory().CreateLogger(id);
            
            var writer = ioFactory.CreateWriter(logger);
            var reader = ioFactory.CreateReader(logger, writer);
            
            var file = new StreamReader(@"Resources/input2.txt");

            var test = reader.ReadLine<Fisher>(file, Fisher.TryParse);
            var fishers = new List<Fisher>();
            
            foreach (var fisher in test)
            {
                var hasPonty = false;
                var counter = 0;
                int i;
                for (i = 0; i < fisher.Cathces.Count && !hasPonty; i++)
                {
                    var @catch = fisher.Cathces[i];
                    if (@catch.Name == "ponty" && @catch.Weight >= 1)
                    {
                        hasPonty = true;
                    }
                }

                for (; i < fisher.Cathces.Count; i++)
                {
                    var @catch = fisher.Cathces[i];
                    if (@catch.Name == "harcsa" && @catch.Length >= 1 && hasPonty)
                    {
                        counter++;
                    }
                }

                if (counter >= 4)
                {
                    fishers.Add(fisher);
                }
            }

            foreach (var fisher in fishers)
            {
                Console.WriteLine($"{fisher.Name}");
            }
        }
    }
}