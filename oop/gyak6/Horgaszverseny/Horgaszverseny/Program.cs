using System;
using System.Collections.Generic;
using System.IO;
using Implementation.Core;
using Infrastructure.IO;
using Unity;

namespace Horgaszverseny
{
    class Program
    {    
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            var c = Bootsrapper.GetDefaultContainer("log.txt", id);
            var reader = c.Resolve<IReader>();
            var file = new StreamReader(@"Resources/input2.txt");
            
            var test = reader.ReadLine<Fisher>(file, Fisher.TryParse);
            var fishers = new List<Fisher>();
            foreach (var fisher in test)
            {
                var hasPonty = false;
                var added = false;
                var counter = 0;
                foreach (var @catch in fisher.Cathces)
                {
                    if (@catch.Name == "ponty" && @catch.Weight >= 1)
                    {
                        hasPonty = true;
                    }
                    if (@catch.Name == "harcha" && @catch.Length >= 1 && hasPonty)
                    {
                        counter++;
                    }
                    if (counter >= 4 && !added)
                    {
                        added = true;
                        fishers.Add(fisher);
                    }
                }
                hasPonty = false;
                added = false;
                counter = 0;
            }
            foreach (var fisher in fishers)
            {
                Console.WriteLine($"{fisher.Name}");
            }
            
        }
    }
}
