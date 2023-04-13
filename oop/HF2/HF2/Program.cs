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
            // var nums = new List<int>();
            var nums = reader.ReadLine<int>(streamReader, int.TryParse, ' ').ToList();
            
            var hasPrime = nums.Any(l => l.Any(IsPrime)) ? "van" : "nincs";
            var smallest = nums.Min(l => l.Min());
            
            Console.WriteLine($"{smallest} {hasPrime}");
        }
        
        private static bool IsPrime(int number)
        {
            switch (number)
            {
                case <= 1:
                    return false;
                case 2:
                    return true;
            }
            
            if (number % 2 == 0)
            {
                return false;
            }

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (var i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
                
            return true;        
        }
    }
}
