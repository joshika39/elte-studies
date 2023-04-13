using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;

namespace HF2
{
    internal abstract class Program
    {
        private static void Main()
        {
            Bootstrapper.Initialize(out var reader);
            var streamReader = new StreamReader(@"Res\imp.txt");
            var smallest = int.MaxValue;
            var hasPrime = "";
            
            foreach (var numList in reader.ReadLine<int>(streamReader, int.TryParse, ' '))
            {
                foreach (var num in numList)
                {
                    hasPrime = Tools.IsPrime(num) ? "van" : "nincs";
                    if (num < smallest && num % 2 == 0)
                    {
                        smallest = num;
                    }
                }
            }

            Console.WriteLine($"{smallest} {hasPrime}");
        }
    }
}
