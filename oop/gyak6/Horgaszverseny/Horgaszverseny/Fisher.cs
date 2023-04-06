using System.Collections.Generic;
using System.Linq;

namespace Horgaszverseny
{
    public class Fisher
    {
        public string Name { get; set; }
        public List<Catch> Cathces { get; set; }

        public Fisher(string name)
        {
            Name = name;
            Cathces = new List<Catch>();
        }
        
        public Fisher(string name, List<Catch> catches)
        {
            Name = name;
            Cathces = catches;
        }

        public static bool TryParse(string line, out Fisher result)
        {
            var elements = line.Split(' ').ToList();
            elements.RemoveAll(string.IsNullOrWhiteSpace);
            var name = elements[0];
            elements.RemoveAt(0);
            var i = 0;
            var catches = new List<Catch>();
            var tmpCatch = new Catch();
            foreach (var catchData in elements)
            {
                var pos = i % 4;
                switch (pos)
                {
                    case 0:
                        tmpCatch = new Catch
                        {
                            Name = catchData
                        };
                        break;
                    case 1:
                        tmpCatch.Time = catchData;
                        break;
                    case 2:
                        tmpCatch.Weight = double.Parse(catchData);
                        break;
                    case 3:
                        tmpCatch.Length = double.Parse(catchData);
                        catches.Add(tmpCatch);
                        break;
                }
                i++;
            }
            result = new Fisher(name, catches);
            return true;
        }
    }
}
