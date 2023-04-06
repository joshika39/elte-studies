using System;
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
            var file = new StreamReader(@"Resources/input.txt");
            
            var test = reader.ReadLine<Fisher>(file, Fisher.TryParse);
            
            foreach (var fisher in test)
            {
                Console.Write(fisher.Name);
                foreach (var @catch in fisher.Cathces)
                {
                    Console.Write($"{@catch.Name} ");
                }
                Console.WriteLine();
            }
        }
    }
}
