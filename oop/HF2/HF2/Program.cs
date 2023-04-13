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
            var nums = reader.ReadLine<int>(streamReader, int.TryParse).ToList();
            
            var hasPrime = nums.Any(Tools.IsPrime) ? "van" : "nincs";
            var smallest = nums.Where(n => n % 2 == 0).Min();
            
            Console.WriteLine($"{smallest} {hasPrime}");
        }
    }
}
