using System;
using System.IO;
using System.Linq;
using Implementation.IO.Factories;
using Implementation.Logger.Factories;

namespace HF2
{
    internal abstract class Program
    {
        private static void Main()
        {
            var id = Guid.NewGuid();
            using var logger = new LoggerFactory().CreateLogger(id);
            var ioFactory = new IOFactory();
            var writer = ioFactory.CreateWriter(logger);
            var reader = ioFactory.CreateReader(logger, writer);

            var streamReader = new StreamReader(@"Res\imp.txt");

            var nums = reader.ReadLine<int>(streamReader, int.TryParse, ' ').FirstOrDefault().ToList();

            var hasPrime = nums.Any(IsPrime) ? "van" : "nincs";
            var smallest = nums.Where(n => n % 2 == 0).Min();
            
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
